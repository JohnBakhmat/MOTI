using Microsoft.EntityFrameworkCore;

using MOTI.Models;

namespace MOTI.Data {
    public class ApplicationDbContext:DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<ClimateSetting> ClimateSettings { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}