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
    public class MemberGameInfoesController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 개별 entity 복호화
        public MemberGameInfoes DecryptResult(MemberGameInfoes item)
        {
            try
            {
                item.MemberID = Crypto.AES_decrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_decrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Level = Crypto.AES_decrypt(item.Level, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Exps = Crypto.AES_decrypt(item.Exps, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Points = Crypto.AES_decrypt(item.Points, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT1 = Crypto.AES_decrypt(item.UserSTAT1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT2 = Crypto.AES_decrypt(item.UserSTAT2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT3 = Crypto.AES_decrypt(item.UserSTAT3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT4 = Crypto.AES_decrypt(item.UserSTAT4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT5 = Crypto.AES_decrypt(item.UserSTAT5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT6 = Crypto.AES_decrypt(item.UserSTAT6, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT7 = Crypto.AES_decrypt(item.UserSTAT7, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT8 = Crypto.AES_decrypt(item.UserSTAT8, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT9 = Crypto.AES_decrypt(item.UserSTAT9, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT10 = Crypto.AES_decrypt(item.UserSTAT10, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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
        public MemberGameInfoes EncryptResult(MemberGameInfoes item)
        {
            try
            {
                item.MemberID = Crypto.AES_encrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_encrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Level = Crypto.AES_encrypt(item.Level, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Exps = Crypto.AES_encrypt(item.Exps, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Points = Crypto.AES_encrypt(item.Points, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT1 = Crypto.AES_encrypt(item.UserSTAT1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT2 = Crypto.AES_encrypt(item.UserSTAT2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT3 = Crypto.AES_encrypt(item.UserSTAT3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT4 = Crypto.AES_encrypt(item.UserSTAT4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT5 = Crypto.AES_encrypt(item.UserSTAT5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT6 = Crypto.AES_encrypt(item.UserSTAT6, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT7 = Crypto.AES_encrypt(item.UserSTAT7, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT8 = Crypto.AES_encrypt(item.UserSTAT8, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT9 = Crypto.AES_encrypt(item.UserSTAT9, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.UserSTAT10 = Crypto.AES_encrypt(item.UserSTAT10, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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


        // GET: MemberGameInfoes
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

                var queryString = from mg in db.MemberGameInfoes.ToList()
                    join m in db.Members on mg.MemberID equals m.MemberID
                    select new MemberGameInfoes()
                    {
                        MemberID = mg.MemberID,
                        Level = mg.Level,
                        Exps = mg.Exps,
                        Points = mg.Points,
                        UserSTAT1 = mg.UserSTAT1,
                        UserSTAT2 = mg.UserSTAT2,
                        UserSTAT3 = mg.UserSTAT3,
                        UserSTAT4 = mg.UserSTAT4,
                        UserSTAT5 = mg.UserSTAT5,
                        UserSTAT6 = mg.UserSTAT6,
                        UserSTAT7 = mg.UserSTAT7,
                        UserSTAT8 = mg.UserSTAT8,
                        UserSTAT9 = mg.UserSTAT9,
                        UserSTAT10 = mg.UserSTAT10,
                        sCol1 = mg.sCol1,
                        sCol2 = mg.sCol2,
                        sCol3 = mg.sCol3,
                        sCol4 = mg.sCol4,
                        sCol5 = mg.sCol5,
                        sCol6 = mg.sCol6,
                        sCol7 = mg.sCol7,
                        sCol8 = mg.sCol8,
                        sCol9 = mg.sCol9,
                        sCol10 = mg.sCol10,
                        HideYN = mg.HideYN,
                        DeleteYN = mg.DeleteYN,
                        CreatedAt = mg.CreatedAt,
                        UpdatedAt = mg.UpdatedAt,
                        DataFromRegion = mg.DataFromRegion,
                        DataFromRegionDT = mg.DataFromRegionDT,
                        Members = new Members()
                        {
                            Name1 = m.Name1
                        }
                    };

                if (!String.IsNullOrEmpty(searchString))
                {
                    switch (SearchCondition)
                    {
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
                logMessage.Logger = "MemberGameInfoesController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);

            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoesController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberGameInfoes/Details/5
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
                    from mg in db.MemberGameInfoes.ToList()
                    join m in db.Members on mg.MemberID equals m.MemberID
                    where mg.MemberID == id        //id값

                    select new MemberGameInfoes()
                    {
                        MemberID = mg.MemberID,
                        Level = mg.Level,
                        Exps = mg.Exps,
                        Points = mg.Points,
                        UserSTAT1 = mg.UserSTAT1,
                        UserSTAT2 = mg.UserSTAT2,
                        UserSTAT3 = mg.UserSTAT3,
                        UserSTAT4 = mg.UserSTAT4,
                        UserSTAT5 = mg.UserSTAT5,
                        UserSTAT6 = mg.UserSTAT6,
                        UserSTAT7 = mg.UserSTAT7,
                        UserSTAT8 = mg.UserSTAT8,
                        UserSTAT9 = mg.UserSTAT9,
                        UserSTAT10 = mg.UserSTAT10,
                        sCol1 = mg.sCol1,
                        sCol2 = mg.sCol2,
                        sCol3 = mg.sCol3,
                        sCol4 = mg.sCol4,
                        sCol5 = mg.sCol5,
                        sCol6 = mg.sCol6,
                        sCol7 = mg.sCol7,
                        sCol8 = mg.sCol8,
                        sCol9 = mg.sCol9,
                        sCol10 = mg.sCol10,
                        HideYN = mg.HideYN,
                        DeleteYN = mg.DeleteYN,
                        CreatedAt = mg.CreatedAt,
                        UpdatedAt = mg.UpdatedAt,
                        DataFromRegion = mg.DataFromRegion,
                        DataFromRegionDT = mg.DataFromRegionDT,
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
                logMessage.Logger = "MemberGameInfoesController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoesController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberGameInfoes/Create
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
                logMessage.Logger = "MemberGameInfoesController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoesController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: MemberGameInfoes/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,Level,Exps,Points,UserSTAT1,UserSTAT2,UserSTAT3,UserSTAT4,UserSTAT5,UserSTAT6,UserSTAT7,UserSTAT8,UserSTAT9,UserSTAT10,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] MemberGameInfoes memberGameInfoes)
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
                    memberGameInfoes.CreatedAt = DateTimeOffset.UtcNow;
                    memberGameInfoes.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        //암호화
                        EncryptResult(memberGameInfoes);
                    }

                    db.MemberGameInfoes.Add(memberGameInfoes);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MemberGameInfoesController-Create(memberGameInfoes)";
                    logMessage.Message = JsonConvert.SerializeObject(memberGameInfoes);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(memberGameInfoes);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoesController-Create(memberGameInfoes)";
                logMessage.Message = JsonConvert.SerializeObject(memberGameInfoes);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberGameInfoes/Edit/5
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


                //MemberGameInfoes memberGameInfoes = db.MemberGameInfoes.Find(id);

                //테이블 조인 처리
                var result =
                    from mg in db.MemberGameInfoes.ToList()
                    join m in db.Members on mg.MemberID equals m.MemberID
                    where mg.MemberID == id        //id값

                    select new MemberGameInfoes()
                    {
                        MemberID = mg.MemberID,
                        Level = mg.Level,
                        Exps = mg.Exps,
                        Points = mg.Points,
                        UserSTAT1 = mg.UserSTAT1,
                        UserSTAT2 = mg.UserSTAT2,
                        UserSTAT3 = mg.UserSTAT3,
                        UserSTAT4 = mg.UserSTAT4,
                        UserSTAT5 = mg.UserSTAT5,
                        UserSTAT6 = mg.UserSTAT6,
                        UserSTAT7 = mg.UserSTAT7,
                        UserSTAT8 = mg.UserSTAT8,
                        UserSTAT9 = mg.UserSTAT9,
                        UserSTAT10 = mg.UserSTAT10,
                        sCol1 = mg.sCol1,
                        sCol2 = mg.sCol2,
                        sCol3 = mg.sCol3,
                        sCol4 = mg.sCol4,
                        sCol5 = mg.sCol5,
                        sCol6 = mg.sCol6,
                        sCol7 = mg.sCol7,
                        sCol8 = mg.sCol8,
                        sCol9 = mg.sCol9,
                        sCol10 = mg.sCol10,
                        HideYN = mg.HideYN,
                        DeleteYN = mg.DeleteYN,
                        CreatedAt = mg.CreatedAt,
                        UpdatedAt = mg.UpdatedAt,
                        DataFromRegion = mg.DataFromRegion,
                        DataFromRegionDT = mg.DataFromRegionDT,
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
                logMessage.Logger = "MemberGameInfoesController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoesController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: MemberGameInfoes/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,Level,Exps,Points,UserSTAT1,UserSTAT2,UserSTAT3,UserSTAT4,UserSTAT5,UserSTAT6,UserSTAT7,UserSTAT8,UserSTAT9,UserSTAT10,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] MemberGameInfoes memberGameInfoes)
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
                    // 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(memberGameInfoes);
                    }

                    // Edit 입력값 자동처리
                    memberGameInfoes.UpdatedAt = DateTimeOffset.UtcNow;

                    db.Entry(memberGameInfoes).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MemberGameInfoesController-Edit(memberGameInfoes)";
                    logMessage.Message = JsonConvert.SerializeObject(memberGameInfoes);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(memberGameInfoes);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoesController-Edit(memberGameInfoes)";
                logMessage.Message = JsonConvert.SerializeObject(memberGameInfoes);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberGameInfoes/Delete/5
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
                MemberGameInfoes memberGameInfoes = db.MemberGameInfoes.Find(id);
                if (memberGameInfoes == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberGameInfoesController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(memberGameInfoes);

            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoesController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: MemberGameInfoes/Delete/5
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

                MemberGameInfoes memberGameInfoes = db.MemberGameInfoes.Find(id);
                db.MemberGameInfoes.Remove(memberGameInfoes);
                db.SaveChanges();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberGameInfoesController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoesController-DeleteConfirm(id)";
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
