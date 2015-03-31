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
    public class ItemListsController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        //복호화 처리
        //public IPagedList<ItemLists> DecryptResult(IPagedList<ItemLists> result)
        //{
        //    foreach (var item in result)    //List의 경우
        //    {
        //        //리플렉션 쓰지 말자....
        //        item.GetType().GetProperties().ToList().ForEach(p =>
        //        {
        //            if (p.Name == "IteamUpdateAdminID" || p.Name == "IteamCreateAdminID" || p.Name == "HideYN" || p.Name == "DeleteYN" || p.Name == "CreatedAt" || p.Name == "UpdatedAt" || p.Name == "DataFromRegion" || p.Name == "DataFromRegionDT")       // 복호화 안하고 통과 시킬 녀석들
        //            {
        //                // 추가 처리
        //            }
        //            else
        //            {
        //                Debug.WriteLine((p.GetValue(item, null) ?? "").ToString());
        //                p.SetValue(item, Crypto.AES_decrypt(p.GetValue(item, null).ToString(), globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV), null);   // null인 경우 오류, DB기본값 체크 필요
        //                Debug.WriteLine((p.GetValue(item, null) ?? "").ToString());
        //            }
        //        });
        //    }

        //    return result;

        //}

        // 개별 entity 복호화
        public ItemLists DecryptResult(ItemLists item)   
        {
            try
            {
                item.ItemListID = Crypto.AES_decrypt(item.ItemListID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemName = Crypto.AES_decrypt(item.ItemName, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemDescription = Crypto.AES_decrypt(item.ItemDescription, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemPrice = Crypto.AES_decrypt(item.ItemPrice, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemSellPrice = Crypto.AES_decrypt(item.ItemSellPrice, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCategory1 = Crypto.AES_decrypt(item.ItemCategory1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCategory2 = Crypto.AES_decrypt(item.ItemCategory2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCategory3 = Crypto.AES_decrypt(item.ItemCategory3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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

                //리플렉션 안쓴다.
                //result.GetType().GetProperties().ToList().ForEach(p =>
                //{
                //    if (p.Name == "IteamUpdateAdminID" || p.Name == "IteamCreateAdminID" || p.Name == "HideYN" || p.Name == "DeleteYN" || p.Name == "CreatedAt" || p.Name == "UpdatedAt" || p.Name == "DataFromRegion" || p.Name == "DataFromRegionDT")       // 복호화 안하고 통과 시킬 녀석들
                //    {
                //        // 추가 처리
                //    }
                //    else
                //    {
                //        p.SetValue(result, Crypto.AES_decrypt((p.GetValue(result, null) ?? "").ToString(), globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV), null);
                //    }
                //});
            }
            catch (Exception)
            {
                
                throw;
            }

            return item;

        }

        //암호화 처리
        public ItemLists EncryptResult(ItemLists item)
        {
            try
            {
                item.ItemListID = Crypto.AES_encrypt(item.ItemListID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemName = Crypto.AES_encrypt(item.ItemName, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemDescription = Crypto.AES_encrypt(item.ItemDescription, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemPrice = Crypto.AES_encrypt(item.ItemPrice, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemSellPrice = Crypto.AES_encrypt(item.ItemSellPrice, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCategory1 = Crypto.AES_encrypt(item.ItemCategory1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCategory2 = Crypto.AES_encrypt(item.ItemCategory2, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCategory3 = Crypto.AES_encrypt(item.ItemCategory3, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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

                //result.GetType().GetProperties().ToList().ForEach(p =>
                //{
                //    // 암호화 안할 항복 체크
                //    if (p.Name == "IteamUpdateAdminID" || p.Name == "IteamCreateAdminID" || p.Name == "HideYN" || p.Name == "DeleteYN" || p.Name == "CreatedAt" || p.Name == "UpdatedAt" || p.Name == "DataFromRegion" || p.Name == "DataFromRegionDT") 
                //    {
                //        // 추가 처리
                //    }
                //    else
                //    {
                //        Debug.WriteLine(p.PropertyType.FullName);
                //        Debug.WriteLine((p.GetValue(result, null) ?? "").ToString());
                //        p.SetValue(result, Crypto.AES_encrypt((p.GetValue(result, null) ?? "").ToString(), globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV), null);   // null인 경우 오류 가능성. 빈문자열로 치환해 암호화 한다.
                //    }
                //});
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
                return true ;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        // GET: ItemLists
        public ActionResult Index(string searchString, string SearchCondition, string currentFilter, int? page)  // 검색 방식 테스트 + 페이징
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

                var queryString = from s in db.ItemLists
                                  select s;

                //from il in db.ItemLists
                //join mi in db.MemberItems
                //on il.ItemListID equals mi.ItemListID
                //select new{il.ItemListID, il.ItemName, il.ItemDescription, il.ItemPrice, il.ItemSellPrice, il.ItemCategory1, il.ItemCategory2, il.ItemCategory3, mi.MemberItemID, mi.MemberID};
                if (!String.IsNullOrEmpty(searchString))
                {
                    switch (SearchCondition)
                    {
                        case "ItemID":
                            queryString = queryString.Where(s => s.ItemListID.Contains(searchString));
                            break;
                        case "ItemName":
                            queryString = queryString.Where(s => s.ItemName.Contains(searchString));
                            break;
                        case "ItemDescription":
                            queryString = queryString.Where(s => s.ItemDescription.Contains(searchString));
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
                logMessage.Logger = "ItemListsController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ItemListsController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // GET: ItemLists/Details/5
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
                ItemLists itemLists = db.ItemLists.Find(id);

                if (itemLists == null)
                {
                    return HttpNotFound();
                }

                //복호화 처리
                if (globalVal.CloudBreadCryptSetting == "AES256")
                {
                    DecryptResult(itemLists);       //ItemLists 타입
                }

                //UTC를 세션의 UserTimeZone으로 변환 - 안하면 UTC 그대로 보임
                //암호화된 컬럼 값에 대한 변환
                //members.LastLoginDT = UserTime.GetUserTime(DateTime.Parse(members.LastLoginDT), Session["AdminTimeZone"].ToString()).ToString();

                itemLists.CreatedAt = UserTime.GetUserTime(itemLists.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                itemLists.UpdatedAt = UserTime.GetUserTime(itemLists.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                itemLists.DataFromRegionDT = UserTime.GetUserTime(itemLists.DataFromRegionDT.DateTime, Session["AdminTimeZone"].ToString());

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "ItemListsController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(itemLists);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ItemListsController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // GET: ItemLists/Create
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
                logMessage.Logger = "ItemListsController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ItemListsController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // POST: ItemLists/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemListID,ItemName,ItemDescription,ItemPrice,ItemSellPrice,ItemCategory1,ItemCategory2,ItemCategory3,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,IteamCreateAdminID,IteamUpdateAdminID,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] ItemLists itemLists)
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
                    itemLists.IteamCreateAdminID = this.Session["AdminID"].ToString();
                    itemLists.IteamUpdateAdminID = this.Session["AdminID"].ToString();

                    itemLists.CreatedAt = DateTimeOffset.UtcNow;
                    itemLists.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        //암호화
                        EncryptResult(itemLists);
                    }

                    db.ItemLists.Add(itemLists);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "ItemListsController-Create(itemLists)";
                    logMessage.Message = JsonConvert.SerializeObject(itemLists);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(itemLists);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ItemListsController-Create(itemLists)";
                logMessage.Message = JsonConvert.SerializeObject(itemLists);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // GET: ItemLists/Edit/5
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
                ItemLists itemLists = db.ItemLists.Find(id);
                if (itemLists == null)
                {
                    return HttpNotFound();
                }

                //복호화 처리
                if (globalVal.CloudBreadCryptSetting == "AES256")
                {
                    DecryptResult(itemLists);
                }

                //UTC를 세션의 UserTimeZone으로 변환 - 안하면 UTC 그대로 보임

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "ItemListsController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(itemLists);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ItemListsController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: ItemLists/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemListID,ItemName,ItemDescription,ItemPrice,ItemSellPrice,ItemCategory1,ItemCategory2,ItemCategory3,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,IteamCreateAdminID,IteamUpdateAdminID,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] ItemLists itemLists)
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
                        EncryptResult(itemLists);
                    }

                    // Edit 입력값 자동처리
                    itemLists.IteamUpdateAdminID = this.Session["AdminID"].ToString();
                    itemLists.UpdatedAt = DateTimeOffset.UtcNow;

                    db.Entry(itemLists).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "ItemListsController-Edit(itemLists)";
                    logMessage.Message = JsonConvert.SerializeObject(itemLists);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(itemLists);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ItemListsController-Edit(itemLists)";
                logMessage.Message = JsonConvert.SerializeObject(itemLists);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // GET: ItemLists/Delete/5
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
                ItemLists itemLists = db.ItemLists.Find(id);
                if (itemLists == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "ItemListsController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(itemLists);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ItemListsController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: ItemLists/Delete/5
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

                ItemLists itemLists = db.ItemLists.Find(id);
                db.ItemLists.Remove(itemLists);
                db.SaveChanges();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "ItemListsController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "ItemListsController-DeleteConfirm(id)";
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
