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
    public class GiftDepositoriesController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 복호화 수행
        public GiftDepositories DecryptResult(GiftDepositories item)
        {
            try
            {
                item.GiftDepositoryID = Crypto.AES_decrypt(item.GiftDepositoryID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemListID = Crypto.AES_decrypt(item.ItemListID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCount = Crypto.AES_decrypt(item.ItemCount, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.FromMemberID = Crypto.AES_decrypt(item.FromMemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_decrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ToMemberID = Crypto.AES_decrypt(item.ToMemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members1.Name1 = Crypto.AES_decrypt(item.Members1.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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
        public GiftDepositories EncryptResult(GiftDepositories item)
        {
            try
            {
                item.GiftDepositoryID = Crypto.AES_encrypt(item.GiftDepositoryID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemListID = Crypto.AES_encrypt(item.ItemListID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCount = Crypto.AES_encrypt(item.ItemCount, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.FromMemberID = Crypto.AES_encrypt(item.FromMemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_encrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ToMemberID = Crypto.AES_encrypt(item.ToMemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members1.Name1 = Crypto.AES_encrypt(item.Members1.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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



        // GET: GiftDepositories
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

                var queryString = from gd in db.GiftDepositories.ToList()
                                  join il in db.ItemLists on gd.ItemListID equals il.ItemListID
                                  join m in db.Members on gd.FromMemberID equals m.MemberID
                                  join m2 in db.Members on gd.ToMemberID equals m2.MemberID
                                  select new GiftDepositories()
                                  {
                                      GiftDepositoryID = gd.GiftDepositoryID,
                                      ItemListID = gd.ItemListID,
                                      ItemCount = gd.ItemCount,
                                      FromMemberID = m.MemberID,
                                      ToMemberID = m2.MemberID,
                                      sCol1 = gd.sCol1,
                                      sCol2 = gd.sCol2,
                                      sCol3 = gd.sCol3,
                                      sCol4 = gd.sCol4,
                                      sCol5 = gd.sCol5,
                                      sCol6 = gd.sCol6,
                                      sCol7 = gd.sCol7,
                                      sCol8 = gd.sCol8,
                                      sCol9 = gd.sCol9,
                                      sCol10 = gd.sCol10,
                                      SentMemberYN = gd.SentMemberYN,
                                      HideYN = gd.HideYN,
                                      DeleteYN = gd.DeleteYN,
                                      CreatedAt = gd.CreatedAt,
                                      UpdatedAt = gd.UpdatedAt,
                                      DataFromRegion = gd.DataFromRegion,
                                      DataFromRegionDT = gd.DataFromRegionDT,
                                      ItemLists = new ItemLists()
                                      {
                                          ItemName = il.ItemName
                                      },
                                      Members = new Members()
                                      {
                                          Name1 = m.Name1
                                      },
                                      Members1 = new Members()
                                      {
                                          Name1 = m2.Name1
                                      }
                                  };

                if (!String.IsNullOrEmpty(searchString))
                {
                    switch (SearchCondition)
                    {
                        case "GiftDepositoryID":
                            queryString = queryString.Where(s => s.GiftDepositoryID.Contains(searchString));
                            break;
                        case "ItemListID":
                            queryString = queryString.Where(s => s.ItemListID.Contains(searchString));
                            break;
                        case "FromMemberID":
                            queryString = queryString.Where(s => s.FromMemberID.Contains(searchString));
                            break;
                        case "ToMemberID":
                            queryString = queryString.Where(s => s.ToMemberID.Contains(searchString));
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
                logMessage.Logger = "GiftDepositoriesController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);

            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GiftDepositoriesController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: GiftDepositories/Details/5
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
                    from gd in db.GiftDepositories.ToList()
                    join il in db.ItemLists on gd.ItemListID equals il.ItemListID
                    join m in db.Members on gd.FromMemberID equals m.MemberID
                    join m2 in db.Members on gd.ToMemberID equals m2.MemberID
                    where gd.GiftDepositoryID == id        //id값
                    select new GiftDepositories()
                    {
                        GiftDepositoryID = gd.GiftDepositoryID,
                        ItemListID = gd.ItemListID,
                        ItemCount = gd.ItemCount,
                        FromMemberID = m.MemberID,
                        ToMemberID = m2.MemberID,
                        sCol1 = gd.sCol1,
                        sCol2 = gd.sCol2,
                        sCol3 = gd.sCol3,
                        sCol4 = gd.sCol4,
                        sCol5 = gd.sCol5,
                        sCol6 = gd.sCol6,
                        sCol7 = gd.sCol7,
                        sCol8 = gd.sCol8,
                        sCol9 = gd.sCol9,
                        sCol10 = gd.sCol10,
                        SentMemberYN = gd.SentMemberYN,
                        HideYN = gd.HideYN,
                        DeleteYN = gd.DeleteYN,
                        CreatedAt = gd.CreatedAt,
                        UpdatedAt = gd.UpdatedAt,
                        DataFromRegion = gd.DataFromRegion,
                        DataFromRegionDT = gd.DataFromRegionDT,
                        ItemLists = new ItemLists()
                        {
                            ItemName = il.ItemName
                        },
                        Members = new Members()
                        {
                            Name1 = m.Name1
                        },
                        Members1 = new Members()
                        {
                            Name1 = m2.Name1
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
                logMessage.Logger = "GiftDepositoriesController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GiftDepositoriesController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: GiftDepositories/Create
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
                logMessage.Logger = "GiftDepositoriesController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GiftDepositoriesController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // POST: GiftDepositories/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GiftDepositoryID,ItemListID,ItemCount,FromMemberID,ToMemberID,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,SentMemberYN,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] GiftDepositories giftDepositories)
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
                    giftDepositories.CreatedAt = DateTimeOffset.UtcNow;
                    giftDepositories.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(giftDepositories);
                    }

                    db.GiftDepositories.Add(giftDepositories);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "GiftDepositoriesController-Create(giftDepositories)";
                    logMessage.Message = JsonConvert.SerializeObject(giftDepositories);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(giftDepositories);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GiftDepositoriesController-Create(giftDepositories)";
                logMessage.Message = JsonConvert.SerializeObject(giftDepositories);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        // GET: GiftDepositories/Edit/5
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
                    from gd in db.GiftDepositories.ToList()
                    join il in db.ItemLists on gd.ItemListID equals il.ItemListID
                    join m in db.Members on gd.FromMemberID equals m.MemberID
                    join m2 in db.Members on gd.ToMemberID equals m2.MemberID
                    where gd.GiftDepositoryID == id        //id값
                    select new GiftDepositories()
                    {
                        GiftDepositoryID = gd.GiftDepositoryID,
                        ItemListID = gd.ItemListID,
                        ItemCount = gd.ItemCount,
                        FromMemberID = m.MemberID,
                        ToMemberID = m2.MemberID,
                        sCol1 = gd.sCol1,
                        sCol2 = gd.sCol2,
                        sCol3 = gd.sCol3,
                        sCol4 = gd.sCol4,
                        sCol5 = gd.sCol5,
                        sCol6 = gd.sCol6,
                        sCol7 = gd.sCol7,
                        sCol8 = gd.sCol8,
                        sCol9 = gd.sCol9,
                        sCol10 = gd.sCol10,
                        SentMemberYN = gd.SentMemberYN,
                        HideYN = gd.HideYN,
                        DeleteYN = gd.DeleteYN,
                        CreatedAt = gd.CreatedAt,
                        UpdatedAt = gd.UpdatedAt,
                        DataFromRegion = gd.DataFromRegion,
                        DataFromRegionDT = gd.DataFromRegionDT,
                        ItemLists = new ItemLists()
                        {
                            ItemName = il.ItemName
                        },
                        Members = new Members()
                        {
                            Name1 = m.Name1
                        },
                        Members1 = new Members()
                        {
                            Name1 = m2.Name1
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
                logMessage.Logger = "GiftDepositoriesController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GiftDepositoriesController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // POST: GiftDepositories/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GiftDepositoryID,ItemListID,ItemCount,FromMemberID,ToMemberID,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,SentMemberYN,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] GiftDepositories giftDepositories)
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
                    giftDepositories.UpdatedAt = DateTimeOffset.UtcNow;

                    // 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(giftDepositories);
                    }

                    db.Entry(giftDepositories).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "GiftDepositoriesController-Edit(giftDepositories)";
                    logMessage.Message = JsonConvert.SerializeObject(giftDepositories);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(giftDepositories);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GiftDepositoriesController-Edit(giftDepositories)";
                logMessage.Message = JsonConvert.SerializeObject(giftDepositories);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        //// GET: GiftDepositories/Delete/5
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
                GiftDepositories giftDepositories = db.GiftDepositories.Find(id);
                if (giftDepositories == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "GiftDepositoriesController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(giftDepositories);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GiftDepositoriesController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: GiftDepositories/Delete/5
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

                GiftDepositories giftDepositories = db.GiftDepositories.Find(id);
                db.GiftDepositories.Remove(giftDepositories);
                db.SaveChanges();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "GiftDepositoriesController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "GiftDepositoriesController-DeleteConfirm(id)";
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
