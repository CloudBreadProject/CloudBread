using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudBreadAdminWeb;
using CloudBreadLib;
using CloudBreadLib.BAL.Crypto;

namespace CloudBreadAdminWeb.Controllers
{
    public class ___AdminMembersController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // GET: AdminMembers
        public ActionResult Index()
        {
            return View(db.AdminMembers.ToList());
        }

        // GET: AdminMembers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminMembers adminMembers = db.AdminMembers.Find(id);
            if (adminMembers == null)
            {
                return HttpNotFound();
            }
            return View(adminMembers);
        }

        // GET: AdminMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminMembers/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AdminMemberID,AdminMemberPWD,AdminMemberEmail,IDCreateAdminMember,AdminGroup,PINumber,Name1,Name2,Name3,DOB,LastIPaddress,LastLoginDT,LastLogoutDT,HideYN,AccountBlockYN,DeleteYN,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,Version,CreatedAt,UpdatedAt,Deleted")] AdminMembers adminMembers)
        {
            // PWD에 대해서 Hash 처리 진행
            adminMembers.AdminMemberPWD = Crypto.SHA512Hash(adminMembers.AdminMemberPWD);

            if (ModelState.IsValid)
            {
                try
                {
                    db.AdminMembers.Add(adminMembers);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
                               
            }

            return View(adminMembers);
        }

        // GET: AdminMembers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminMembers adminMembers = db.AdminMembers.Find(id);
            if (adminMembers == null)
            {
                return HttpNotFound();
            }
            return View(adminMembers);
        }

        // POST: AdminMembers/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdminMemberID,AdminMemberPWD,AdminMemberEmail,IDCreateAdminMember,AdminGroup,PINumber,Name1,Name2,Name3,DOB,LastIPaddress,LastLoginDT,LastLogoutDT,HideYN,AccountBlockYN,DeleteYN,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,Version,CreatedAt,UpdatedAt,Deleted")] AdminMembers adminMembers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminMembers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminMembers);
        }

        // GET: AdminMembers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminMembers adminMembers = db.AdminMembers.Find(id);
            if (adminMembers == null)
            {
                return HttpNotFound();
            }
            return View(adminMembers);
        }

        // POST: AdminMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AdminMembers adminMembers = db.AdminMembers.Find(id);
            db.AdminMembers.Remove(adminMembers);
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
