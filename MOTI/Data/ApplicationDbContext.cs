using Microsoft.EntityFrameworkCore;

using MOTI.Models;

namespace MOTI.Data {
    public class ApplicationDbContext:DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        
        public DbSet<ClimateSetting> ClimateSettings { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Preset> Presets { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}