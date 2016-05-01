using System.ComponentModel.DataAnnotations;

namespace UniversityCourseResultManagementSystem.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        [Display(Name = "Grade")]
        public string GradeName { get; set; }
    }
}