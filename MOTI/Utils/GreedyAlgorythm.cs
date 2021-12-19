using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MOTI.Data;
using MOTI.Models;

using NuGet.Protocol;

namespace MOTI.Utils {
    public static class GreedyAlgorythm {
        public static async Task<IEnumerable<Device>> OptimizeDevices(ApplicationDbContext context, int roomId, int requestId) {
            //Fetch data
            var room = await context.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId);
            var request =  await context.Requests.FirstOrDefaultAsync(r => r.RequestId == requestId);
            var devices = await context.Devices.Where(d => d.RoomId == roomId).ToListAsync();
            var climateSettings = await context.ClimateSettings.Where(cs => cs.RequestId == requestId).ToListAsync();
            var result = new List<Device>();
            
            devices.Sort((device, device1) => Convert.ToInt32(Math.Round(device.Capacity - device1.Capacity)));
            double time = 0f;
            foreach (var device in devices) {
                result.Add(device);
                time = GetTime(result, climateSettings[0].Value);
                Console.WriteLine($"###: {result.Count} {time} {request.MaxTime}");
                if (time <= request.MaxTime) {
                    break;
                }
            }

            Console.WriteLine($"Device Count: {result.Count}; Time: {time}");
            return result;
        }

        private static double GetTime(IEnumerable<Device> devices, double changes) {
            var sumCapacity = 0.0;
            foreach (var device in devices) {
                if (device.Status == DeviceStatus.Working) {
                    sumCapacity += device.Capacity;
                }
            }
            Console.WriteLine($"*** Capacity: {sumCapacity}");
            var sumTime = changes / sumCapacity;
            return sumTime;
        }
    }
}