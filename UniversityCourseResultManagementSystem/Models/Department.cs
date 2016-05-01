using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UniversityCourseResultManagementSystem.Models
{
    public class Department
    {
         [Display(Name = "Department")]
        public int DepartmentId { get; set; }


        [Required(ErrorMessage="Department code is required")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Department code must be two 2 to 7 characters long")]
        [Remote("IsDeptCodeAvailable", "Department", ErrorMessage = "Department Code already exist.")]
        [Display(Name = "Department Code")]
        public string DeptCode { get; set; }


        [Required(ErrorMessage = "Department Name is required")]
        [Remote("IsDeptNameAvailable", "Department", ErrorMessage = "Department Name already exist.")]
        [Display(Name = "Name")]
        public string DeptName { get; set; }
    }
}