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
    public class GameEventMembersController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 복호화 수행
        public GameEventMember DecryptResult(GameEventMember item)
        {
            try
            {
                item.GameEventMemberID = Crypto.AES_decrypt(item.GameEventMemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.eventID = Crypto.AES_decrypt(item.eventID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.GameEvents.Title = Crypto.AES_decrypt(item.GameEvents.Title, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberID = Crypto.AES_decrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_decrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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
        public GameEventMember EncryptResult(GameEventMember item)
        {
            try
            {
                item.GameEventMemberID = Crypto.AES_encrypt(item.GameEventMemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.eventID = Crypto.AES_encrypt(item.eventID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.GameEvents.Title = Crypto.AES_encrypt(item.GameEvents.Title, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberID = Crypto.AES_encrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_encrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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


        // GET: GameEventMember
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

                var queryString = from gem in db.GameEventMember.ToList()
                    join ge in db.GameEvents on gem.eventID equals ge.GameEventID
                    join m in db.Members on gem.MemberID equals m.MemberID
                    select new GameEventMember()
                    {
                        GameEventMemberID = gem.GameEventMemberID,
                        eventID = gem.eventID,
                        MemberID = gem.MemberID,
                        sCol1 = gem.sCol1,
                        sCol2 = gem.sCol2,
                        sCol3 = gem.sCol3,
                        sCol4 = gem.sCol4,
                        sCol5 = gem.sCol5,
                        sCol6 = gem.sCol6,
                        sCol7 = gem.sCol7,
                        sCol8 = gem.sCol8,
                        sCol9 = gem.sCol9,
                        sCol10 = gem.sCol10,
                        HideYN = gem.HideYN,
                        DeleteYN = gem.DeleteYN,
                        CreatedAt = gem.CreatedAt,
                        UpdatedAt = gem.UpdatedAt,
                        DataFromRegion = gem.DataFromRegion,
                        DataFromRegionDT = gem.DataFromRegionDT,
                        GameEvents = new GameEvents()
                        {
                            Title = ge.Title
                        },
                        Members = new Members()
                        {
                            Name1 = m.Name1
                        }
                    };

                if (!String.IsNullOrEmpty(searchString))
                {
                    switch (SearchCondition)
                    {
                        case "GameEventMemberID":
                            queryString = queryString.Where(s => s.GameEventMemberID.Contains(searchString));
                            break;
                        case "eventID":
                            queryString = queryString.Where(s => s.eventID.Contains(searchString));
                            break;
                        case "MemberID":
                            queryString = queryString.Where(s => s.MemberID.Contains(searchString));
                            break;
                        default:
                            break;
                    }
                }

                // 기본 order 처리 - ItemName으로 정렬 처리 - ToPagedList의 제약 조건
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
                logMessage.Logger = "GameEventMemberController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);

            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventMemberController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: GameEventMember/Details/5
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
                var result = from gem in db.GameEventMember.ToList()
                             join ge in db.GameEvents on gem.eventID equals ge.GameEventID
                             join m in db.Members on gem.MemberID equals m.MemberID
                             where gem.GameEventMemberID == id        //id값

                    select new GameEventMember()
                    {
                        GameEventMemberID = gem.GameEventMemberID,
                        eventID = gem.eventID,
                        MemberID = gem.MemberID,
                        sCol1 = gem.sCol1,
                        sCol2 = gem.sCol2,
                        sCol3 = gem.sCol3,
                        sCol4 = gem.sCol4,
                        sCol5 = gem.sCol5,
                        sCol6 = gem.sCol6,
                        sCol7 = gem.sCol7,
                        sCol8 = gem.sCol8,
                        sCol9 = gem.sCol9,
                        sCol10 = gem.sCol10,
                        HideYN = gem.HideYN,
                        DeleteYN = gem.DeleteYN,
                        CreatedAt = gem.CreatedAt,
                        UpdatedAt = gem.UpdatedAt,
                        DataFromRegion = gem.DataFromRegion,
                        DataFromRegionDT = gem.DataFromRegionDT,
                        GameEvents = new GameEvents()
                        {
                            Title = ge.Title
                        },
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
                logMessage.Logger = "GameEventMemberController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventMemberController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: GameEventMember/Create
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
                logMessage.Logger = "GameEventMemberController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventMemberController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: GameEventMember/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameEventMemberID,eventID,MemberID,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] GameEventMember gameEventMember)
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
                    gameEventMember.CreatedAt = DateTimeOffset.UtcNow;
                    gameEventMember.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(gameEventMember);
                    }

                    db.GameEventMember.Add(gameEventMember);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "GameEventMemberController-Create(gameEventMember)";
                    logMessage.Message = JsonConvert.SerializeObject(gameEventMember);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(gameEventMember);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventMemberController-Create(gameEventMember)";
                logMessage.Message = JsonConvert.SerializeObject(gameEventMember);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: GameEventMember/Edit/5
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


                //GameEventMember gameEventMembers = db.GameEventMember.Find(id);

                //테이블 조인 처리
                var result = from gem in db.GameEventMember.ToList()
                             join ge in db.GameEvents on gem.eventID equals ge.GameEventID
                             join m in db.Members on gem.MemberID equals m.MemberID
                             where gem.GameEventMemberID == id        //id값

                             select new GameEventMember()
                             {
                                 GameEventMemberID = gem.GameEventMemberID,
                                 eventID = gem.eventID,
                                 MemberID = gem.MemberID,
                                 sCol1 = gem.sCol1,
                                 sCol2 = gem.sCol2,
                                 sCol3 = gem.sCol3,
                                 sCol4 = gem.sCol4,
                                 sCol5 = gem.sCol5,
                                 sCol6 = gem.sCol6,
                                 sCol7 = gem.sCol7,
                                 sCol8 = gem.sCol8,
                                 sCol9 = gem.sCol9,
                                 sCol10 = gem.sCol10,
                                 HideYN = gem.HideYN,
                                 DeleteYN = gem.DeleteYN,
                                 CreatedAt = gem.CreatedAt,
                                 UpdatedAt = gem.UpdatedAt,
                                 DataFromRegion = gem.DataFromRegion,
                                 DataFromRegionDT = gem.DataFromRegionDT,
                                 GameEvents = new GameEvents()
                                 {
                                     Title = ge.Title
                                 },
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
                logMessage.Logger = "GameEventMemberController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventMemberController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: GameEventMember/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameEventMemberID,eventID,MemberID,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] GameEventMember gameEventMember)
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
                    gameEventMember.UpdatedAt = DateTimeOffset.UtcNow;

                    // 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(gameEventMember);
                    }

                    db.Entry(gameEventMember).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "GameEventMemberController-Edit(gameEventMember)";
                    logMessage.Message = JsonConvert.SerializeObject(gameEventMember);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(gameEventMember);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventMemberController-Edit(gameEventMember)";
                logMessage.Message = JsonConvert.SerializeObject(gameEventMember);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: GameEventMember/Delete/5
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
                GameEventMember gameEventMember = db.GameEventMember.Find(id);
                if (gameEventMember == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "GameEventMemberController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(gameEventMember);

            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventMemberController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: GameEventMember/Delete/5
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

                GameEventMember gameEventMember = db.GameEventMember.Find(id);
                db.GameEventMember.Remove(gameEventMember);
                db.SaveChanges();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "GameEventMemberController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventMemberController-DeleteConfirm(id)";
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
