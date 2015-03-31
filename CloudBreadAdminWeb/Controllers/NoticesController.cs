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
    public class NoticesController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 복호화 수행
        public Notices DecryptResult(Notices item)
        {
            try
            {
                item.NoticeID = Crypto.AES_decrypt(item.NoticeID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.NoticeCategory1 = Crypto.AES_decrypt(item.NoticeCategory1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.NoticeCategory2 = Crypto.AES_decrypt(item.NoticeCategory2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.NoticeCategory3 = Crypto.AES_decrypt(item.NoticeCategory3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetGroup = Crypto.AES_decrypt(item.TargetGroup, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetOS = Crypto.AES_decrypt(item.TargetOS, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetDevice = Crypto.AES_decrypt(item.TargetDevice, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.NoticeImageLink = Crypto.AES_decrypt(item.NoticeImageLink, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.title = Crypto.AES_decrypt(item.title, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.content = Crypto.AES_decrypt(item.content, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol1 = Crypto.AES_decrypt(item.sCol1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol2 = Crypto.AES_decrypt(item.sCol2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol3 = Crypto.AES_decrypt(item.sCol3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol4 = Crypto.AES_decrypt(item.sCol4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol5 = Crypto.AES_decrypt(item.sCol5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol6 = Crypto.AES_decrypt(item.sCol6, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol7 = Crypto.AES_decrypt(item.sCol7, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol8 = Crypto.AES_decrypt(item.sCol8, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol9 = Crypto.AES_decrypt(item.sCol9, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol10 = Crypto.AES_decrypt(item.sCol10, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
            }
            catch (Exception)
            {

                throw;
            }

            return item;

        }

        //암호화 처리
        public Notices EncryptResult(Notices item)
        {
            try
            {
                item.NoticeID = Crypto.AES_encrypt(item.NoticeID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.NoticeCategory1 = Crypto.AES_encrypt(item.NoticeCategory1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.NoticeCategory2 = Crypto.AES_encrypt(item.NoticeCategory2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.NoticeCategory3 = Crypto.AES_encrypt(item.NoticeCategory3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetGroup = Crypto.AES_encrypt(item.TargetGroup, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetOS = Crypto.AES_encrypt(item.TargetOS, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetDevice = Crypto.AES_encrypt(item.TargetDevice, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.NoticeImageLink = Crypto.AES_encrypt(item.NoticeImageLink, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.title = Crypto.AES_encrypt(item.title, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.content = Crypto.AES_encrypt(item.content, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol1 = Crypto.AES_encrypt(item.sCol1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol2 = Crypto.AES_encrypt(item.sCol2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol3 = Crypto.AES_encrypt(item.sCol3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol4 = Crypto.AES_encrypt(item.sCol4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol5 = Crypto.AES_encrypt(item.sCol5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol6 = Crypto.AES_encrypt(item.sCol6, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol7 = Crypto.AES_encrypt(item.sCol7, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol8 = Crypto.AES_encrypt(item.sCol8, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol9 = Crypto.AES_encrypt(item.sCol9, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol10 = Crypto.AES_encrypt(item.sCol10, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
            }
            catch (Exception)
            {

                throw;
            }

            return item;

        }

        //세션체크 Admin 또는 Operator 여부 체크
        public bool CheckSession()
        {
            try
            {
                string strSession = (this.Session["AdminGroup"] ?? "").ToString();
                if (strSession != "Admin" && strSession != "Operator")
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

        // GET: Notices
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

                //암호화일 경우 searchString도 암호화해 검색 해야 한다.
                if (globalVal.CloudBreadCryptSetting == "AES256" && searchString != null)
                {
                    searchString = Crypto.AES_encrypt(searchString, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    ViewBag.CurrentFilter = searchString;
                }
                else
                {
                    ViewBag.CurrentFilter = searchString;
                }

                ViewBag.SearchCondition = SearchCondition;

                var queryString = from n in db.Notices.ToList()
                    select new Notices()
                    {
                        NoticeID = n.NoticeID,
                        NoticeCategory1 = n.NoticeCategory1,
                        NoticeCategory2 = n.NoticeCategory2,
                        NoticeCategory3 = n.NoticeCategory3,
                        TargetGroup = n.TargetGroup,
                        TargetOS = n.TargetOS,
                        TargetDevice = n.TargetDevice,
                        NoticeImageLink = n.NoticeImageLink,
                        title = n.title,
                        content = n.content,
                        sCol1 = n.sCol1,
                        sCol2 = n.sCol2,
                        sCol3 = n.sCol3,
                        sCol4 = n.sCol4,
                        sCol5 = n.sCol5,
                        sCol6 = n.sCol6,
                        sCol7 = n.sCol7,
                        sCol8 = n.sCol8,
                        sCol9 = n.sCol9,
                        sCol10 = n.sCol10,
                        NoticeDurationFrom = n.NoticeDurationFrom,
                        NoticeDurationTo = n.NoticeDurationTo,
                        OrderNumber = n.OrderNumber,
                        CreateAdminID = n.CreateAdminID,
                        HideYN = n.HideYN,
                        DeleteYN = n.DeleteYN,
                        CreatedAt = n.CreatedAt,
                        UpdatedAt = n.UpdatedAt,
                        DataFromRegion = n.DataFromRegion,
                        DataFromRegionDT = n.DataFromRegionDT
                    };

                if (!String.IsNullOrEmpty(searchString))
                {
                    switch (SearchCondition)
                    {
                        case "NoticeID":
                            queryString = queryString.Where(s => s.NoticeID.Contains(searchString));
                            break;
                        case "NoticeCategory1":
                            queryString = queryString.Where(s => s.NoticeCategory1.Contains(searchString));
                            break;
                        case "TargetGroup":
                            queryString = queryString.Where(s => s.TargetGroup.Contains(searchString));
                            break;
                        case "Title":
                            queryString = queryString.Where(s => s.title.Contains(searchString));
                            break;
                        default:
                            break;
                    }
                }

                // 기본 order 처리 - ToPagedList의 제약 조건
                queryString = queryString.OrderByDescending(s => s.CreatedAt);
                int pageSize = globalVal.CloudBreadAdminWebListPageSize;
                int pageNumber = (page ?? 1);
                var result = queryString.ToPagedList(pageNumber, pageSize);

                if (globalVal.CloudBreadCryptSetting == "AES256")
                {
                    // 복호화
                    foreach (var item in result)
                    {
                        DecryptResult(item);
                    }
                }

                //날자 데이터 현지 시각으로 변환
                foreach (var item in result)
                {
                    item.NoticeDurationFrom = UserTime.GetUserTime(item.NoticeDurationFrom.DateTime, Session["AdminTimeZone"].ToString());
                    item.NoticeDurationTo = UserTime.GetUserTime(item.NoticeDurationTo.DateTime, Session["AdminTimeZone"].ToString());
                    item.CreatedAt = UserTime.GetUserTime(item.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                    item.UpdatedAt = UserTime.GetUserTime(item.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "NoticesController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);

            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "NoticesController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: Notices/Details/5
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

                //테이블 조인 처리
                var result =
                    from n in db.Notices.ToList()
                    where n.NoticeID == id        //id값

                    select new Notices()
                    {
                        NoticeID = n.NoticeID,
                        NoticeCategory1 = n.NoticeCategory1,
                        NoticeCategory2 = n.NoticeCategory2,
                        NoticeCategory3 = n.NoticeCategory3,
                        TargetGroup = n.TargetGroup,
                        TargetOS = n.TargetOS,
                        TargetDevice = n.TargetDevice,
                        NoticeImageLink = n.NoticeImageLink,
                        title = n.title,
                        content = n.content,
                        sCol1 = n.sCol1,
                        sCol2 = n.sCol2,
                        sCol3 = n.sCol3,
                        sCol4 = n.sCol4,
                        sCol5 = n.sCol5,
                        sCol6 = n.sCol6,
                        sCol7 = n.sCol7,
                        sCol8 = n.sCol8,
                        sCol9 = n.sCol9,
                        sCol10 = n.sCol10,
                        NoticeDurationFrom = n.NoticeDurationFrom,
                        NoticeDurationTo = n.NoticeDurationTo,
                        OrderNumber = n.OrderNumber,
                        CreateAdminID = n.CreateAdminID,
                        HideYN = n.HideYN,
                        DeleteYN = n.DeleteYN,
                        CreatedAt = n.CreatedAt,
                        UpdatedAt = n.UpdatedAt,
                        DataFromRegion = n.DataFromRegion,
                        DataFromRegionDT = n.DataFromRegionDT
                    };

                if (result == null)
                {
                    return HttpNotFound();
                }

                // FirstOrDefault result1로 재처리
                var result1 = result.FirstOrDefault();

                //복호화 처리
                if (globalVal.CloudBreadCryptSetting == "AES256")
                {
                    DecryptResult(result1);
                }

                //UTC를 세션의 UserTimeZone으로 변환 - 안하면 UTC 그대로 보임
                result1.NoticeDurationFrom = UserTime.GetUserTime(result1.NoticeDurationFrom.DateTime, Session["AdminTimeZone"].ToString());
                result1.NoticeDurationTo = UserTime.GetUserTime(result1.NoticeDurationTo.DateTime, Session["AdminTimeZone"].ToString());
                result1.CreatedAt = UserTime.GetUserTime(result1.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                result1.UpdatedAt = UserTime.GetUserTime(result1.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                result1.DataFromRegionDT = UserTime.GetUserTime(result1.DataFromRegionDT.DateTime, Session["AdminTimeZone"].ToString());

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "NoticesController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "NoticesController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: Notices/Create
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
                logMessage.Logger = "NoticesController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "NoticesController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: Notices/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NoticeID,NoticeCategory1,NoticeCategory2,NoticeCategory3,TargetGroup,TargetOS,TargetDevice,NoticeImageLink,title,content,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,NoticeDurationFrom,NoticeDurationTo,OrderNumber,CreateAdminID,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] Notices notices)
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
                    // 입력값 자동 처리 
                    notices.CreateAdminID = this.Session["AdminID"].ToString();
                    notices.CreatedAt = DateTimeOffset.UtcNow;
                    notices.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(notices);
                    }

                    db.Notices.Add(notices);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "NoticesController-Create(notices)";
                    logMessage.Message = JsonConvert.SerializeObject(notices);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(notices);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "NoticesController-Create(notices)";
                logMessage.Message = JsonConvert.SerializeObject(notices);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: Notices/Edit/5
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


                //Notices notices = db.Notices.Find(id);

                //테이블 조인 처리
                var result =
                    from n in db.Notices.ToList()
                    where n.NoticeID == id        //id값

                    select new Notices()
                    {
                        NoticeID = n.NoticeID,
                        NoticeCategory1 = n.NoticeCategory1,
                        NoticeCategory2 = n.NoticeCategory2,
                        NoticeCategory3 = n.NoticeCategory3,
                        TargetGroup = n.TargetGroup,
                        TargetOS = n.TargetOS,
                        TargetDevice = n.TargetDevice,
                        NoticeImageLink = n.NoticeImageLink,
                        title = n.title,
                        content = n.content,
                        sCol1 = n.sCol1,
                        sCol2 = n.sCol2,
                        sCol3 = n.sCol3,
                        sCol4 = n.sCol4,
                        sCol5 = n.sCol5,
                        sCol6 = n.sCol6,
                        sCol7 = n.sCol7,
                        sCol8 = n.sCol8,
                        sCol9 = n.sCol9,
                        sCol10 = n.sCol10,
                        NoticeDurationFrom = n.NoticeDurationFrom,
                        NoticeDurationTo = n.NoticeDurationTo,
                        OrderNumber = n.OrderNumber,
                        CreateAdminID = n.CreateAdminID,
                        HideYN = n.HideYN,
                        DeleteYN = n.DeleteYN,
                        CreatedAt = n.CreatedAt,
                        UpdatedAt = n.UpdatedAt,
                        DataFromRegion = n.DataFromRegion,
                        DataFromRegionDT = n.DataFromRegionDT
                    };

                if (result == null)
                {
                    return HttpNotFound();
                }

                // FirstOrDefault result1로 재처리
                var result1 = result.FirstOrDefault();

                //복호화 처리
                if (globalVal.CloudBreadCryptSetting == "AES256")
                {
                    DecryptResult(result1);
                }

                //UTC를 세션의 UserTimeZone으로 변환 - 안하면 UTC 그대로 보임
                result1.NoticeDurationFrom = UserTime.GetUserTime(result1.NoticeDurationFrom.DateTime, Session["AdminTimeZone"].ToString());
                result1.NoticeDurationTo = UserTime.GetUserTime(result1.NoticeDurationTo.DateTime, Session["AdminTimeZone"].ToString());

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "NoticesController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "NoticesController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: Notices/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NoticeID,NoticeCategory1,NoticeCategory2,NoticeCategory3,TargetGroup,TargetOS,TargetDevice,NoticeImageLink,title,content,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,NoticeDurationFrom,NoticeDurationTo,OrderNumber,CreateAdminID,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] Notices notices)
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
                    // Edit 입력값 자동처리
                    notices.NoticeDurationFrom = UserTime.SetUtcTime(notices.NoticeDurationFrom.DateTime, Session["AdminTimeZone"].ToString());
                    notices.NoticeDurationTo = UserTime.SetUtcTime(notices.NoticeDurationTo.DateTime, Session["AdminTimeZone"].ToString());

                    notices.UpdatedAt = DateTimeOffset.UtcNow;

                    // 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(notices);
                    }

                    db.Entry(notices).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "NoticesController-Edit(notices)";
                    logMessage.Message = JsonConvert.SerializeObject(notices);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(notices);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "NoticesController-Edit(notices)";
                logMessage.Message = JsonConvert.SerializeObject(notices);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: Notices/Delete/5
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
                Notices notices = db.Notices.Find(id);
                if (notices == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "NoticesController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(notices);

            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "NoticesController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);
                throw;
            }
            
        }

        // POST: Notices/Delete/5
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

                Notices notices = db.Notices.Find(id);
                db.Notices.Remove(notices);
                db.SaveChanges();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "NoticesController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "NoticesController-DeleteConfirm(id)";
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
