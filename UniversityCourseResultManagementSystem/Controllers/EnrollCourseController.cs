using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem.Controllers
{
    public class EnrollCourseController : Controller
    {
        public ProjectDbContext db = new ProjectDbContext();

        // GET: /EnrollCourse/
        public ActionResult Index()
        {
            var enrollcourses = db.EnrollCourses.Include(e => e.Course);
            return View(enrollcourses.ToList());
        }

        // GET: /EnrollCourse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollcourse = db.EnrollCourses.Find(id);
            if (enrollcourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollcourse);
        }

        // GET: /EnrollCourse/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            ViewBag.StudentList = db.Students.ToList();
            return View();
        }

        // POST: /EnrollCourse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EnrollCourseId,RegistrationId,CourseId,EnrollDate,GradeName")] EnrollCourse enrollcourse)
        {
            if (ModelState.IsValid)
            {
                db.EnrollCourses.Add(enrollcourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", enrollcourse.CourseId);
            return View(enrollcourse);
        }

        // GET: /EnrollCourse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollcourse = db.EnrollCourses.Find(id);
            if (enrollcourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", enrollcourse.CourseId);
            return View(enrollcourse);
        }

        // POST: /EnrollCourse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="EnrollCourseId,RegistrationId,CourseId,EnrollDate,GradeName")] EnrollCourse enrollcourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollcourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", enrollcourse.CourseId);
            return View(enrollcourse);
        }

        // GET: /EnrollCourse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollcourse = db.EnrollCourses.Find(id);
            if (enrollcourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollcourse);
        }

        public ActionResult SaveResult()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            ViewBag.StudentList = db.Students.ToList();
            ViewBag.GradeList = db.Grades.ToList();
            return View();
        }

        // POST: /EnrollCourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnrollCourse enrollcourse = db.EnrollCourses.Find(id);
            db.EnrollCourses.Remove(enrollcourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult ViewResult()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            ViewBag.StudentList = db.Students.ToList();
            return View();
        }

        public JsonResult GetStudentById(string regNo)
        {
            var student = db.Students.Where(s => s.RegistrationId == regNo).ToList();
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoursesbyDept(int deptCode)
        {
            var courses = db.Courses.Where(c => c.DepartmentId == deptCode).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsEnrolled(string regNo, int courseId)
        {
            var enrollCourses = db.EnrollCourses.Where(s => s.RegistrationId == regNo && s.CourseId == courseId);
            int itm = enrollCourses.Count();
            if (itm==0)
            {
                return Json(false);
            }
            return Json(true);
        }

        public ActionResult EnrollCoursetoStudent(EnrollCourse enrollCourse)
        {
            var enrollCourses = db.EnrollCourses.Where(s => s.RegistrationId == enrollCourse.RegistrationId && s.CourseId == enrollCourse.CourseId).ToList();
            int itm = enrollCourses.Count();
            if (itm == 1)
            {
                var id = enrollCourses[0].EnrollCourseId;
                var date = enrollCourses[0].EnrollDate;
                enrollCourse.EnrollCourseId = id;
                enrollCourse.EnrollDate = date;
                db.EnrollCourses.AddOrUpdate(enrollCourse);
            }
            else
            {
                db.EnrollCourses.Add(enrollCourse);
            }
           
            db.SaveChanges();
            return Json(true);
        }

        public ActionResult GetCoursesbyRegNo(string regNo)
        {
            var courses = db.EnrollCourses.Where(c => c.RegistrationId == regNo).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

    }
}
