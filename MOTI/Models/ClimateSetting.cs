using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MOTI.Models.Enums;

using Newtonsoft.Json;

namespace MOTI.Models {
    public class ClimateSetting {
        [Key]
        public int ClimateSettingId { get; set; }
        public ClimateType ClimateType { get; set; }
        public double Value { get; set; }
        public string Units { get; set; } 
        
        
        public int RequestId { get; set; }
        [ForeignKey("RequestId")]
        public Request Request { get; set; }
        
    }
}