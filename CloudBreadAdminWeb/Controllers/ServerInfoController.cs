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
    public class ServerInfoController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 복호화 수행
        public ServerInfo DecryptResult(ServerInfo item)
        {
            try
            {
                item.InfoID = Crypto.AES_decrypt(item.InfoID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.FEServerLists = Crypto.AES_decrypt(item.FEServerLists, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.SocketServerLists = Crypto.AES_decrypt(item.SocketServerLists, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Version = Crypto.AES_decrypt(item.Version, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ResourceLink = Crypto.AES_decrypt(item.ResourceLink, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EULAText = Crypto.AES_decrypt(item.EULAText, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol1 = Crypto.AES_decrypt(item.sCol1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol2 = Crypto.AES_decrypt(item.sCol2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol3 = Crypto.AES_decrypt(item.sCol3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol4 = Crypto.AES_decrypt(item.sCol4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol5 = Crypto.AES_decrypt(item.sCol5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);

            }
            catch (Exception)
            {

                throw;
            }

            return item;

        }

        //암호화 처리
        public ServerInfo EncryptResult(ServerInfo item)
        {
            try
            {
                item.InfoID = Crypto.AES_encrypt(item.InfoID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.FEServerLists = Crypto.AES_encrypt(item.FEServerLists, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.SocketServerLists = Crypto.AES_encrypt(item.SocketServerLists, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Version = Crypto.AES_encrypt(item.Version, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ResourceLink = Crypto.AES_encrypt(item.ResourceLink, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EULAText = Crypto.AES_encrypt(item.EULAText, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol1 = Crypto.AES_encrypt(item.sCol1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol2 = Crypto.AES_encrypt(item.sCol2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol3 = Crypto.AES_encrypt(item.sCol3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol4 = Crypto.AES_encrypt(item.sCol4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.sCol5 = Crypto.AES_encrypt(item.sCol5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);

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


        // GET: ServerInfo
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

                var queryString = from si in db.ServerInfo.ToList()
                    select new ServerInfo()
                    {
                        InfoID = si.InfoID,
                        FEServerLists = si.FEServerLists,
                        SocketServerLists = si.SocketServerLists,
                        Version = si.Version,
                        ResourceLink = si.ResourceLink,
                        EULAText = si.EULAText,
                        sCol1 = si.sCol1,
                        sCol2 = si.sCol2,
                        sCol3 = si.sCol3,
                        sCol4 = si.sCol4,
                        sCol5 = si.sCol5,
                        CreatedAt = si.CreatedAt,
                        UpdatedAt = si.UpdatedAt,
                        DataFromRegion = si.DataFromRegion,
                        DataFromRegionDT = si.DataFromRegionDT
                     };

                if (!String.IsNullOrEmpty(searchString))
                {
                    switch (SearchCondition)
                    {
                        case "InfoID":
                            queryString = queryString.Where(s => s.InfoID.Contains(searchString));
                            break;
                        case "FEServerLists":
                            queryString = queryString.Where(s => s.FEServerLists.Contains(searchString));
                            break;
                        case "SocketServerLists":
                            queryString = queryString.Where(s => s.SocketServerLists.Contains(searchString));
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
                logMessage.Logger = "ServerInfoController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);

            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ServerInfoController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: ServerInfo/Details/5
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
                    from si in db.ServerInfo.ToList()
                    
                    where si.InfoID == id        //id값

                    select new ServerInfo()
                    {
                        InfoID = si.InfoID,
                        FEServerLists = si.FEServerLists,
                        SocketServerLists = si.SocketServerLists,
                        Version = si.Version,
                        ResourceLink = si.ResourceLink,
                        EULAText = si.EULAText,
                        sCol1 = si.sCol1,
                        sCol2 = si.sCol2,
                        sCol3 = si.sCol3,
                        sCol4 = si.sCol4,
                        sCol5 = si.sCol5,
                        CreatedAt = si.CreatedAt,
                        UpdatedAt = si.UpdatedAt,
                        DataFromRegion = si.DataFromRegion,
                        DataFromRegionDT = si.DataFromRegionDT
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
                logMessage.Logger = "ServerInfoController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ServerInfoController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: ServerInfo/Create
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
                logMessage.Logger = "ServerInfoController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ServerInfoController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: ServerInfo/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InfoID,FEServerLists, SocketServerLists,Version,ResourceLink,EULAText,sCol1, sCol2, sCol3, sCol4, sCol5,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] ServerInfo serverInfo)
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
                    serverInfo.CreatedAt = DateTimeOffset.UtcNow;
                    serverInfo.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(serverInfo);
                    }

                    db.ServerInfo.Add(serverInfo);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "ServerInfoController-Create(serverInfo)";
                    logMessage.Message = JsonConvert.SerializeObject(serverInfo);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(serverInfo);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ServerInfoController-Create(serverInfo)";
                logMessage.Message = JsonConvert.SerializeObject(serverInfo);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: ServerInfo/Edit/5
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


                //ServerInfo serverInfo = db.ServerInfo.Find(id);

                //테이블 조인 처리
                var result =
                    from si in db.ServerInfo.ToList()
                    where si.InfoID == id        //id값

                    select new ServerInfo()
                    {
                        InfoID = si.InfoID,
                        FEServerLists = si.FEServerLists,
                        SocketServerLists = si.SocketServerLists,
                        Version = si.Version,
                        ResourceLink = si.ResourceLink,
                        EULAText = si.EULAText,
                        sCol1 = si.sCol1,
                        sCol2 = si.sCol2,
                        sCol3 = si.sCol3,
                        sCol4 = si.sCol4,
                        sCol5 = si.sCol5,
                        CreatedAt = si.CreatedAt,
                        UpdatedAt = si.UpdatedAt,
                        DataFromRegion = si.DataFromRegion,
                        DataFromRegionDT = si.DataFromRegionDT
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
                logMessage.Logger = "ServerInfoController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ServerInfoController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: ServerInfo/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InfoID,FEServerLists, SocketServerLists,Version,ResourceLink,EULAText,sCol1, sCol2, sCol3, sCol4, sCol5,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] ServerInfo serverInfo)
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
                    serverInfo.UpdatedAt = DateTimeOffset.UtcNow;

                    // 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(serverInfo);
                    }

                    db.Entry(serverInfo).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "ServerInfoController-Edit(serverInfo)";
                    logMessage.Message = JsonConvert.SerializeObject(serverInfo);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(serverInfo);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ServerInfoController-Edit(serverInfo)";
                logMessage.Message = JsonConvert.SerializeObject(serverInfo);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: ServerInfo/Delete/5
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
                ServerInfo serverInfo = db.ServerInfo.Find(id);
                if (serverInfo == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "ServerInfoController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(serverInfo);
            }
            catch (Exception ex)
            {
                 //에러로그
                 logMessage.memberID = this.Session["AdminID"].ToString();
                 logMessage.Level = "ERROR";
                 logMessage.Logger = "ServerInfoController-Delete(id)";
                 logMessage.Message = string.Format("id : {0}", id);
                 logMessage.Exception = ex.ToString();
                 Logging.RunLog(logMessage);

                 throw;
            }

        }

        // POST: ServerInfo/Delete/5
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

                ServerInfo serverInfo = db.ServerInfo.Find(id);
                db.ServerInfo.Remove(serverInfo);

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "ServerInfoController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ServerInfoController-DeleteConfirm(id)";
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
