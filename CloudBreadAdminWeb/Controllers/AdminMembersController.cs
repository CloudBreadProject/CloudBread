using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

//추가
using System.Threading.Tasks;
using CloudBreadAdminWeb;
using System.Diagnostics;
using PagedList;
using CloudBreadAdminWeb.globals;
using CloudBreadLib.BAL.Crypto;
using CloudBreadLib.BAL.UserTime;
using Logger.Logging;
using Newtonsoft.Json;


namespace CloudBreadAdminWeb.Controllers
{
    public class AdminMembersController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 개별 entity 복호화
        //public AdminMembers DecryptResult(AdminMembers item)
        //{
        //    try
        //    {
        //        // AdminMember 복호화 안함
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return item;

        //}

        //암호화 처리
        //public AdminMembers EncryptResult(AdminMembers item)
        //{
        //    try
        //    {
        //        //AdminMember 암호화 안함

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return item;

        //}


        //세션체크 Admin 또는 Operator 여부 체크
        public bool CheckSession()
        {
            try
            {
                string strSession = (this.Session["AdminGroup"] ?? "").ToString();
                if (strSession != "Admin")  // Admin 그룸만 접근 가능
                {
                    Session.Add("LoginAlert", "로그인 하지 않았거나 접근 권한이 부족합니다.");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        // GET: AdminMembers
        public ActionResult Index(string searchString, string SearchCondition, string currentFilter, int? page)
        {
            try
            {
                // Index 세션체크
                if (!CheckSession())
                {
                    return Redirect("/AdminLogin/Login");
                }

                //페이징 기본 처리
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                // AdminMembers는 암호화 안한다
                //암호화일 경우 searchString도 암호화해 검색 해야 한다.
                //if (globalVal.CloudBreadCryptSetting == "AES256" && searchString != null)
                //{
                //    searchString = Crypto.AES_encrypt(searchString, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                //    ViewBag.CurrentFilter = searchString;
                //}
                //else
                //{
                //    ViewBag.CurrentFilter = searchString;
                //}
                
                ViewBag.CurrentFilter = searchString;

                ViewBag.SearchCondition = SearchCondition;

                var queryString = from s in db.AdminMembers
                                  select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    switch (SearchCondition)
                    {
                        case "AdminMemberID":
                            queryString = queryString.Where(s => s.AdminMemberID.Contains(searchString));
                            break;
                        case "AdminMemberEmail":
                            queryString = queryString.Where(s => s.AdminMemberEmail.Contains(searchString));
                            break;
                        case "Name1":
                            queryString = queryString.Where(s => s.Name1.Contains(searchString));
                            break;
                        default:
                            break;
                    }
                }

                // 기본 order 처리 - CreatedAt으로 정렬 처리 - ToPagedList의 제약 조건
                queryString = queryString.OrderByDescending(s => s.CreatedAt);
                int pageSize = globalVal.CloudBreadAdminWebListPageSize;
                int pageNumber = (page ?? 1);
                var result = queryString.ToPagedList(pageNumber, pageSize);

                // AdminMember는 암호화 안한다
                //복호화 처리
                //if (globalVal.CloudBreadCryptSetting == "AES256")
                //{
                //    // 복호화
                //    foreach (var item in result)
                //    {
                //        DecryptResult(item);
                //    }
                //}

                //날자 데이터 현지 시각으로 변환
                foreach (var item in result)
                {
                    item.CreatedAt = UserTime.GetUserTime(item.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                    item.UpdatedAt = UserTime.GetUserTime(item.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "AdminMembersController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "AdminMembersController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: AdminMembers/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                // Detail 세션체크
                if (!CheckSession())
                {
                    return Redirect("/AdminLogin/Login");
                }

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //테이블 조회 - 필요할 경우 조인
                AdminMembers adminMembers = db.AdminMembers.Find(id);

                if (adminMembers == null)
                {
                    return HttpNotFound();
                }

                //암호화 안한다
                //복호화 처리
                //if (globalVal.CloudBreadCryptSetting == "AES256")
                //{
                //    DecryptResult(adminMembers);       //AdminMembers 타입
                //}

                //UTC를 세션의 UserTimeZone으로 변환 - 안하면 UTC 그대로 보임
                adminMembers.LastLoginDT = UserTime.GetUserTime(DateTime.Parse(adminMembers.LastLoginDT), Session["AdminTimeZone"].ToString()).ToString();
                adminMembers.LastLogoutDT = UserTime.GetUserTime(DateTime.Parse(adminMembers.LastLogoutDT), Session["AdminTimeZone"].ToString()).ToString();

                adminMembers.CreatedAt = UserTime.GetUserTime(adminMembers.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                adminMembers.UpdatedAt = UserTime.GetUserTime(adminMembers.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                adminMembers.DataFromRegionDT = UserTime.GetUserTime(adminMembers.DataFromRegionDT.DateTime, Session["AdminTimeZone"].ToString());

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "AdminMembersController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(adminMembers);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "AdminMembersController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: AdminMembers/Create
        public ActionResult Create()
        {
            try
            {
                // Create 세션체크
                if (!CheckSession())
                {
                    return Redirect("/AdminLogin/Login");
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "AdminMembersController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "AdminMembersController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: AdminMembers/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminMemberID,AdminMemberPWD,AdminMemberEmail,IDCreateAdminMember,AdminGroup,TimeZoneID,PINumber,Name1,Name2,Name3,DOB,LastIPaddress,LastLoginDT,LastLogoutDT,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] AdminMembers adminMembers)
        {
            try
            {
                // Create 세션체크
                if (!CheckSession())
                {
                    return Redirect("/AdminLogin/Login");
                }

                if (ModelState.IsValid)
                {
                    //입력값 기본 처리
                    adminMembers.AdminMemberPWD = Crypto.SHA512Hash(adminMembers.AdminMemberPWD);
                    adminMembers.IDCreateAdminMember = this.Session["AdminID"].ToString();
                    adminMembers.LastIPaddress = "";
                    adminMembers.LastLoginDT = "1900-01-01";
                    adminMembers.LastLogoutDT = "1900-01-01";
                    
                    adminMembers.CreatedAt = DateTimeOffset.UtcNow;
                    adminMembers.UpdatedAt = DateTimeOffset.UtcNow;
                    //adminMembers.DataFromRegionDT = DateTimeOffset.Parse("1900-01-01");

                    //암호화 안함
                    // Insert : 암호화 처리
                    //if (globalVal.CloudBreadCryptSetting == "AES256")
                    //{
                    //    //암호화
                    //    EncryptResult(adminMembers);
                    //}

                    db.AdminMembers.Add(adminMembers);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "AdminMembersController-Create(adminMembers)";
                    logMessage.Message = JsonConvert.SerializeObject(adminMembers);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(adminMembers);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "AdminMembersController-Create(adminMembers)";
                logMessage.Message = JsonConvert.SerializeObject(adminMembers);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: AdminMembers/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                // Edit 세션체크
                if (!CheckSession())
                {
                    return Redirect("/AdminLogin/Login");
                }

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AdminMembers adminMembers = db.AdminMembers.Find(id);
                if (adminMembers == null)
                {
                    return HttpNotFound();
                }

                //복호화 안함
                //복호화 처리
                //if (globalVal.CloudBreadCryptSetting == "AES256")
                //{
                //    DecryptResult(adminMembers);
                //}

                //UTC를 세션의 UserTimeZone으로 변환 - 안하면 UTC 그대로 보임
                adminMembers.LastLoginDT = UserTime.GetUserTime(DateTime.Parse(adminMembers.LastLoginDT), Session["AdminTimeZone"].ToString()).ToString();
                adminMembers.LastLogoutDT = UserTime.GetUserTime(DateTime.Parse(adminMembers.LastLogoutDT), Session["AdminTimeZone"].ToString()).ToString();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "AdminMembersController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(adminMembers);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "AdminMembersController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: AdminMembers/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminMemberID,AdminMemberPWD,AdminMemberEmail,IDCreateAdminMember,AdminGroup,TimeZoneID,PINumber,Name1,Name2,Name3,DOB,LastIPaddress,LastLoginDT,LastLogoutDT,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] AdminMembers adminMembers)
        {
            try
            {
                // Edit  세션체크
                if (!CheckSession())
                {
                    return Redirect("/AdminLogin/Login");
                }

                if (ModelState.IsValid)
                {
                    //암호화 안함
                    // 암호화 처리
                    //if (globalVal.CloudBreadCryptSetting == "AES256")
                    //{
                    //    EncryptResult(adminMembers);
                    //}

                    // Edit 입력값 자동처리
                    //adminMembers.IteamUpdateAdminID = this.Session["AdminID"].ToString();
                    adminMembers.UpdatedAt = DateTimeOffset.UtcNow;

                    db.Entry(adminMembers).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "AdminMembersController-Edit(adminMembers)";
                    logMessage.Message = JsonConvert.SerializeObject(adminMembers);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(adminMembers);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "AdminMembersController-Edit(AdminMembers)";
                logMessage.Message = JsonConvert.SerializeObject(adminMembers);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: AdminMembers/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                // Delete  세션체크
                if (!CheckSession())
                {
                    return Redirect("/AdminLogin/Login");
                }

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AdminMembers adminMembers = db.AdminMembers.Find(id);
                if (adminMembers == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "AdminMembersController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(adminMembers);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "AdminMembersController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: AdminMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // Delete  세션체크
                if (!CheckSession())
                {
                    return Redirect("/AdminLogin/Login");
                }

                AdminMembers adminMembers = db.AdminMembers.Find(id);
                db.AdminMembers.Remove(adminMembers);
                db.SaveChanges();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "AdminMembersController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "AdminMembersController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
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
