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
    public class UsersController : Controller
    {
        private CallPoliceEntities db = new CallPoliceEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,UserCellPhone,UserPwd,UserAddress,UserRelative")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        [HttpPost]
        public JsonResult CreateUser(string userCellphone, string userPwd)
        {
            var u = db.Users.Where(t => t.UserCellPhone == userCellphone);
              if (u.Count()==0)
            {
                Users user = new Users();
                user.UserCellPhone = userCellphone;
                user.UserPwd = userPwd;

                db.Users.Add(user);
                db.SaveChanges();

                return Json(new { success = true, message = "注册成功", code = "000000" });

            }
            else
            {
                return Json(new { success = false, message = "注册失败", code = "900001" });
            }



        }
        [HttpPost]
        public JsonResult UserLogin(string userCellphone, string userPwd)
        {
            var user = db.Users.SingleOrDefault(u => u.UserCellPhone == userCellphone);
            if (user!=null && user.UserPwd == userPwd)
            {
                return Json(new { success = true, message = "登录成功", userId = user.UserId, code = "000000" });
            }
            else
            {
                return Json(new { success = false, message = "用户名或密码错误", code = "900002" });
            }

        }
        [HttpPost]
        public JsonResult UpdateUserInfor(int userId, string userCellPhone, string userName, string UserRelativeCellPhone, string address)
        {
            var user = db.Users.SingleOrDefault(u => u.UserId == userId);
            if (user != null)
            { 

                user.UserCellPhone = userCellPhone;
                user.UserName = userName;
                user.UserRelative = UserRelativeCellPhone;
                user.UserAddress = address;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "修改用户信息成功", userId = user.UserId, code = "000000" });
            }
            else
            {
                return Json(new { success = true, message = "修改用户信息失败", userId = user.UserId, code = "900003" });
            }
        }
        [HttpPost]
        public JsonResult GetUserInfor(int userId)
        {
            var user = db.Users.SingleOrDefault(u => u.UserId == userId);
            if(user!=null)
            {

                return Json(new { success = true, message = "获取用户信息成功", UserName = user.UserName, UserCellPhone = user.UserCellPhone, UserAddress = user.UserAddress, UserRelative = user.UserRelative });
               

            }
            else
            {
                return Json(new { success = false, message = "获取用户信息失败", code = "900004" });
            }
        }


        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,UserCellPhone,UserPwd,UserAddress,UserRelative")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
