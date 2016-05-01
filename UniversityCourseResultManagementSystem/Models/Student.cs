using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace UniversityCourseResultManagementSystem.Models
{
    public class Student
    {
         [Display(Name = "Student")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Name field must not be empty!")]
        [Display(Name = "Name")]
        public string StudentName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Index(IsUnique = true)]
        [StringLength(50)]
        [EmailAddress]
        [Remote("IsStudentEmailAvailable", "Student", ErrorMessage = "This email already exist!")]
        [Display(Name = "Email")]
           
        public string Email { get; set; }

        [Phone(ErrorMessage = "Please provide correct Contact No format")]
        [Display(Name = "Contact No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?[+]([8]{2})([0]{1})([1]{1})([1-9]{1})([0-9]{8})$", ErrorMessage = "Invalid Phone number. Try this format (8801XXXXXXXXX)")]
        public string ContactNo { get; set; }


        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department d { get; set; }

        [Display(Name = "Reg. No")]
        public virtual string RegistrationId { get; set; }
    }
}