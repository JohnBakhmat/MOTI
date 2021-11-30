using System.ComponentModel.DataAnnotations;

namespace MOTI.Models {
    public class ClimateSetting {
        [Key]
        public int ClimateSettingId { get; set; }
        public string Expression { get; set; }
        public double Value { get; set; }
        public string Units { get; set; }
    }
}