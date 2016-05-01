using System.ComponentModel.DataAnnotations;

namespace UniversityCourseResultManagementSystem.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        [Display(Name = "Room No")]
        public string Name { get; set; }
    }
}