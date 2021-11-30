using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using MOTI.Models.Enums;

namespace MOTI.Models {
    public class Room {
        [Key]
        public int RoomId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Request> Requests { get; set; } = new List<Request>();
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}