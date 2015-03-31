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
    public class MemberGameInfoStagesController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 복호화 수행
        public MemberGameInfoStages DecryptResult(MemberGameInfoStages item)
        {
            try
            {
                item.MemberGameInfoStageID = Crypto.AES_decrypt(item.MemberGameInfoStageID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberID = Crypto.AES_decrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_decrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageName = Crypto.AES_decrypt(item.StageName, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStatus = Crypto.AES_decrypt(item.StageStatus, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Category1 = Crypto.AES_decrypt(item.Category1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Category2 = Crypto.AES_decrypt(item.Category2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Category3 = Crypto.AES_decrypt(item.Category3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Mission1 = Crypto.AES_decrypt(item.Mission1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Mission2 = Crypto.AES_decrypt(item.Mission2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Mission3 = Crypto.AES_decrypt(item.Mission3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Mission4 = Crypto.AES_decrypt(item.Mission4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Mission5 = Crypto.AES_decrypt(item.Mission5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Points = Crypto.AES_decrypt(item.Points, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStat1 = Crypto.AES_decrypt(item.StageStat1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStat2 = Crypto.AES_decrypt(item.StageStat2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStat3 = Crypto.AES_decrypt(item.StageStat3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStat4 = Crypto.AES_decrypt(item.StageStat4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStat5 = Crypto.AES_decrypt(item.StageStat5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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
        public MemberGameInfoStages EncryptResult(MemberGameInfoStages item)
        {
            try
            {
                item.MemberGameInfoStageID = Crypto.AES_encrypt(item.MemberGameInfoStageID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberID = Crypto.AES_encrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_encrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageName = Crypto.AES_encrypt(item.StageName, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStatus = Crypto.AES_encrypt(item.StageStatus, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Category1 = Crypto.AES_encrypt(item.Category1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Category2 = Crypto.AES_encrypt(item.Category2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Category3 = Crypto.AES_encrypt(item.Category3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Mission1 = Crypto.AES_encrypt(item.Mission1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Mission2 = Crypto.AES_encrypt(item.Mission2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Mission3 = Crypto.AES_encrypt(item.Mission3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Mission4 = Crypto.AES_encrypt(item.Mission4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Mission5 = Crypto.AES_encrypt(item.Mission5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Points = Crypto.AES_encrypt(item.Points, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStat1 = Crypto.AES_encrypt(item.StageStat1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStat2 = Crypto.AES_encrypt(item.StageStat2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStat3 = Crypto.AES_encrypt(item.StageStat3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStat4 = Crypto.AES_encrypt(item.StageStat4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.StageStat5 = Crypto.AES_encrypt(item.StageStat5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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


        // GET: MemberGameInfoStages
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

                var queryString = 
                    from mgs in db.MemberGameInfoStages.ToList()
                    join m in db.Members on mgs.MemberID equals m.MemberID
                        select new MemberGameInfoStages()
                        {
                            MemberGameInfoStageID = mgs.MemberGameInfoStageID,
                            MemberID = mgs.MemberID,
                            StageName = mgs.StageName,
                            StageStatus = mgs.StageStatus,
                            Category1 = mgs.Category1,
                            Category2 = mgs.Category2,
                            Category3 = mgs.Category3,
                            Mission1 = mgs.Mission1,
                            Mission2 = mgs.Mission2,
                            Mission3 = mgs.Mission3,
                            Mission4 = mgs.Mission4,
                            Mission5 = mgs.Mission5,
                            Points = mgs.Points,
                            StageStat1 = mgs.StageStat1,
                            StageStat2 = mgs.StageStat2,
                            StageStat3 = mgs.StageStat3,
                            StageStat4 = mgs.StageStat4,
                            StageStat5 = mgs.StageStat5,
                            sCol1 = mgs.sCol1,
                            sCol2 = mgs.sCol2,
                            sCol3 = mgs.sCol3,
                            sCol4 = mgs.sCol4,
                            sCol5 = mgs.sCol5,
                            sCol6 = mgs.sCol6,
                            sCol7 = mgs.sCol7,
                            sCol8 = mgs.sCol8,
                            sCol9 = mgs.sCol9,
                            sCol10 = mgs.sCol10,
                            HideYN = mgs.HideYN,
                            DeleteYN = mgs.DeleteYN,
                            CreatedAt = mgs.CreatedAt,
                            UpdatedAt = mgs.UpdatedAt,
                            DataFromRegion = mgs.DataFromRegion,
                            DataFromRegionDT = mgs.DataFromRegionDT,
                            Members = new Members()
                            {
                                Name1 = m.Name1
                            }
                        };

                if (!String.IsNullOrEmpty(searchString))
                {
                    switch (SearchCondition)
                    {
                        case "MemberGameInfoStageID":
                            queryString = queryString.Where(s => s.MemberGameInfoStageID.Contains(searchString));
                            break;
                        case "MemberID":
                            queryString = queryString.Where(s => s.StageName.Contains(searchString));
                            break;
                        case "StageName":
                            queryString = queryString.Where(s => s.StageName.Contains(searchString));
                            break;
                        case "Category1":
                            queryString = queryString.Where(s => s.Category1.Contains(searchString));
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
                logMessage.Logger = "MemberGameInfoStagesController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);

            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoStagesController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: MemberGameInfoStages/Details/5
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
                    from mgs in db.MemberGameInfoStages.ToList()
                    join m in db.Members on mgs.MemberID equals m.MemberID
                    where mgs.MemberGameInfoStageID == id        //id값
                    select new MemberGameInfoStages()
                    {
                        MemberGameInfoStageID = mgs.MemberGameInfoStageID,
                        MemberID = mgs.MemberID,
                        StageName = mgs.StageName,
                        StageStatus = mgs.StageStatus,
                        Category1 = mgs.Category1,
                        Category2 = mgs.Category2,
                        Category3 = mgs.Category3,
                        Mission1 = mgs.Mission1,
                        Mission2 = mgs.Mission2,
                        Mission3 = mgs.Mission3,
                        Mission4 = mgs.Mission4,
                        Mission5 = mgs.Mission5,
                        Points = mgs.Points,
                        StageStat1 = mgs.StageStat1,
                        StageStat2 = mgs.StageStat2,
                        StageStat3 = mgs.StageStat3,
                        StageStat4 = mgs.StageStat4,
                        StageStat5 = mgs.StageStat5,
                        sCol1 = mgs.sCol1,
                        sCol2 = mgs.sCol2,
                        sCol3 = mgs.sCol3,
                        sCol4 = mgs.sCol4,
                        sCol5 = mgs.sCol5,
                        sCol6 = mgs.sCol6,
                        sCol7 = mgs.sCol7,
                        sCol8 = mgs.sCol8,
                        sCol9 = mgs.sCol9,
                        sCol10 = mgs.sCol10,
                        HideYN = mgs.HideYN,
                        DeleteYN = mgs.DeleteYN,
                        CreatedAt = mgs.CreatedAt,
                        UpdatedAt = mgs.UpdatedAt,
                        DataFromRegion = mgs.DataFromRegion,
                        DataFromRegionDT = mgs.DataFromRegionDT,
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
                logMessage.Logger = "MemberGameInfoStagesController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoStagesController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: MemberGameInfoStages/Create
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
                logMessage.Logger = "MemberGameInfoStagesController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoStagesController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // POST: MemberGameInfoStages/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberGameInfoStageID,MemberID,StageName,StageStatus,Category1,Category2,Category3,Mission1,Mission2,Mission3,Mission4,Mission5,Points,StageStat1,StageStat2,StageStat3,StageStat4,StageStat5,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] MemberGameInfoStages memberGameInfoStages)
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
                    memberGameInfoStages.CreatedAt = DateTimeOffset.UtcNow;
                    memberGameInfoStages.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(memberGameInfoStages);
                    }

                    db.MemberGameInfoStages.Add(memberGameInfoStages);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MemberGameInfoStagesController-Create(memberGameInfoStages)";
                    logMessage.Message = JsonConvert.SerializeObject(memberGameInfoStages);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(memberGameInfoStages);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoStagesController-Create(memberGameInfoStages)";
                logMessage.Message = JsonConvert.SerializeObject(memberGameInfoStages);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberGameInfoStages/Edit/5
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

                //테이블 조인 처리
                var result =
                    from mgs in db.MemberGameInfoStages.ToList()
                    join m in db.Members on mgs.MemberID equals m.MemberID
                    where mgs.MemberGameInfoStageID == id        //id값
                    select new MemberGameInfoStages()
                    {
                        MemberGameInfoStageID = mgs.MemberGameInfoStageID,
                        MemberID = mgs.MemberID,
                        StageName = mgs.StageName,
                        StageStatus = mgs.StageStatus,
                        Category1 = mgs.Category1,
                        Category2 = mgs.Category2,
                        Category3 = mgs.Category3,
                        Mission1 = mgs.Mission1,
                        Mission2 = mgs.Mission2,
                        Mission3 = mgs.Mission3,
                        Mission4 = mgs.Mission4,
                        Mission5 = mgs.Mission5,
                        Points = mgs.Points,
                        StageStat1 = mgs.StageStat1,
                        StageStat2 = mgs.StageStat2,
                        StageStat3 = mgs.StageStat3,
                        StageStat4 = mgs.StageStat4,
                        StageStat5 = mgs.StageStat5,
                        sCol1 = mgs.sCol1,
                        sCol2 = mgs.sCol2,
                        sCol3 = mgs.sCol3,
                        sCol4 = mgs.sCol4,
                        sCol5 = mgs.sCol5,
                        sCol6 = mgs.sCol6,
                        sCol7 = mgs.sCol7,
                        sCol8 = mgs.sCol8,
                        sCol9 = mgs.sCol9,
                        sCol10 = mgs.sCol10,
                        HideYN = mgs.HideYN,
                        DeleteYN = mgs.DeleteYN,
                        CreatedAt = mgs.CreatedAt,
                        UpdatedAt = mgs.UpdatedAt,
                        DataFromRegion = mgs.DataFromRegion,
                        DataFromRegionDT = mgs.DataFromRegionDT,
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
                logMessage.Logger = "MemberGameInfoStagesController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoStagesController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: MemberGameInfoStages/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberGameInfoStageID,MemberID,StageName,StageStatus,Category1,Category2,Category3,Mission1,Mission2,Mission3,Mission4,Mission5,Points,StageStat1,StageStat2,StageStat3,StageStat4,StageStat5,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] MemberGameInfoStages memberGameInfoStages)
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
                    memberGameInfoStages.UpdatedAt = DateTimeOffset.UtcNow;

                    // 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(memberGameInfoStages);
                    }

                    db.Entry(memberGameInfoStages).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MemberGameInfoStagesController-Edit(memberGameInfoStages)";
                    logMessage.Message = JsonConvert.SerializeObject(memberGameInfoStages);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(memberGameInfoStages);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoStagesController-Edit(memberGameInfoStages)";
                logMessage.Message = JsonConvert.SerializeObject(memberGameInfoStages);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberGameInfoStages/Delete/5
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
                MemberGameInfoStages memberGameInfoStages = db.MemberGameInfoStages.Find(id);
                if (memberGameInfoStages == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberGameInfoStagesController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(memberGameInfoStages);

            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoStagesController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: MemberGameInfoStages/Delete/5
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

                MemberGameInfoStages memberGameInfoStages = db.MemberGameInfoStages.Find(id);
                db.MemberGameInfoStages.Remove(memberGameInfoStages);
                db.SaveChanges();
                
                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberGameInfoStagesController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberGameInfoStagesController-DeleteConfirm(id)";
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
