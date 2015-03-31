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
    public class MemberItemPurchasesController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 복호화 수행
        public MemberItemPurchases DecryptResult(MemberItemPurchases item)
        {
            try
            {
                item.MemberItemPurchaseID = Crypto.AES_decrypt(item.MemberItemPurchaseID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberID = Crypto.AES_decrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_decrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemListID = Crypto.AES_decrypt(item.ItemListID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemLists.ItemName = Crypto.AES_decrypt(item.ItemLists.ItemName, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchasePrice = Crypto.AES_decrypt(item.PurchasePrice, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseQuantity = Crypto.AES_decrypt(item.PurchaseQuantity, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PGinfo1 = Crypto.AES_decrypt(item.PGinfo1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PGinfo2 = Crypto.AES_decrypt(item.PGinfo2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PGinfo3 = Crypto.AES_decrypt(item.PGinfo3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PGinfo4 = Crypto.AES_decrypt(item.PGinfo4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PGinfo5 = Crypto.AES_decrypt(item.PGinfo5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseDeviceID = Crypto.AES_decrypt(item.PurchaseDeviceID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseDeviceIPAddress = Crypto.AES_decrypt(item.PurchaseDeviceIPAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseDeviceMACAddress = Crypto.AES_decrypt(item.PurchaseDeviceMACAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseDT = Crypto.AES_decrypt(item.PurchaseDT, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelYN = Crypto.AES_decrypt(item.PurchaseCancelYN, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelDT = Crypto.AES_decrypt(item.PurchaseCancelDT, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelingStatus = Crypto.AES_decrypt(item.PurchaseCancelingStatus, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelReturnedAmount = Crypto.AES_decrypt(item.PurchaseCancelReturnedAmount, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelDeviceID = Crypto.AES_decrypt(item.PurchaseCancelDeviceID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelDeviceIPAddress = Crypto.AES_decrypt(item.PurchaseCancelDeviceIPAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelDeviceMACAddress = Crypto.AES_decrypt(item.PurchaseCancelDeviceMACAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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
        public MemberItemPurchases EncryptResult(MemberItemPurchases item)
        {
            try
            {
                item.MemberItemPurchaseID = Crypto.AES_encrypt(item.MemberItemPurchaseID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberID = Crypto.AES_encrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_encrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemListID = Crypto.AES_encrypt(item.ItemListID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemLists.ItemName = Crypto.AES_encrypt(item.ItemLists.ItemName, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchasePrice = Crypto.AES_encrypt(item.PurchasePrice, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseQuantity = Crypto.AES_encrypt(item.PurchaseQuantity, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PGinfo1 = Crypto.AES_encrypt(item.PGinfo1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PGinfo2 = Crypto.AES_encrypt(item.PGinfo2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PGinfo3 = Crypto.AES_encrypt(item.PGinfo3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PGinfo4 = Crypto.AES_encrypt(item.PGinfo4, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PGinfo5 = Crypto.AES_encrypt(item.PGinfo5, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseDeviceID = Crypto.AES_encrypt(item.PurchaseDeviceID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseDeviceIPAddress = Crypto.AES_encrypt(item.PurchaseDeviceIPAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseDeviceMACAddress = Crypto.AES_encrypt(item.PurchaseDeviceMACAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseDT = Crypto.AES_encrypt(item.PurchaseDT, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelYN = Crypto.AES_encrypt(item.PurchaseCancelYN, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelDT = Crypto.AES_encrypt(item.PurchaseCancelDT, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelingStatus = Crypto.AES_encrypt(item.PurchaseCancelingStatus, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelReturnedAmount = Crypto.AES_encrypt(item.PurchaseCancelReturnedAmount, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelDeviceID = Crypto.AES_encrypt(item.PurchaseCancelDeviceID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelDeviceIPAddress = Crypto.AES_encrypt(item.PurchaseCancelDeviceIPAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.PurchaseCancelDeviceMACAddress = Crypto.AES_encrypt(item.PurchaseCancelDeviceMACAddress, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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
                if (strSession != "Admin" && strSession != "Operator") // Admin, Operator 권한
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

        // GET: MemberItemPurchases
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

                var queryString = from mip in db.MemberItemPurchases.ToList()
                                  join m in db.Members on mip.MemberID equals m.MemberID
                                  join il in db.ItemLists on mip.ItemListID equals il.ItemListID
                                  select new MemberItemPurchases()
                                  {
                                        MemberItemPurchaseID = mip.MemberItemPurchaseID,
                                        MemberID = mip.MemberID,
                                        ItemListID = mip.ItemListID,
                                        PurchasePrice = mip.PurchasePrice,
                                        PurchaseQuantity = mip.PurchaseQuantity,
                                        PGinfo1 = mip.PGinfo1,
                                        PGinfo2 = mip.PGinfo2,
                                        PGinfo3 = mip.PGinfo3,
                                        PGinfo4 = mip.PGinfo4,
                                        PGinfo5 = mip.PGinfo5,
                                        PurchaseDeviceID = mip.PurchaseDeviceID,
                                        PurchaseDeviceIPAddress = mip.PurchaseDeviceIPAddress,
                                        PurchaseDeviceMACAddress = mip.PurchaseDeviceMACAddress,
                                        PurchaseDT = mip.PurchaseDT,
                                        PurchaseCancelYN = mip.PurchaseCancelYN,
                                        PurchaseCancelDT = mip.PurchaseCancelDT,
                                        PurchaseCancelingStatus = mip.PurchaseCancelingStatus,
                                        PurchaseCancelReturnedAmount = mip.PurchaseCancelReturnedAmount,
                                        PurchaseCancelDeviceID = mip.PurchaseCancelDeviceID,
                                        PurchaseCancelDeviceIPAddress = mip.PurchaseCancelDeviceIPAddress,
                                        PurchaseCancelDeviceMACAddress = mip.PurchaseCancelDeviceMACAddress,
                                        sCol1 = mip.sCol1,
                                        sCol2 = mip.sCol2,
                                        sCol3 = mip.sCol3,
                                        sCol4 = mip.sCol4,
                                        sCol5 = mip.sCol5,
                                        sCol6 = mip.sCol6,
                                        sCol7 = mip.sCol7,
                                        sCol8 = mip.sCol8,
                                        sCol9 = mip.sCol9,
                                        sCol10 = mip.sCol10,
                                        PurchaseCancelConfirmAdminMemberID = mip.PurchaseCancelConfirmAdminMemberID,
                                        HideYN = mip.HideYN,
                                        DeleteYN = mip.DeleteYN,
                                        CreatedAt = mip.CreatedAt,
                                        UpdatedAt = mip.UpdatedAt,
                                        DataFromRegion = mip.DataFromRegion,
                                        DataFromRegionDT = mip.DataFromRegionDT,

                                      ItemLists = new ItemLists()
                                      {
                                          ItemName = il.ItemName
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
                        case "MemberItemPurchaseID":
                            queryString = queryString.Where(s => s.MemberItemPurchaseID.Contains(searchString));
                            break;
                        case "MemberID":
                            queryString = queryString.Where(s => s.MemberID.Contains(searchString));
                            break;
                        case "ItemListID":
                            queryString = queryString.Where(s => s.ItemListID.Contains(searchString));
                            break;
                        case "PurchaseDeviceID":
                            queryString = queryString.Where(s => s.PurchaseDeviceID.Contains(searchString));
                            break;
                        case "PurchaseCancelingStatus":
                            queryString = queryString.Where(s => s.PurchaseCancelingStatus.Contains(searchString));
                            break;
                        case "PurchaseCancelDeviceID":
                            queryString = queryString.Where(s => s.PurchaseCancelDeviceID.Contains(searchString));
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
                    item.PurchaseDT = UserTime.GetUserTime(DateTime.Parse(item.PurchaseDT), Session["AdminTimeZone"].ToString()).ToString();
                    item.PurchaseCancelDT = UserTime.GetUserTime(DateTime.Parse(item.PurchaseCancelDT), Session["AdminTimeZone"].ToString()).ToString();

                    item.CreatedAt = UserTime.GetUserTime(item.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                    item.UpdatedAt = UserTime.GetUserTime(item.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberItemPurchasesController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);

            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemPurchasesController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberItemPurchases/Details/5
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
                var result = from mip in db.MemberItemPurchases.ToList()
                                  join m in db.Members on mip.MemberID equals m.MemberID
                                  join il in db.ItemLists on mip.ItemListID equals il.ItemListID
                             where mip.MemberItemPurchaseID == id        //id값
                                  select new MemberItemPurchases()
                                  {
                                      MemberItemPurchaseID = mip.MemberItemPurchaseID,
                                      MemberID = mip.MemberID,
                                      ItemListID = mip.ItemListID,
                                      PurchasePrice = mip.PurchasePrice,
                                      PurchaseQuantity = mip.PurchaseQuantity,
                                      PGinfo1 = mip.PGinfo1,
                                      PGinfo2 = mip.PGinfo2,
                                      PGinfo3 = mip.PGinfo3,
                                      PGinfo4 = mip.PGinfo4,
                                      PGinfo5 = mip.PGinfo5,
                                      PurchaseDeviceID = mip.PurchaseDeviceID,
                                      PurchaseDeviceIPAddress = mip.PurchaseDeviceIPAddress,
                                      PurchaseDeviceMACAddress = mip.PurchaseDeviceMACAddress,
                                      PurchaseDT = mip.PurchaseDT,
                                      PurchaseCancelYN = mip.PurchaseCancelYN,
                                      PurchaseCancelDT = mip.PurchaseCancelDT,
                                      PurchaseCancelingStatus = mip.PurchaseCancelingStatus,
                                      PurchaseCancelReturnedAmount = mip.PurchaseCancelReturnedAmount,
                                      PurchaseCancelDeviceID = mip.PurchaseCancelDeviceID,
                                      PurchaseCancelDeviceIPAddress = mip.PurchaseCancelDeviceIPAddress,
                                      PurchaseCancelDeviceMACAddress = mip.PurchaseCancelDeviceMACAddress,
                                      sCol1 = mip.sCol1,
                                      sCol2 = mip.sCol2,
                                      sCol3 = mip.sCol3,
                                      sCol4 = mip.sCol4,
                                      sCol5 = mip.sCol5,
                                      sCol6 = mip.sCol6,
                                      sCol7 = mip.sCol7,
                                      sCol8 = mip.sCol8,
                                      sCol9 = mip.sCol9,
                                      sCol10 = mip.sCol10,
                                      PurchaseCancelConfirmAdminMemberID = mip.PurchaseCancelConfirmAdminMemberID,
                                      HideYN = mip.HideYN,
                                      DeleteYN = mip.DeleteYN,
                                      CreatedAt = mip.CreatedAt,
                                      UpdatedAt = mip.UpdatedAt,
                                      DataFromRegion = mip.DataFromRegion,
                                      DataFromRegionDT = mip.DataFromRegionDT,

                                      ItemLists = new ItemLists()
                                      {
                                          ItemName = il.ItemName
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
                result1.PurchaseDT = UserTime.GetUserTime(DateTime.Parse(result1.PurchaseDT), Session["AdminTimeZone"].ToString()).ToString();
                result1.PurchaseCancelDT = UserTime.GetUserTime(DateTime.Parse(result1.PurchaseCancelDT), Session["AdminTimeZone"].ToString()).ToString();

                result1.CreatedAt = UserTime.GetUserTime(result1.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                result1.UpdatedAt = UserTime.GetUserTime(result1.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                result1.DataFromRegionDT = UserTime.GetUserTime(result1.DataFromRegionDT.DateTime, Session["AdminTimeZone"].ToString());

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberItemPurchasesController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemPurchasesController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: MemberItemPurchases/Create
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
                logMessage.Logger = "MemberItemPurchasesController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemPurchasesController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: MemberItemPurchases/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberItemPurchaseID,MemberID,ItemListID,PurchaseQuantity,PurchasePrice,PGinfo1,PGinfo2,PGinfo3,PGinfo4,PGinfo5,PurchaseDeviceID,PurchaseDeviceIPAddress,PurchaseDeviceMACAddress,PurchaseDT,PurchaseCancelYN,PurchaseCancelDT,PurchaseCancelingStatus,PurchaseCancelReturnedAmount,PurchaseCancelDeviceID,PurchaseCancelDeviceIPAddress,PurchaseCancelDeviceMACAddress,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,PurchaseCancelConfirmAdminMemberID,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] MemberItemPurchases memberItemPurchases)
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
                    memberItemPurchases.PurchaseDT = DateTime.UtcNow.ToString();    // DateTimeOffset 으로 하면 GetUserTime 함수에서 오류. DateTime.Parse()가 자동으로 UTC면 시스템의 localTimeZone을 읽어 local time으로 만들어버린다.

                    memberItemPurchases.PurchaseCancelConfirmAdminMemberID = "";
                    memberItemPurchases.CreatedAt = DateTimeOffset.UtcNow;
                    memberItemPurchases.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(memberItemPurchases);
                    }

                    db.MemberItemPurchases.Add(memberItemPurchases);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MemberItemPurchasesController-Create(memberItemPurchases)";
                    logMessage.Message = JsonConvert.SerializeObject(memberItemPurchases);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(memberItemPurchases);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemPurchasesController-Create(memberItemPurchases)";
                logMessage.Message = JsonConvert.SerializeObject(memberItemPurchases);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: MemberItemPurchases/Edit/5
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
                var result = from mip in db.MemberItemPurchases.ToList()
                    join m in db.Members on mip.MemberID equals m.MemberID
                    join il in db.ItemLists on mip.ItemListID equals il.ItemListID
                    where mip.MemberItemPurchaseID == id        //id값
                    select new MemberItemPurchases()
                    {
                        MemberItemPurchaseID = mip.MemberItemPurchaseID,
                        MemberID = mip.MemberID,
                        ItemListID = mip.ItemListID,
                        PurchasePrice = mip.PurchasePrice,
                        PurchaseQuantity = mip.PurchaseQuantity,
                        PGinfo1 = mip.PGinfo1,
                        PGinfo2 = mip.PGinfo2,
                        PGinfo3 = mip.PGinfo3,
                        PGinfo4 = mip.PGinfo4,
                        PGinfo5 = mip.PGinfo5,
                        PurchaseDeviceID = mip.PurchaseDeviceID,
                        PurchaseDeviceIPAddress = mip.PurchaseDeviceIPAddress,
                        PurchaseDeviceMACAddress = mip.PurchaseDeviceMACAddress,
                        PurchaseDT = mip.PurchaseDT,
                        PurchaseCancelYN = mip.PurchaseCancelYN,
                        PurchaseCancelDT = mip.PurchaseCancelDT,
                        PurchaseCancelingStatus = mip.PurchaseCancelingStatus,
                        PurchaseCancelReturnedAmount = mip.PurchaseCancelReturnedAmount,
                        PurchaseCancelDeviceID = mip.PurchaseCancelDeviceID,
                        PurchaseCancelDeviceIPAddress = mip.PurchaseCancelDeviceIPAddress,
                        PurchaseCancelDeviceMACAddress = mip.PurchaseCancelDeviceMACAddress,
                        sCol1 = mip.sCol1,
                        sCol2 = mip.sCol2,
                        sCol3 = mip.sCol3,
                        sCol4 = mip.sCol4,
                        sCol5 = mip.sCol5,
                        sCol6 = mip.sCol6,
                        sCol7 = mip.sCol7,
                        sCol8 = mip.sCol8,
                        sCol9 = mip.sCol9,
                        sCol10 = mip.sCol10,
                        PurchaseCancelConfirmAdminMemberID = mip.PurchaseCancelConfirmAdminMemberID,
                        HideYN = mip.HideYN,
                        DeleteYN = mip.DeleteYN,
                        CreatedAt = mip.CreatedAt,
                        UpdatedAt = mip.UpdatedAt,
                        DataFromRegion = mip.DataFromRegion,
                        DataFromRegionDT = mip.DataFromRegionDT,

                        ItemLists = new ItemLists()
                        {
                            ItemName = il.ItemName
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
                result1.PurchaseDT = UserTime.GetUserTime(DateTime.Parse(result1.PurchaseDT), Session["AdminTimeZone"].ToString()).ToString();
                result1.PurchaseCancelDT = UserTime.GetUserTime(DateTime.Parse(result1.PurchaseCancelDT), Session["AdminTimeZone"].ToString()).ToString();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberItemPurchasesController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemPurchasesController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: MemberItemPurchases/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberItemPurchaseID,MemberID,ItemListID,PurchaseQuantity,PurchasePrice,PGinfo1,PGinfo2,PGinfo3,PGinfo4,PGinfo5,PurchaseDeviceID,PurchaseDeviceIPAddress,PurchaseDeviceMACAddress,PurchaseDT,PurchaseCancelYN,PurchaseCancelDT,PurchaseCancelingStatus,PurchaseCancelReturnedAmount,PurchaseCancelDeviceID,PurchaseCancelDeviceIPAddress,PurchaseCancelDeviceMACAddress,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,PurchaseCancelConfirmAdminMemberID,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] MemberItemPurchases memberItemPurchases)
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
                    memberItemPurchases.PurchaseDT = UserTime.SetUtcTime(DateTime.Parse(memberItemPurchases.PurchaseDT), Session["AdminTimeZone"].ToString()).ToString();
                    memberItemPurchases.PurchaseCancelDT = UserTime.SetUtcTime(DateTime.Parse(memberItemPurchases.PurchaseCancelDT), Session["AdminTimeZone"].ToString()).ToString();

                    memberItemPurchases.UpdatedAt = DateTimeOffset.UtcNow;
                    //UserTime.SetUtcTime()
                    // 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(memberItemPurchases);
                    }

                    db.Entry(memberItemPurchases).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MemberItemPurchasesController-Edit(memberItemPurchases)";
                    logMessage.Message = JsonConvert.SerializeObject(memberItemPurchases);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(memberItemPurchases);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemPurchasesController-Edit(memberItemPurchases)";
                logMessage.Message = JsonConvert.SerializeObject(memberItemPurchases);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        //// GET: MemberItemPurchases/Delete/5
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
                MemberItemPurchases memberItemPurchases = db.MemberItemPurchases.Find(id);
                if (memberItemPurchases == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberItemPurchasesController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(memberItemPurchases);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemPurchasesController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);
                throw;
            }
            
        }

        // POST: MemberItemPurchases/Delete/5
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

                MemberItemPurchases memberItemPurchases = db.MemberItemPurchases.Find(id);
                db.MemberItemPurchases.Remove(memberItemPurchases);
                db.SaveChanges();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberItemPurchasesController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemPurchasesController-DeleteConfirm(id)";
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
