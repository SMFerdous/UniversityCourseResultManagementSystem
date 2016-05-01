using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem.Controllers
{
    public class RoomAllocationController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: /RoomAllocation/
        public ActionResult Index()
        {

            ViewBag.departments = db.Departments.ToList();
            return View();
        }

        // GET: /RoomAllocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            if (roomallocation == null)
            {
                return HttpNotFound();
            }
            return View(roomallocation);
        }

        // GET: /RoomAllocation/Create
        public ActionResult Create()
        {
            ViewBag.courses = db.Courses.Where(s=>s.Status==true).ToList();
            ViewBag.departments = db.Departments.ToList();
            ViewBag.Rooms = db.Rooms.ToList();
            ViewBag.Days = db.Days.ToList();
            return View();
        }

        //// POST: /RoomAllocation/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="RoomAllocationId,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime")] RoomAllocation roomallocation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.RoomAllocations.Add(roomallocation);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", roomallocation.CourseId);
        //    ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomallocation.DayId);
        //    ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptCode", roomallocation.DepartmentId);
        //    ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomallocation.RoomId);
        //    return View(roomallocation);
        //}

        // GET: /RoomAllocation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            if (roomallocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", roomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptName", roomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomallocation.RoomId);
            return View(roomallocation);
        }

        // POST: /RoomAllocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomAllocationId,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime")] RoomAllocation roomallocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomallocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", roomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptName", roomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomallocation.RoomId);
            return View(roomallocation);
        }

        // GET: /RoomAllocation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            if (roomallocation == null)
            {
                return HttpNotFound();
            }
            return View(roomallocation);
        }

        // POST: /RoomAllocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            db.RoomAllocations.Remove(roomallocation);
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

        public JsonResult GetCoursesByDeptId(int deptId)
        {
            var courses = db.Courses.Where(t => t.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveRoomSchedule(RoomAllocation roomAllocation)
        {
            var scheduleList = db.RoomAllocations.Where(t => t.RoomId == roomAllocation.RoomId && t.DayId == roomAllocation.DayId && t.Status=="Allocated").ToList();
            if (scheduleList.Count == 0)
            {
                roomAllocation.Status = "Allocated";
                db.RoomAllocations.Add(roomAllocation);
                db.SaveChanges();
                return Json(true);
            }
            else
            {
                bool state = false;
                foreach (var allocation in scheduleList)
                {
                    if ((roomAllocation.StartTime >= allocation.StartTime && roomAllocation.StartTime < allocation.EndTime)
                         || (roomAllocation.EndTime > allocation.StartTime && roomAllocation.EndTime <= allocation.EndTime) && roomAllocation.Status=="Allocated")
                    {
                        state = true;
                    }
                }
                if (state == false)
                {
                    roomAllocation.Status = "Allocated";
                    db.RoomAllocations.Add(roomAllocation);
                    db.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }

        }

        public JsonResult GetClassScheduleInfo(int deptId)
        {
            var courses = db.Courses.Where(t => t.DepartmentId == deptId).ToList();
            List<ClassSchedule> classSchedules = new List<ClassSchedule>();
            foreach (var course in courses)
            {
                string schedule = "";
                int counter = 0;
                var courseSchedules = db.RoomAllocations.Where(t => t.DepartmentId == course.DepartmentId && t.CourseId == course.CourseId && t.Status=="Allocated").ToList();
                foreach (var courseSchedule in courseSchedules)
                {
                    if (counter != 0)
                    {
                        schedule += "; ";
                    }
                    schedule += "R. No : " + courseSchedule.Room.Name + ", " + courseSchedule.Day.Name.Substring(0, 3) + ", ";
                    int h, m;
                    string p = "AM";
                    int st = courseSchedule.StartTime;
                    h = st / 60;
                    m = st - (h * 60);
                    if (st >= 720)
                    {
                        h -= 12;
                        if (h == 0) h = 12;
                        p = "PM";
                    }
                    schedule += h + ":" + m.ToString("00") + " " + p + " - ";
                    int et = courseSchedule.EndTime;
                    h = et / 60;
                    m = et - (h * 60);
                    p = "AM";
                    if (et >= 720)
                    {
                        h -= 12;
                        if (h == 0) h = 12;
                        p = "PM";
                    }
                    schedule += h + ":" + m.ToString("00") + " " + p;
                    counter++;
                }
                if (schedule == "") schedule = "Not Scheduled Yet.";
                ClassSchedule classSchedule = new ClassSchedule(course.CourseCode, course.CourseName, schedule);
                classSchedules.Add(classSchedule);
            }
            return Json(classSchedules, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnallocateRoom()
        {
            return View();
        }

        public JsonResult UnallocateAllRooms(bool name)
        {
            var roomInfo = db.RoomAllocations.Where(r => r.Status == "Allocated").ToList();
            if (roomInfo.Count == 0)
            {
                return Json(false);
            }
            else
            {
                foreach (var room in roomInfo)
                {
                    room.Status = null;
                    db.RoomAllocations.AddOrUpdate(room);
                    db.SaveChanges();

                }
                return Json(true);
            }
           
             
        }
    }
}
