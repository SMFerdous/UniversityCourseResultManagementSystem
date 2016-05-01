using System.ComponentModel.DataAnnotations;

namespace UniversityCourseResultManagementSystem.Models
{
    public class Semester
    {
        
        public int SemesterId { get; set; }

         [Display(Name = "Semester")]
        public string SemesterName { get; set; }

    }
}