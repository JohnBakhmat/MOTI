using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MOTI.Models.Enums;

namespace MOTI.Models {
    public class Device {
        [Key]
        public int DeviceId { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        public string SerialNumber { get; set; }
        public double Capacity { get; set; }
        public ClimateType ClimateType { get; set; }
    }
}