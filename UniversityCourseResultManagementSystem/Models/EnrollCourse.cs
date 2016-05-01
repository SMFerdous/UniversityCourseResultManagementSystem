using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityCourseResultManagementSystem.Models
{
    public class EnrollCourse
    {
        public int EnrollCourseId { set; get; }
        [Required(ErrorMessage = "Pleae enter a student")]
        [Display(Name = "Student Reg. No.")]
        public string RegistrationId { set; get; }
        public virtual Student Student { set; get; }
        [Required(ErrorMessage = "Pleae enter a course")]
        [Display(Name = "Select Course")]
        public int CourseId { set; get; }
        public virtual Course Course { set; get; }
        [Display(Name = "Date")]
        [Required(ErrorMessage = "Please enter a date")]
        [DataType(DataType.Date)]
        public DateTime EnrollDate { set; get; }

        public virtual string GradeName { set; get; }
        
    }
}