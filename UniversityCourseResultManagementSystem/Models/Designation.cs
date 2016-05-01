using System.ComponentModel.DataAnnotations;

namespace UniversityCourseResultManagementSystem.Models
{
    public class Designation
    {
        public int DesignationId { get; set; }

         [Display(Name = "Designation")]
        public string DesignationName { get; set; }

    }
}