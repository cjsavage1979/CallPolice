using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CallPolice.Models;

namespace CallPolice.Controllers
{
    public class AlarmsController : Controller
    {
        private CallPoliceEntities db = new CallPoliceEntities();

        // GET: Alarms
        public ActionResult Index()
        {
            var alarms = db.Alarms.Include(a => a.User);
            return View(alarms.ToList());
        }

        // GET: Alarms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alarms alarms = db.Alarms.Find(id);
            if (alarms == null)
            {
                return HttpNotFound();
            }
            return View(alarms);
        }

        // GET: Alarms/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: Alarms/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlarmId,UserId,Longitide,Latitude,FileType,FileName,AlarmContent")] Alarms alarms)
        {
            if (ModelState.IsValid)
            {
                db.Alarms.Add(alarms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", alarms.UserId);
            return View(alarms);
        }
        private string GetPostFix(string s)
        {

            string[] arr = s.Split('.');
            string PostFix = arr[arr.Length - 1];
            return PostFix;
        }
        [HttpPost]
        public JsonResult CreateAlarm(int UserId, string Longitide, string Latitude, int FileType, string AlarmContent, HttpPostedFileBase AlarmFile)
        {
            Alarms alarm = new Alarms();
            alarm.UserId = UserId;
            alarm.Longitide = Longitide;
            alarm.Latitude = Latitude;
            alarm.FileType = FileType;
            alarm.AlarmContent = AlarmContent;
            string guid = Guid.NewGuid().ToString();
            string fileName = "";
            if (AlarmFile != null && AlarmFile.ContentLength > 0)
            {
                switch (FileType)
                {
                    case 1: fileName = guid + ".jpg"; break;
                    case 2: fileName = guid + ".mp4"; break;
                    case 3: fileName = guid + ".wav"; break;

                    default:fileName = "";break;
                }

                alarm.FileName = fileName;
                fileName = Server.MapPath("~/Files/") + fileName;
               
                AlarmFile.SaveAs(fileName);
                db.Alarms.Add(alarm);
                db.SaveChanges();
                return Json(new { success = true, message = "添加报警成功", code = "000000" });
            }

            return Json(new { success = true, message = "添加报警失败", code = "800000" });
        }

        // GET: Alarms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alarms alarms = db.Alarms.Find(id);
            if (alarms == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", alarms.UserId);
            return View(alarms);
        }

        // POST: Alarms/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlarmId,UserId,Longitide,Latitude,FileType,FileName,AlarmContent")] Alarms alarms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alarms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", alarms.UserId);
            return View(alarms);
        }

        // GET: Alarms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alarms alarms = db.Alarms.Find(id);
            if (alarms == null)
            {
                return HttpNotFound();
            }
            return View(alarms);
        }

        // POST: Alarms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alarms alarms = db.Alarms.Find(id);
            db.Alarms.Remove(alarms);
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
