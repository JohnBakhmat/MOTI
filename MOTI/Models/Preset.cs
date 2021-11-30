using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOTI.Models {
    public class Preset {
        [Key]
        public int PresetId { get; set; }
        public string Title { get; set; }
        public ICollection<ClimateSetting> Settings { get; set; } = new List<ClimateSetting>();
    }
}