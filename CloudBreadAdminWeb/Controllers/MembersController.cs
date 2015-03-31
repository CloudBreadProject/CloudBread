using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudBreadAdminWeb;

using System.Threading.Tasks;
using System.Diagnostics;
using PagedList;
using CloudBreadAdminWeb.globals;
using CloudBreadLib.BAL.Crypto;
using CloudBreadLib.BAL.UserTime;
using Logger.Logging;
using Newtonsoft.Json;

namespace CloudBreadAdminWeb.Controllers
{
    public class MembersController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 개별 entity 복호화
        public Members DecryptResult(Members item)   
        {
            try
            {
                item.MemberID = Crypto.AES_decrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberPWD = Crypto.AES_decrypt(item.MemberPWD, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EmailAddress = Crypto.AES_decrypt(item.EmailAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EmailConfirmedYN = Crypto.AES_decrypt(item.EmailConfirmedYN, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PhoneNumber1 = Crypto.AES_decrypt(item.PhoneNumber1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PhoneNumber2 = Crypto.AES_decrypt(item.PhoneNumber2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PINumber = Crypto.AES_decrypt(item.PINumber, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Name1 = Crypto.AES_decrypt(item.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Name2 = Crypto.AES_decrypt(item.Name2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Name3 = Crypto.AES_decrypt(item.Name3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.DOB = Crypto.AES_decrypt(item.DOB, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.RecommenderID = Crypto.AES_decrypt(item.RecommenderID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberGroup = Crypto.AES_decrypt(item.MemberGroup, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.LastDeviceID = Crypto.AES_decrypt(item.LastDeviceID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.LastIPaddress = Crypto.AES_decrypt(item.LastIPaddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.LastLoginDT = Crypto.AES_decrypt(item.LastLoginDT, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.LastLogoutDT = Crypto.AES_decrypt(item.LastLogoutDT, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.LastMACAddress = Crypto.AES_decrypt(item.LastMACAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.AccountBlockYN = Crypto.AES_decrypt(item.AccountBlockYN, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.AccountBlockEndDT = Crypto.AES_decrypt(item.AccountBlockEndDT, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.AnonymousYN = Crypto.AES_decrypt(item.AnonymousYN, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.C3rdAuthProvider = Crypto.AES_decrypt(item.C3rdAuthProvider, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.C3rdAuthID = Crypto.AES_decrypt(item.C3rdAuthID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.C3rdAuthParam = Crypto.AES_decrypt(item.C3rdAuthParam, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PushNotificationID = Crypto.AES_decrypt(item.PushNotificationID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PushNotificationProvider = Crypto.AES_decrypt(item.PushNotificationProvider, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PushNotificationGroup = Crypto.AES_decrypt(item.PushNotificationGroup, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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
        public Members EncryptResult(Members item)
        {
            try
            {
                item.MemberID = Crypto.AES_encrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberPWD = Crypto.AES_encrypt(item.MemberPWD, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EmailAddress = Crypto.AES_encrypt(item.EmailAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.EmailConfirmedYN = Crypto.AES_encrypt(item.EmailConfirmedYN, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PhoneNumber1 = Crypto.AES_encrypt(item.PhoneNumber1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PhoneNumber2 = Crypto.AES_encrypt(item.PhoneNumber2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PINumber = Crypto.AES_encrypt(item.PINumber, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Name1 = Crypto.AES_encrypt(item.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Name2 = Crypto.AES_encrypt(item.Name2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Name3 = Crypto.AES_encrypt(item.Name3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.DOB = Crypto.AES_encrypt(item.DOB, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.RecommenderID = Crypto.AES_encrypt(item.RecommenderID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberGroup = Crypto.AES_encrypt(item.MemberGroup, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.LastDeviceID = Crypto.AES_encrypt(item.LastDeviceID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.LastIPaddress = Crypto.AES_encrypt(item.LastIPaddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.LastLoginDT = Crypto.AES_encrypt(item.LastLoginDT, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.LastLogoutDT = Crypto.AES_encrypt(item.LastLogoutDT, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.LastMACAddress = Crypto.AES_encrypt(item.LastMACAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.AccountBlockYN = Crypto.AES_encrypt(item.AccountBlockYN, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.AccountBlockEndDT = Crypto.AES_encrypt(item.AccountBlockEndDT, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.AnonymousYN = Crypto.AES_encrypt(item.AnonymousYN, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.C3rdAuthProvider = Crypto.AES_encrypt(item.C3rdAuthProvider, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.C3rdAuthID = Crypto.AES_encrypt(item.C3rdAuthID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.C3rdAuthParam = Crypto.AES_encrypt(item.C3rdAuthParam, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PushNotificationID = Crypto.AES_encrypt(item.PushNotificationID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PushNotificationProvider = Crypto.AES_encrypt(item.PushNotificationProvider, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PushNotificationGroup = Crypto.AES_encrypt(item.PushNotificationGroup, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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

        // GET: Members
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

                var queryString = from s in db.Members
                                  select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    switch (SearchCondition)
                    {
                        case "MemberID":
                            queryString = queryString.Where(s => s.MemberID.Contains(searchString));
                            break;
                        case "EmailAddress":
                            queryString = queryString.Where(s => s.EmailAddress.Contains(searchString));
                            break;
                        case "Name1":
                            queryString = queryString.Where(s => s.Name1.Contains(searchString));
                            break;
                        case "LastDeviceID":
                            queryString = queryString.Where(s => s.LastDeviceID.Contains(searchString));
                            break;
                        case "3rdAuthID":
                            queryString = queryString.Where(s => s.C3rdAuthID.Contains(searchString));
                            break;
                        case "PushNotificationID":
                            queryString = queryString.Where(s => s.PushNotificationID.Contains(searchString));
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

                //복호화 처리
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
                logMessage.Logger = "MembersController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MembersController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: Members/Details/5
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
                Members members = db.Members.Find(id);

                if (members == null)
                {
                    return HttpNotFound();
                }

                //복호화 처리
                if (globalVal.CloudBreadCryptSetting == "AES256")
                {
                    DecryptResult(members);       //members 타입
                }

                //UTC를 세션의 UserTimeZone으로 변환 - 안하면 UTC 그대로 보임
                members.LastLoginDT = UserTime.GetUserTime(DateTime.Parse(members.LastLoginDT), Session["AdminTimeZone"].ToString()).ToString();
                members.LastLogoutDT = UserTime.GetUserTime(DateTime.Parse(members.LastLogoutDT), Session["AdminTimeZone"].ToString()).ToString();
                members.AccountBlockEndDT = UserTime.GetUserTime(DateTime.Parse(members.AccountBlockEndDT), Session["AdminTimeZone"].ToString()).ToString();
                
                members.CreatedAt = UserTime.GetUserTime(members.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                members.UpdatedAt = UserTime.GetUserTime(members.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                members.DataFromRegionDT = UserTime.GetUserTime(members.DataFromRegionDT.DateTime, Session["AdminTimeZone"].ToString());
                
                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "membersController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(members);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "membersController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: Members/Create
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
                logMessage.Logger = "MembersController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MembersController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // POST: Members/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,MemberPWD,EmailAddress,EmailConfirmedYN,PhoneNumber1,PhoneNumber2,PINumber,Name1,Name2,Name3,DOB,RecommenderID,MemberGroup,LastDeviceID,LastIPaddress,LastLoginDT,LastLogoutDT,LastMACAddress,AccountBlockYN,AccountBlockEndDT,AnonymousYN,C3rdAuthProvider,C3rdAuthID,C3rdAuthParam,PushNotificationID,PushNotificationProvider,PushNotificationGroup,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,TimeZoneID,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] Members members)
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

                    // 입력값 기본 처리 
                    members.MemberPWD = Crypto.SHA512Hash(members.MemberPWD);
                    members.EmailConfirmedYN = "N";
                    members.LastDeviceID = "";
                    members.LastIPaddress = "";
                    members.LastLoginDT = "1900-01-01";
                    members.LastLogoutDT = "1900-01-01";
                    members.LastMACAddress = "";
                    members.AccountBlockYN = "N";
                    members.AccountBlockEndDT = "1900-01-01";

                    members.CreatedAt = DateTimeOffset.UtcNow;
                    members.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        //암호화
                        EncryptResult(members);
                    }

                    db.Members.Add(members);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MembersController-Create(members)";
                    logMessage.Message = JsonConvert.SerializeObject(members);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(members);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MembersController-Create(members)";
                logMessage.Message = JsonConvert.SerializeObject(members);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: Members/Edit/5
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
                Members members = db.Members.Find(id);
                if (members == null)
                {
                    return HttpNotFound();
                }

                //복호화 처리
                if (globalVal.CloudBreadCryptSetting == "AES256")
                {
                    DecryptResult(members);
                }

                //UTC를 세션의 UserTimeZone으로 변환 - 안하면 UTC 그대로 보임
                //암호화된 컬럼 값에 대한 처리
                members.LastLoginDT = UserTime.GetUserTime(DateTime.Parse(members.LastLoginDT), Session["AdminTimeZone"].ToString()).ToString();
                members.LastLogoutDT = UserTime.GetUserTime(DateTime.Parse(members.LastLogoutDT), Session["AdminTimeZone"].ToString()).ToString();
                members.AccountBlockEndDT = UserTime.GetUserTime(DateTime.Parse(members.AccountBlockEndDT), Session["AdminTimeZone"].ToString()).ToString();
                
                //일반 컬럼 값에 대한 처리

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MembersController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(members);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MembersController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            } 
                
        }

        // POST: Members/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,MemberPWD,EmailAddress,EmailConfirmedYN,PhoneNumber1,PhoneNumber2,PINumber,Name1,Name2,Name3,DOB,RecommenderID,MemberGroup,LastDeviceID,LastIPaddress,LastLoginDT,LastLogoutDT,LastMACAddress,AccountBlockYN,AccountBlockEndDT,AnonymousYN,C3rdAuthProvider,C3rdAuthID,C3rdAuthParam,PushNotificationID,PushNotificationProvider,PushNotificationGroup,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,TimeZoneID,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] Members members)
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
                        EncryptResult(members);
                    }

                    // Edit 입력값 자동처리
                    //members.MemberPWD = Crypto.SHA512Hash(members.MemberPWD); // Edit은 PWD 해쉬 시키면 안된다.
                    //members.LastLoginDT = UserTime.SetUtcTime(DateTime.Parse(members.LastLoginDT), Session["AdminTimeZone"].ToString()).ToString();
                    //members.LastLogoutDT = UserTime.SetUtcTime(DateTime.Parse(members.LastLogoutDT), Session["AdminTimeZone"].ToString()).ToString();
                    //members.AccountBlockEndDT = UserTime.SetUtcTime(DateTime.Parse(members.AccountBlockEndDT), Session["AdminTimeZone"].ToString()).ToString();

                    members.UpdatedAt = DateTimeOffset.UtcNow;

                    db.Entry(members).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MembersController-Edit(members)";
                    logMessage.Message = JsonConvert.SerializeObject(members);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(members);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MembersController-Edit(members)";
                logMessage.Message = JsonConvert.SerializeObject(members);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: Members/Delete/5
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
                Members members = db.Members.Find(id);
                if (members == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MembersController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(members);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MembersController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: Members/Delete/5
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

                Members members = db.Members.Find(id);
                db.Members.Remove(members);
                db.SaveChanges();
                
                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MembersController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MembersController-DeleteConfirm(id)";
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
