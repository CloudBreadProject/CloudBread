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
    public class GameEventsController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 복호화 수행
        public GameEvents DecryptResult(GameEvents item)
        {
            try
            {
                item.GameEventID = Crypto.AES_decrypt(item.GameEventID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EventCategory1 = Crypto.AES_decrypt(item.EventCategory1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EventCategory2 = Crypto.AES_decrypt(item.EventCategory2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EventCategory3 = Crypto.AES_decrypt(item.EventCategory3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemListID = Crypto.AES_decrypt(item.ItemListID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCount = Crypto.AES_decrypt(item.ItemCount, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Itemstatus = Crypto.AES_decrypt(item.Itemstatus, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetGroup = Crypto.AES_decrypt(item.TargetGroup, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetOS = Crypto.AES_decrypt(item.TargetOS, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetDevice = Crypto.AES_decrypt(item.TargetDevice, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EventImageLink = Crypto.AES_decrypt(item.EventImageLink, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Title = Crypto.AES_decrypt(item.Title, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Content = Crypto.AES_decrypt(item.Content, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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
        public GameEvents EncryptResult(GameEvents item)
        {
            try
            {
                item.GameEventID = Crypto.AES_encrypt(item.GameEventID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EventCategory1 = Crypto.AES_encrypt(item.EventCategory1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EventCategory2 = Crypto.AES_encrypt(item.EventCategory2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EventCategory3 = Crypto.AES_encrypt(item.EventCategory3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemListID = Crypto.AES_encrypt(item.ItemListID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCount = Crypto.AES_encrypt(item.ItemCount, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Itemstatus = Crypto.AES_encrypt(item.Itemstatus, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetGroup = Crypto.AES_encrypt(item.TargetGroup, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetOS = Crypto.AES_encrypt(item.TargetOS, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.TargetDevice = Crypto.AES_encrypt(item.TargetDevice, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EventImageLink = Crypto.AES_encrypt(item.EventImageLink, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Title = Crypto.AES_encrypt(item.Title, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Content = Crypto.AES_encrypt(item.Content, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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

        // GET: GameEvents
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

                var queryString = from ge in db.GameEvents.ToList()
                    join il in db.ItemLists on ge.ItemListID equals il.ItemListID
                    select new GameEvents()
                    {
                        GameEventID = ge.GameEventID,
                        EventCategory1 = ge.EventCategory1,
                        EventCategory2 = ge.EventCategory2,
                        EventCategory3 = ge.EventCategory3,
                        ItemListID = ge.ItemListID,
                        ItemCount = ge.ItemCount,
                        Itemstatus = ge.Itemstatus,
                        TargetGroup = ge.TargetGroup,
                        TargetOS = ge.TargetOS,
                        TargetDevice = ge.TargetDevice,
                        EventImageLink = ge.EventImageLink,
                        Title = ge.Title,
                        Content = ge.Content,
                        sCol1 = ge.sCol1,
                        sCol2 = ge.sCol2,
                        sCol3 = ge.sCol3,
                        sCol4 = ge.sCol4,
                        sCol5 = ge.sCol5,
                        sCol6 = ge.sCol6,
                        sCol7 = ge.sCol7,
                        sCol8 = ge.sCol8,
                        sCol9 = ge.sCol9,
                        sCol10 = ge.sCol10,
                        CreateAdminID = ge.CreateAdminID,
                        HideYN = ge.HideYN,
                        DeleteYN = ge.DeleteYN,
                        OrderNumber = ge.OrderNumber,
                        EventDurationFrom = ge.EventDurationFrom,
                        EventDurationTo = ge.EventDurationTo,
                        CreatedAt = ge.CreatedAt,
                        UpdatedAt = ge.UpdatedAt,
                        DataFromRegion = ge.DataFromRegion,
                        DataFromRegionDT = ge.DataFromRegionDT,

                        ItemLists = new ItemLists()
                        {
                            ItemName = il.ItemName
                        }
                    };

                if (!String.IsNullOrEmpty(searchString))
                {
                    switch (SearchCondition)
                    {
                        case "GameEventID":
                            queryString = queryString.Where(s => s.GameEventID.Contains(searchString));
                            break;
                        case "EventCategory1":
                            queryString = queryString.Where(s => s.EventCategory1.Contains(searchString));
                            break;
                        case "ItemListID":
                            queryString = queryString.Where(s => s.ItemListID.Contains(searchString));
                            break;
                        case "Title":
                            queryString = queryString.Where(s => s.Title.Contains(searchString));
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
                    item.EventDurationFrom = UserTime.GetUserTime(item.EventDurationFrom.DateTime, Session["AdminTimeZone"].ToString());
                    item.EventDurationTo = UserTime.GetUserTime(item.EventDurationTo.DateTime, Session["AdminTimeZone"].ToString());
                    item.CreatedAt = UserTime.GetUserTime(item.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                    item.UpdatedAt = UserTime.GetUserTime(item.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "GameEventsController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);

            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventsController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: GameEvents/Details/5
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
                    from ge in db.GameEvents.ToList()
                    join il in db.ItemLists on ge.ItemListID equals il.ItemListID
                    where ge.GameEventID == id        //id값

                    select new GameEvents()
                    {
                        GameEventID = ge.GameEventID,
                        EventCategory1 = ge.EventCategory1,
                        EventCategory2 = ge.EventCategory2,
                        EventCategory3 = ge.EventCategory3,
                        ItemListID = ge.ItemListID,
                        ItemCount = ge.ItemCount,
                        Itemstatus = ge.Itemstatus,
                        TargetGroup = ge.TargetGroup,
                        TargetOS = ge.TargetOS,
                        TargetDevice = ge.TargetDevice,
                        EventImageLink = ge.EventImageLink,
                        Title = ge.Title,
                        Content = ge.Content,
                        sCol1 = ge.sCol1,
                        sCol2 = ge.sCol2,
                        sCol3 = ge.sCol3,
                        sCol4 = ge.sCol4,
                        sCol5 = ge.sCol5,
                        sCol6 = ge.sCol6,
                        sCol7 = ge.sCol7,
                        sCol8 = ge.sCol8,
                        sCol9 = ge.sCol9,
                        sCol10 = ge.sCol10,
                        CreateAdminID = ge.CreateAdminID,
                        HideYN = ge.HideYN,
                        DeleteYN = ge.DeleteYN,
                        OrderNumber = ge.OrderNumber,
                        EventDurationFrom = ge.EventDurationFrom,
                        EventDurationTo = ge.EventDurationTo,
                        CreatedAt = ge.CreatedAt,
                        UpdatedAt = ge.UpdatedAt,
                        DataFromRegion = ge.DataFromRegion,
                        DataFromRegionDT = ge.DataFromRegionDT,

                        ItemLists = new ItemLists()
                        {
                            ItemName = il.ItemName
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
                result1.EventDurationFrom = UserTime.GetUserTime(result1.EventDurationFrom.DateTime, Session["AdminTimeZone"].ToString());
                result1.EventDurationTo = UserTime.GetUserTime(result1.EventDurationTo.DateTime, Session["AdminTimeZone"].ToString());
                result1.CreatedAt = UserTime.GetUserTime(result1.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                result1.UpdatedAt = UserTime.GetUserTime(result1.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                result1.DataFromRegionDT = UserTime.GetUserTime(result1.DataFromRegionDT.DateTime, Session["AdminTimeZone"].ToString());

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "GameEventsController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventsController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: GameEvents/Create
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
                logMessage.Logger = "GameEventsController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventsController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: GameEvents/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameEventID,EventCategory1,EventCategory2,EventCategory3,ItemListID,ItemCount,Itemstatus,TargetGroup,TargetOS,TargetDevice,EventImageLink,Title,Content,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,EventDurationFrom,EventDurationTo,OrderNumber,CreateAdminID,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] GameEvents gameEvents)
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
                    gameEvents.CreateAdminID = this.Session["AdminID"].ToString();
                    gameEvents.CreatedAt = DateTimeOffset.UtcNow;
                    gameEvents.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(gameEvents);
                    }

                    db.GameEvents.Add(gameEvents);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "GameEventsController-Create(gameEvents)";
                    logMessage.Message = JsonConvert.SerializeObject(gameEvents);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(gameEvents);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventsController-Create(gameEvents)";
                logMessage.Message = JsonConvert.SerializeObject(gameEvents);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: GameEvents/Edit/5
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


                //GameEvents gameEvents = db.GameEvents.Find(id);

                //테이블 조인 처리
                var result =
                    from ge in db.GameEvents.ToList()
                    join il in db.ItemLists on ge.ItemListID equals il.ItemListID
                    where ge.GameEventID == id        //id값

                    select new GameEvents()
                    {
                        GameEventID = ge.GameEventID,
                        EventCategory1 = ge.EventCategory1,
                        EventCategory2 = ge.EventCategory2,
                        EventCategory3 = ge.EventCategory3,
                        ItemListID = ge.ItemListID,
                        ItemCount = ge.ItemCount,
                        Itemstatus = ge.Itemstatus,
                        TargetGroup = ge.TargetGroup,
                        TargetOS = ge.TargetOS,
                        TargetDevice = ge.TargetDevice,
                        EventImageLink = ge.EventImageLink,
                        Title = ge.Title,
                        Content = ge.Content,
                        sCol1 = ge.sCol1,
                        sCol2 = ge.sCol2,
                        sCol3 = ge.sCol3,
                        sCol4 = ge.sCol4,
                        sCol5 = ge.sCol5,
                        sCol6 = ge.sCol6,
                        sCol7 = ge.sCol7,
                        sCol8 = ge.sCol8,
                        sCol9 = ge.sCol9,
                        sCol10 = ge.sCol10,
                        CreateAdminID = ge.CreateAdminID,
                        HideYN = ge.HideYN,
                        DeleteYN = ge.DeleteYN,
                        OrderNumber = ge.OrderNumber,
                        EventDurationFrom = ge.EventDurationFrom,
                        EventDurationTo = ge.EventDurationTo,
                        CreatedAt = ge.CreatedAt,
                        UpdatedAt = ge.UpdatedAt,
                        DataFromRegion = ge.DataFromRegion,
                        DataFromRegionDT = ge.DataFromRegionDT,

                        ItemLists = new ItemLists()
                        {
                            ItemName = il.ItemName
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
                result1.EventDurationFrom = UserTime.GetUserTime(result1.EventDurationFrom.DateTime, Session["AdminTimeZone"].ToString());
                result1.EventDurationTo = UserTime.GetUserTime(result1.EventDurationTo.DateTime, Session["AdminTimeZone"].ToString());

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "GameEventsController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventsController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: GameEvents/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameEventID,EventCategory1,EventCategory2,EventCategory3,ItemListID,ItemCount,Itemstatus,TargetGroup,TargetOS,TargetDevice,EventImageLink,Title,Content,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,EventDurationFrom,EventDurationTo,OrderNumber,CreateAdminID,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] GameEvents gameEvents)
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
                    gameEvents.EventDurationFrom = UserTime.SetUtcTime(gameEvents.EventDurationFrom.DateTime, Session["AdminTimeZone"].ToString());
                    gameEvents.EventDurationTo = UserTime.SetUtcTime(gameEvents.EventDurationTo.DateTime, Session["AdminTimeZone"].ToString());

                    gameEvents.UpdatedAt = DateTimeOffset.UtcNow;

                    // 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(gameEvents);
                    }

                    db.Entry(gameEvents).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "GameEventsController-Edit(gameEvents)";
                    logMessage.Message = JsonConvert.SerializeObject(gameEvents);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(gameEvents);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventsController-Edit(gameEvents)";
                logMessage.Message = JsonConvert.SerializeObject(gameEvents);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: GameEvents/Delete/5
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
                GameEvents gameEvents = db.GameEvents.Find(id);
                if (gameEvents == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "GameEventsController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(gameEvents);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventsController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: GameEvents/Delete/5
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

                GameEvents gameEvents = db.GameEvents.Find(id);
                db.GameEvents.Remove(gameEvents);
                db.SaveChanges();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "GameEventsController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GameEventsController-DeleteConfirm(id)";
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
