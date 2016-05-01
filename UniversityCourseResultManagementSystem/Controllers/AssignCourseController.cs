using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem.Controllers
{
    public class AssignCourseController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        public ActionResult ViewCourseStatus()
        {
            ViewBag.departments = db.Departments.ToList();
            return View();
        }

        public ActionResult Save()
        {
            ViewBag.departments = db.Departments.ToList();
            return View();
        }

        public JsonResult GetTeachersByDeptId(int deptId)
        {
            var teachers = db.Teachers.Where(t => t.DepartmentId == deptId).ToList();
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeachersById(int teacherId)
        {
            Teacher aTeacher = db.Teachers.FirstOrDefault(s => s.TeacherId == teacherId);
            return Json(aTeacher, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseById(int courseId)
        {
            Course course = db.Courses.FirstOrDefault(s => s.CourseId == courseId);
            return Json(course, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveAssignCourse(AssignCourse assignCourse)
        {
            var asignCoursesList = db.AssignCourses.Where(t => t.CourseId == assignCourse.CourseId && t.Course.Status==true).ToList();
            if (asignCoursesList.Count > 0)
            {

                return Json(false);
            }
            else
            {
                db.AssignCourses.Add(assignCourse);

                db.SaveChanges();


                var teacher = db.Teachers.FirstOrDefault(t => t.TeacherId == assignCourse.TeacherId);
                if (teacher != null)
                {
                    teacher.RemainingCredit = assignCourse.RemainingCredit;
                    
                    db.Teachers.AddOrUpdate(teacher);
                    db.SaveChanges();
                    var course = db.Courses.FirstOrDefault(t => t.CourseId == assignCourse.CourseId);
                    if (course != null)
                    {
                        course.Status = true;
                        course.AssignTo = teacher.TeacherName;
                        db.Courses.AddOrUpdate(course);
                        db.SaveChanges();
                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                return Json(false);
            }
        }

        public ActionResult CourseInfo(int deptId)
        {
            var courses = db.Courses.Where(t => t.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }
    }
}
