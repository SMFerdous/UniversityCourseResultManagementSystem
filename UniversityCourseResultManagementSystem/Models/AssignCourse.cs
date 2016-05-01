using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityCourseResultManagementSystem.Models
{
    public class AssignCourse
    {
        public int AssignCourseId { set; get; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department d { get; set; }
        public int TeacherId { set; get; }
        public virtual Teacher Teacher { get; set; }
        [DefaultValue(0.0)]
        public virtual double TakenCredit { set; get; }
        [DefaultValue(0.0)]
        public virtual double RemainingCredit { set; get; }
        public int CourseId { set; get; }
        public virtual Course Course { get; set; }
    }
}