using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var stopwatch = Stopwatch.StartNew();
            //Fetch data
            var room = await context.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId);
            var request = await context.Requests.FirstOrDefaultAsync(r => r.RequestId == requestId);
            var climateSettings = await context.ClimateSettings.Where(cs => cs.RequestId == requestId)
                .ToListAsync();
            var types = climateSettings.Select(cs => cs.ClimateType)
                .ToList();
            var devices = await context.Devices.Where(d => d.RoomId == roomId && types.Contains(d.ClimateType))
                .ToListAsync();
            Console.WriteLine("### Fetched the DB data");
            
            devices.Sort((device,
                device1) => Convert.ToInt32(Math.Round(device.Consumption - device1.Consumption)));
            Console.WriteLine("### Sorted devices by consumption ASC");
            
            var result = new List<Device>();
            
            var timingsList = new List<double>();
            var i=0;
            var j=0;
            foreach (var climateSetting in climateSettings) {
                Console.WriteLine($"### Loop iteration: {i++}");
                
                var tempResult = new List<Device>();
                var filteredDevices = devices.Where(d => d.ClimateType.Equals(climateSetting.ClimateType));
                // Console.WriteLine($"### Filtered devices by climate type");
                foreach (var device in filteredDevices) {
                    Console.WriteLine($"### Device loop iteration: {j++}");
                    tempResult.Add(device);
                    var totalTime = GetTime(tempResult, climateSetting.Value);
                    if (!(totalTime <= request.MaxTime)) continue;
                    timingsList.Add(totalTime);
                    result.AddRange(tempResult);
                    break;
                }
                j = 0;
            }
            Console.WriteLine($"### Algorithm is complete! Elapsed: {stopwatch.ElapsedMilliseconds}ms");

            Console.WriteLine("### Devices: ");
            foreach (var device in result) {
                Console.Write($"{device.SerialNumber} ");
            }
            Console.WriteLine(";");
            Console.WriteLine("### Time will be spent: {0}", timingsList.Max());
            Console.WriteLine("### Total consumption:{0}", GetConsumption(result));
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