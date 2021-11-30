using System.ComponentModel.DataAnnotations;

namespace MOTI.Models {
    public class Device {
        [Key]
        public int DeviceId { get; set; }

        public string SerialNumber { get; set; }
        public double Power { get; set; }
        public bool IsWorking { get; set; }
    }
}