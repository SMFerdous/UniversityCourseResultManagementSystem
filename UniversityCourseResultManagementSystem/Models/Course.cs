using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace UniversityCourseResultManagementSystem.Models
{
    public class Course
    {
         [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course code is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Course Code must be at least 5 characters long")]
        [Remote("IsCourseCodeAvailable", "Course", ErrorMessage = "Course Code already exist.")]
        [Display(Name="Course Code")]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [Remote("IsCourseNameAvailable", "Course", ErrorMessage = "Course name already exist.")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Course credit is required")]
        [Range(0.5, 5.0, ErrorMessage = "Credit must be within 0.5 to 5.0")]
        [Display(Name = "Credit")]
        public double Credit { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

         
        public int DepartmentId { get; set; }
        [Display(Name = "Department")]
        [ForeignKey("DepartmentId")]      
        public virtual Department d { get; set; }

        
        public int SemesterId { get; set; }
        [Display(Name = "Semester")]
        [ForeignKey("SemesterId")]     
        public virtual Semester Sem{ get; set; }

        public virtual String AssignTo { get; set; }

        public bool Status { get; set; }
       // public virtual String ScheduleInfo { get; set; }

    }
}