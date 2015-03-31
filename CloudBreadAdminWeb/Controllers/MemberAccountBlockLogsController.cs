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
    public class MemberAccountBlockLogsController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 복호화 수행
        public MemberAccountBlockLog DecryptResult(MemberAccountBlockLog item)
        {
            try
            {
                item.MemberAccountBlockID = Crypto.AES_decrypt(item.MemberAccountBlockID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberID = Crypto.AES_decrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberAccountBlockReasonCategory1 = Crypto.AES_decrypt(item.MemberAccountBlockReasonCategory1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberAccountBlockReasonCategory2 = Crypto.AES_decrypt(item.MemberAccountBlockReasonCategory2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberAccountBlockReasonCategory3 = Crypto.AES_decrypt(item.MemberAccountBlockReasonCategory3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberAccountBlockReason = Crypto.AES_decrypt(item.MemberAccountBlockReason, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberAccountBlockProcess = Crypto.AES_decrypt(item.MemberAccountBlockProcess, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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
        public MemberAccountBlockLog EncryptResult(MemberAccountBlockLog item)
        {
            try
            {
                item.MemberAccountBlockID = Crypto.AES_encrypt(item.MemberAccountBlockID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberID = Crypto.AES_encrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberAccountBlockReasonCategory1 = Crypto.AES_encrypt(item.MemberAccountBlockReasonCategory1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberAccountBlockReasonCategory2 = Crypto.AES_encrypt(item.MemberAccountBlockReasonCategory2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberAccountBlockReasonCategory3 = Crypto.AES_encrypt(item.MemberAccountBlockReasonCategory3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberAccountBlockReason = Crypto.AES_encrypt(item.MemberAccountBlockReason, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberAccountBlockProcess = Crypto.AES_encrypt(item.MemberAccountBlockProcess, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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


        // GET: MemberAccountBlockLogs
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

                var queryString = from mab in db.MemberAccountBlockLog.ToList()
                    join m in db.Members on mab.MemberID equals m.MemberID
                    select new MemberAccountBlockLog()
                    {
                        MemberAccountBlockID = mab.MemberAccountBlockID,
                        MemberID = mab.MemberID,
                        MemberAccountBlockReasonCategory1 = mab.MemberAccountBlockReasonCategory1,
                        MemberAccountBlockReasonCategory2 = mab.MemberAccountBlockReasonCategory2,
                        MemberAccountBlockReasonCategory3 = mab.MemberAccountBlockReasonCategory3,
                        MemberAccountBlockReason = mab.MemberAccountBlockReason,
                        MemberAccountBlockProcess = mab.MemberAccountBlockProcess,
                        sCol1 = mab.sCol1,
                        sCol2 = mab.sCol2,
                        sCol3 = mab.sCol3,
                        sCol4 = mab.sCol4,
                        sCol5 = mab.sCol5,
                        sCol6 = mab.sCol6,
                        sCol7 = mab.sCol7,
                        sCol8 = mab.sCol8,
                        sCol9 = mab.sCol9,
                        sCol10 = mab.sCol10,
                        CreateAdminID = mab.CreateAdminID,
                        HideYN = mab.HideYN,
                        DeleteYN = mab.DeleteYN,
                        CreatedAt = mab.CreatedAt,
                        UpdatedAt = mab.UpdatedAt,
                        DataFromRegion = mab.DataFromRegion,
                        DataFromRegionDT = mab.DataFromRegionDT,
                        Members = new Members()
                        {
                            Name1 = m.Name1
                        }
                    };

                if (!String.IsNullOrEmpty(searchString))
                {
                    switch (SearchCondition)
                    {
                        case "MemberAccountBlockID ":
                            queryString = queryString.Where(s => s.MemberAccountBlockID.Contains(searchString));
                            break;
                        case "MemberID ":
                            queryString = queryString.Where(s => s.MemberID.Contains(searchString));
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
                    item.CreatedAt = UserTime.GetUserTime(item.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                    item.UpdatedAt = UserTime.GetUserTime(item.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberAccountBlockLogController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);

            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberAccountBlockLogController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberAccountBlockLogs/Details/5
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
                    from mab in db.MemberAccountBlockLog.ToList()
                    join m in db.Members on mab.MemberID equals m.MemberID
                    where mab.MemberAccountBlockID == id        //id값

                    select new MemberAccountBlockLog()
                    {
                        MemberAccountBlockID = mab.MemberAccountBlockID,
                        MemberID = mab.MemberID,
                        MemberAccountBlockReasonCategory1 = mab.MemberAccountBlockReasonCategory1,
                        MemberAccountBlockReasonCategory2 = mab.MemberAccountBlockReasonCategory2,
                        MemberAccountBlockReasonCategory3 = mab.MemberAccountBlockReasonCategory3,
                        MemberAccountBlockReason = mab.MemberAccountBlockReason,
                        MemberAccountBlockProcess = mab.MemberAccountBlockProcess,
                        sCol1 = mab.sCol1,
                        sCol2 = mab.sCol2,
                        sCol3 = mab.sCol3,
                        sCol4 = mab.sCol4,
                        sCol5 = mab.sCol5,
                        sCol6 = mab.sCol6,
                        sCol7 = mab.sCol7,
                        sCol8 = mab.sCol8,
                        sCol9 = mab.sCol9,
                        sCol10 = mab.sCol10,
                        CreateAdminID = mab.CreateAdminID,
                        HideYN = mab.HideYN,
                        DeleteYN = mab.DeleteYN,
                        CreatedAt = mab.CreatedAt,
                        UpdatedAt = mab.UpdatedAt,
                        DataFromRegion = mab.DataFromRegion,
                        DataFromRegionDT = mab.DataFromRegionDT,
                        Members = new Members()
                        {
                            Name1 = m.Name1
                        }
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
                result1.CreatedAt = UserTime.GetUserTime(result1.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                result1.UpdatedAt = UserTime.GetUserTime(result1.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                result1.DataFromRegionDT = UserTime.GetUserTime(result1.DataFromRegionDT.DateTime, Session["AdminTimeZone"].ToString());

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberAccountBlockLogController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberAccountBlockLogController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberAccountBlockLogs/Create
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
                logMessage.Logger = "MemberAccountBlockLogController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberAccountBlockLogController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: MemberAccountBlockLogs/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberAccountBlockID,MemberID,MemberAccountBlockReasonCategory1,MemberAccountBlockReasonCategory2,MemberAccountBlockReasonCategory3,MemberAccountBlockReason,MemberAccountBlockProcess,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,CreateAdminID,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] MemberAccountBlockLog memberAccountBlockLog)
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
                    memberAccountBlockLog.CreateAdminID = this.Session["AdminID"].ToString();
                    memberAccountBlockLog.CreatedAt = DateTimeOffset.UtcNow;
                    memberAccountBlockLog.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(memberAccountBlockLog);
                    }

                    db.MemberAccountBlockLog.Add(memberAccountBlockLog);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MemberAccountBlockLogController-Create(memberAccountBlockLog)";
                    logMessage.Message = JsonConvert.SerializeObject(memberAccountBlockLog);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(memberAccountBlockLog);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberAccountBlockLogController-Create(memberAccountBlockLog)";
                logMessage.Message = JsonConvert.SerializeObject(memberAccountBlockLog);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberAccountBlockLogs/Edit/5
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


                //MemberAccountBlockLog memberAccountBlockLog = db.MemberAccountBlockLog.Find(id);

                //테이블 조인 처리
                var result =
                    from mab in db.MemberAccountBlockLog.ToList()
                    join m in db.Members on mab.MemberID equals m.MemberID
                    where mab.MemberAccountBlockID == id        //id값

                    select new MemberAccountBlockLog()
                    {
                        MemberAccountBlockID = mab.MemberAccountBlockID,
                        MemberID = mab.MemberID,
                        MemberAccountBlockReasonCategory1 = mab.MemberAccountBlockReasonCategory1,
                        MemberAccountBlockReasonCategory2 = mab.MemberAccountBlockReasonCategory2,
                        MemberAccountBlockReasonCategory3 = mab.MemberAccountBlockReasonCategory3,
                        MemberAccountBlockReason = mab.MemberAccountBlockReason,
                        MemberAccountBlockProcess = mab.MemberAccountBlockProcess,
                        sCol1 = mab.sCol1,
                        sCol2 = mab.sCol2,
                        sCol3 = mab.sCol3,
                        sCol4 = mab.sCol4,
                        sCol5 = mab.sCol5,
                        sCol6 = mab.sCol6,
                        sCol7 = mab.sCol7,
                        sCol8 = mab.sCol8,
                        sCol9 = mab.sCol9,
                        sCol10 = mab.sCol10,
                        CreateAdminID = mab.CreateAdminID,
                        HideYN = mab.HideYN,
                        DeleteYN = mab.DeleteYN,
                        CreatedAt = mab.CreatedAt,
                        UpdatedAt = mab.UpdatedAt,
                        DataFromRegion = mab.DataFromRegion,
                        DataFromRegionDT = mab.DataFromRegionDT,
                        Members = new Members()
                        {
                            Name1 = m.Name1
                        }
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

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberAccountBlockLogController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberAccountBlockLogController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: MemberAccountBlockLogs/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberAccountBlockID,MemberID,MemberAccountBlockReasonCategory1,MemberAccountBlockReasonCategory2,MemberAccountBlockReasonCategory3,MemberAccountBlockReason,MemberAccountBlockProcess,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,CreateAdminID,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] MemberAccountBlockLog memberAccountBlockLog)
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
                    memberAccountBlockLog.UpdatedAt = DateTimeOffset.UtcNow;

                    // 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(memberAccountBlockLog);
                    }

                    db.Entry(memberAccountBlockLog).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MemberAccountBlockLogController-Edit(memberAccountBlockLog)";
                    logMessage.Message = JsonConvert.SerializeObject(memberAccountBlockLog);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(memberAccountBlockLog);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberAccountBlockLogController-Edit(memberAccountBlockLog)";
                logMessage.Message = JsonConvert.SerializeObject(memberAccountBlockLog);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberAccountBlockLogs/Delete/5
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
                MemberAccountBlockLog memberAccountBlockLog = db.MemberAccountBlockLog.Find(id);
                if (memberAccountBlockLog == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberAccountBlockLogsController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(memberAccountBlockLog);

            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberAccountBlockLogsController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: MemberAccountBlockLogs/Delete/5
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

                MemberAccountBlockLog memberAccountBlockLog = db.MemberAccountBlockLog.Find(id);
                db.MemberAccountBlockLog.Remove(memberAccountBlockLog);
                db.SaveChanges();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberAccountBlockLogsController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberAccountBlockLogsController-DeleteConfirm(id)";
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
