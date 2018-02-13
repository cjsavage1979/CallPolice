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
    public class PoliceNewsController : Controller
    {
        private CallPoliceEntities db = new CallPoliceEntities();

        // GET: PoliceNews
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        public JsonResult PoliceNewsIndex()
        {
            var news = db.News.OrderByDescending(u => u.NewsId).ToList();
            List<AndroidNews> an = new List<AndroidNews>();
            foreach(PoliceNews pn in news)
            {
                AndroidNews nn = new AndroidNews() ;
                nn.NewsId = pn.NewsId;
                nn.NewsTitle = pn.NewsTitle;
                nn.NewsContent = pn.NewsContent;
                nn.CreateTime = pn.CreateTime.ToString("yyyy-MM-dd hh:mm:ss");
                an.Add(nn);
            }
            return Json(an, JsonRequestBehavior.AllowGet);
        }


        // GET: PoliceNews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceNews policeNews = db.News.Find(id);
            if (policeNews == null)
            {
                return HttpNotFound();
            }
            return View(policeNews);
        }

        // GET: PoliceNews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoliceNews/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,NewsTitle,NewsContent")] PoliceNews policeNews)
        {
            policeNews.CreateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.News.Add(policeNews);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(policeNews);
        }

        // GET: PoliceNews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceNews policeNews = db.News.Find(id);
            if (policeNews == null)
            {
                return HttpNotFound();
            }
            return View(policeNews);
        }

        // POST: PoliceNews/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,NewsTitle,NewsContent")] PoliceNews policeNews)
        {
            policeNews.CreateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(policeNews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(policeNews);
        }

        // GET: PoliceNews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceNews policeNews = db.News.Find(id);
            if (policeNews == null)
            {
                return HttpNotFound();
            }
            return View(policeNews);
        }

        // POST: PoliceNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PoliceNews policeNews = db.News.Find(id);
            db.News.Remove(policeNews);
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
