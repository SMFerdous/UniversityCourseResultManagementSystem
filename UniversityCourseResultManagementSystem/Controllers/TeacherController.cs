using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementSystem.Models;
using WebGrease.Css.Extensions;

namespace UniversityCourseResultManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: /Teacher/
        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.d).Include(t => t.Desg);
            return View(teachers.ToList());
        }
        
     

        // GET: /Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: /Teacher/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptName");
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
            return View();
        }

        // POST: /Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TeacherId,TeacherName,Address,Email,ContactNo,DesignationId,DepartmentId,TakenCredit")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.RemainingCredit = teacher.TakenCredit;
                db.Teachers.Add(teacher);
                db.SaveChanges();
                TempData["Success"] = "Teacher Successfully Saved!";
                return RedirectToAction("Create");
                //return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptName", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }

        // GET: /Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptName", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }

        // POST: /Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TeacherId,TeacherName,Address,Email,ContactNo,DesignationId,DepartmentId,Credit")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptName", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }

        // GET: /Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: /Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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
        public JsonResult IsTeacherEmailAvailable(string teacherEmail)
        {
            return Json(!db.Teachers.Any(x => x.Email == teacherEmail),
                                                 JsonRequestBehavior.AllowGet);
        }

        //check teacher email is available

        //public JsonResult IsTeacherEmailAvailable(string teacherEmail)
        //{
            
        //    return Json(!db.Teachers.Any(x=>x.Email==teacherEmail),
        //                                         JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult IsTeacherEmailAvailable(string teacherEmail)
        //{
        //    var dept = db.Teachers.ToList();
        //    if (!dept.Any(x => x.Email == teacherEmail))
        //    {
        //        // This show the error message of validation and stop the submit of the form
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        // This will ignore the validation and the submit of the form is gone to take place.
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

       
       

        //public JsonResult IsTeacherEmailAvailable(string Email)
        //{
        //    var teachers = db.Teachers.Include(t => t.d).Include(t => t.Desg);
        //    if (teachers.Any(x => x.Email==Email))
        //    {
        //        // This show the error message of validation and stop the submit of the form
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        // This will ignore the validation and the submit of the form is gone to take place.
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}
