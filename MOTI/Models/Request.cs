using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MOTI.Models.Enums;


namespace MOTI.Models {
    public class Request {
        [Key]
        public int RequestId { get; set; }
        public DateTime DateTime { get; set; }
        public StatusType Status { get; set; }
        public ICollection<ClimateSetting> ClimateSettings { get; set; } = new List<ClimateSetting>();

        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        public int RoomId { get; set; }
    }

    
}