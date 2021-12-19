using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.Infrastructure;
using Microsoft.EntityFrameworkCore;

using MOTI.Data;
using MOTI.Models;

using NuGet.Protocol;

namespace MOTI.Utils {
    public static class GreedyAlgorythm {
        public static async Task<IEnumerable<Device>> OptimizeDevices(ApplicationDbContext context,
            int roomId,
            int requestId) {
            //Fetch data
            var room = await context.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId);
            var request = await context.Requests.FirstOrDefaultAsync(r => r.RequestId == requestId);
            var climateSettings = await context.ClimateSettings.Where(cs => cs.RequestId == requestId)
                .ToListAsync();
            var types = climateSettings.Select(cs => cs.ClimateType)
                .ToList();
            var devices = await context.Devices.Where(d => d.RoomId == roomId && types.Contains(d.ClimateType))
                .ToListAsync();
            devices.Sort((device,
                device1) => Convert.ToInt32(Math.Round(device.Consumption - device1.Consumption)));
            
            var result = new List<Device>();
            
            var timingsList = new List<double>();
            foreach (var climateSetting in climateSettings) {
                var tempResult = new List<Device>();
                foreach (var device in devices.Where(d=>d.ClimateType.Equals(climateSetting.ClimateType))) {
                    tempResult.Add(device);
                    var totalTime = GetTime(tempResult, climateSetting.Value);
                    if (!(totalTime <= request.MaxTime)) continue;
                    timingsList.Add(totalTime);
                    result.AddRange(tempResult);
                    break;
                }
            }
            
            Console.WriteLine("Devices: ");
            foreach (var device in result) {
                Console.Write($"{device.SerialNumber} ");
            }
            Console.WriteLine(";");
            Console.WriteLine("Time spent: {0}", timingsList.Max());
            Console.WriteLine("Total consumption:{0}", GetConsumption(result));
            return result;
        }

        
        private static double GetTime(IEnumerable<Device> devices,
            double changes) {
            var sumCapacity = devices.Where(device => device.Status == DeviceStatus.Working).Sum(device => device.Capacity);
            // Console.WriteLine($"*** Capacity: {sumCapacity}");
            var sumTime = changes / sumCapacity;
            return sumTime;
        }

        private static double GetConsumption(IEnumerable<Device> devices) =>
            devices.Sum(device => device.Consumption);
    }
}