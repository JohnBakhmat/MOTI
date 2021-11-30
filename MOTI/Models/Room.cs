using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOTI.Models {
    public class Room {
        [Key]
        public int RoomId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}