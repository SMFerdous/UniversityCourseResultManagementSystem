using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {

        private ProjectDbContext db = new ProjectDbContext();
        
        // GET: /Department/
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }
        //check departmemt is available
        public JsonResult IsDeptCodeAvailable(string deptCode)
        {
            var dept = db.Departments.ToList();
            if (!dept.Any(x => x.DeptCode == deptCode.ToUpper()))
            {
                // This show the error message of validation and stop the submit of the form
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // This will ignore the validation and the submit of the form is gone to take place.
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult IsDeptCodeAvailable(string DeptCode)
        //{
        //    var department = !db.Departments.Any(x => x.DeptCode == DeptCode);
        //    return Json(department,JsonRequestBehavior.AllowGet);
        //}
             public JsonResult IsDeptNameAvailable(string deptName)
            {
                var dept = db.Departments.ToList();
                if (!dept.Any(x => x.DeptName == deptName))
                {
                    // This show the error message of validation and stop the submit of the form
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // This will ignore the validation and the submit of the form is gone to take place.
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            //public JsonResult IsDeptNameAvailable(string DeptName)
        //{
        //    var departments=!db.Departments.Any(x => x.DeptName == DeptName);
        //    return Json(departments, JsonRequestBehavior.AllowGet);
        //}
        

        // GET: /Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: /Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DepartmentId,DeptCode,DeptName")] Department department)
        {

            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                TempData["Success"] = "Department Successfully Saved!";
                return RedirectToAction("Create");
                //return RedirectToAction("Index");
            }

            
                return View(department);
            
            
        }

        // GET: /Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: /Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DepartmentId,DeptCode,DeptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: /Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: /Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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

        
        

    }
}
