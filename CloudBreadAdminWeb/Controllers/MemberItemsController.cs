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
    public class MemberItemsController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();

        // 복호화 수행
        public MemberItems DecryptResult(MemberItems item)
        {
            try
            {
                item.MemberItemID = Crypto.AES_decrypt(item.MemberItemID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberID = Crypto.AES_decrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_decrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemListID = Crypto.AES_decrypt(item.ItemListID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemLists.ItemName = Crypto.AES_decrypt(item.ItemLists.ItemName, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCount = Crypto.AES_decrypt(item.ItemCount, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemStatus = Crypto.AES_decrypt(item.ItemStatus, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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

                //result.GetType().GetProperties().ToList().ForEach(p =>
                //{
                //    if (p.Name == "HideYN" || p.Name == "DeleteYN" || p.Name == "CreatedAt" || p.Name == "UpdatedAt" || p.Name == "DataFromRegion" || p.Name == "DataFromRegionDT")       // 복호화 안하고 통과 시킬 녀석들
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
        public MemberItems EncryptResult(MemberItems item)
        {
            try
            {
                item.MemberItemID = Crypto.AES_encrypt(item.MemberItemID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.MemberID = Crypto.AES_encrypt(item.MemberID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.Members.Name1 = Crypto.AES_encrypt(item.Members.Name1, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemListID = Crypto.AES_encrypt(item.ItemListID, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemLists.ItemName = Crypto.AES_encrypt(item.ItemLists.ItemName, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemCount = Crypto.AES_encrypt(item.ItemCount, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                item.ItemStatus = Crypto.AES_encrypt(item.ItemStatus, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
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
                //    // Edit에서 암호화 시작
                //    if (p.Name == "HideYN" || p.Name == "DeleteYN" || p.Name == "CreatedAt" || p.Name == "UpdatedAt" || p.Name == "DataFromRegion" || p.Name == "DataFromRegionDT") 
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
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        // GET
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

                var queryString = from mi in db.MemberItems.ToList()
                                  join il in db.ItemLists on mi.ItemListID equals il.ItemListID
                                  join m in db.Members on mi.MemberID equals m.MemberID
                                  select new MemberItems()
                                  {
                                      MemberItemID = mi.MemberItemID,
                                      MemberID = mi.MemberID,
                                      ItemListID = mi.ItemListID,
                                      ItemCount = mi.ItemCount,
                                      ItemStatus = mi.ItemStatus,
                                      sCol1 = mi.sCol1,
                                      sCol2 = mi.sCol2,
                                      sCol3 = mi.sCol3,
                                      sCol4 = mi.sCol4,
                                      sCol5 = mi.sCol5,
                                      sCol6 = mi.sCol6,
                                      sCol7 = mi.sCol7,
                                      sCol8 = mi.sCol8,
                                      sCol9 = mi.sCol9,
                                      sCol10 = mi.sCol10,
                                      HideYN = mi.HideYN,
                                      DeleteYN = mi.DeleteYN,
                                      CreatedAt = mi.CreatedAt,
                                      UpdatedAt = mi.UpdatedAt,
                                      DataFromRegion = mi.DataFromRegion,
                                      DataFromRegionDT = mi.DataFromRegionDT,
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
                        case "MemberItemID":
                            queryString = queryString.Where(s => s.MemberItemID.Contains(searchString));
                            break;
                        case "MemberID":
                            queryString = queryString.Where(s => s.MemberID.Contains(searchString));
                            break;
                        case "ItemListID":
                            queryString = queryString.Where(s => s.ItemListID.Contains(searchString));
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



                //result = result.Include(x => x.itemList.ItemName);

                //var result = db.MemberItems.SqlQuery("SELECT MemberItems.MemberItemID,MemberItems.MemberID,MemberItems.ItemListID,MemberItems.ItemCount,MemberItems.ItemStatus,MemberItems.sCol1,MemberItems.sCol2,MemberItems.sCol3,MemberItems.sCol4,MemberItems.sCol5,MemberItems.sCol6,MemberItems.sCol7,MemberItems.sCol8,MemberItems.sCol9,MemberItems.sCol10,MemberItems.HideYN,MemberItems.DeleteYN,MemberItems.CreatedAt,MemberItems.UpdatedAt,MemberItems.DataFromRegion,MemberItems.DataFromRegionDT,ItemLists.ItemName as ItemName FROM CloudBread.MemberItems inner join CloudBread.ItemLists on MemberItems.ItemListID = ItemLists.ItemListID").ToList();

                //MemberItems.CreatedAt = UserTime.GetUserTime(MemberItems.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                //MemberItems.UpdatedAt = UserTime.GetUserTime(MemberItems.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                //result = result.ToList<MemberItems>;

                //날자 데이터 현지 시각으로 변환
                foreach (var item in result)
                {
                    item.CreatedAt = UserTime.GetUserTime(item.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                    item.UpdatedAt = UserTime.GetUserTime(item.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberItemsController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                Logging.RunLog(logMessage);

                return View(result);

            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemsController-Index";
                logMessage.Message = string.Format("SearchString : {0} , SearchCondition : {1}", searchString, SearchCondition);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }

        }

        // GET: MemberItems/Details/5
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
                    from mi in db.MemberItems.ToList()
                    join il in db.ItemLists on mi.ItemListID equals il.ItemListID
                    join m in db.Members on mi.MemberID equals m.MemberID
                    where mi.MemberItemID == id        //id값

                    select new MemberItems()
                    {
                        MemberItemID = mi.MemberItemID,
                        MemberID = mi.MemberID,
                        ItemListID = mi.ItemListID,
                        ItemCount = mi.ItemCount,
                        ItemStatus = mi.ItemStatus,
                        sCol1 = mi.sCol1,
                        sCol2 = mi.sCol2,
                        sCol3 = mi.sCol3,
                        sCol4 = mi.sCol4,
                        sCol5 = mi.sCol5,
                        sCol6 = mi.sCol6,
                        sCol7 = mi.sCol7,
                        sCol8 = mi.sCol8,
                        sCol9 = mi.sCol9,
                        sCol10 = mi.sCol10,
                        HideYN = mi.HideYN,
                        DeleteYN = mi.DeleteYN,
                        CreatedAt = mi.CreatedAt,
                        UpdatedAt = mi.UpdatedAt,
                        DataFromRegion = mi.DataFromRegion,
                        DataFromRegionDT = mi.DataFromRegionDT,
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
                result1.CreatedAt = UserTime.GetUserTime(result1.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
                result1.UpdatedAt = UserTime.GetUserTime(result1.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());
                result1.DataFromRegionDT = UserTime.GetUserTime(result1.DataFromRegionDT.DateTime, Session["AdminTimeZone"].ToString());

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberItemsController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemsController-Details";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // GET: MemberItems/Create
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
                logMessage.Logger = "MemberItemsController-Create()";
                logMessage.Message = "";
                Logging.RunLog(logMessage);

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemsController-Create()";
                logMessage.Message = "";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: MemberItems/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberItemID,MemberID,ItemListID,ItemCount,ItemStatus,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] MemberItems memberItems)
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
                    memberItems.CreatedAt = DateTimeOffset.UtcNow;
                    memberItems.UpdatedAt = DateTimeOffset.UtcNow;

                    // Insert : 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(memberItems);
                    }

                    db.MemberItems.Add(memberItems);

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MemberItemsController-Create(memberItems)";
                    logMessage.Message = JsonConvert.SerializeObject(memberItems);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(memberItems);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemsController-Create(memberItems)";
                logMessage.Message = JsonConvert.SerializeObject(memberItems);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // GET: MemberItems/Edit/5
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


                //MemberItems memberItems = db.MemberItems.Find(id);

                //테이블 조인 처리
                var result =
                    from mi in db.MemberItems.ToList()
                    join il in db.ItemLists on mi.ItemListID equals il.ItemListID
                    join m in db.Members on mi.MemberID equals m.MemberID
                    where mi.MemberItemID == id        //id값

                    select new MemberItems()
                    {
                        MemberItemID = mi.MemberItemID,
                        MemberID = mi.MemberID,
                        ItemListID = mi.ItemListID,
                        ItemCount = mi.ItemCount,
                        ItemStatus = mi.ItemStatus,
                        sCol1 = mi.sCol1,
                        sCol2 = mi.sCol2,
                        sCol3 = mi.sCol3,
                        sCol4 = mi.sCol4,
                        sCol5 = mi.sCol5,
                        sCol6 = mi.sCol6,
                        sCol7 = mi.sCol7,
                        sCol8 = mi.sCol8,
                        sCol9 = mi.sCol9,
                        sCol10 = mi.sCol10,
                        HideYN = mi.HideYN,
                        DeleteYN = mi.DeleteYN,
                        CreatedAt = mi.CreatedAt,
                        UpdatedAt = mi.UpdatedAt,
                        DataFromRegion = mi.DataFromRegion,
                        DataFromRegionDT = mi.DataFromRegionDT,
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

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberItemsController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(result1);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemsController-Edit(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            

        }

        // POST: MemberItems/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberItemID,MemberID,ItemListID,ItemCount,ItemStatus,sCol1,sCol2,sCol3,sCol4,sCol5,sCol6,sCol7,sCol8,sCol9,sCol10,HideYN,DeleteYN,CreatedAt,UpdatedAt,DataFromRegion,DataFromRegionDT")] MemberItems memberItems)
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
                    memberItems.UpdatedAt = DateTimeOffset.UtcNow;

                    // 암호화 처리
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        EncryptResult(memberItems);
                    }
                    
                    db.Entry(memberItems).State = EntityState.Modified;

                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();
                    logMessage.Level = "INFO";
                    logMessage.Logger = "MemberItemsController-Edit(memberItems)";
                    logMessage.Message = JsonConvert.SerializeObject(memberItems);
                    Logging.RunLog(logMessage);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(memberItems);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemsController-Edit(memberItems)";
                logMessage.Message = JsonConvert.SerializeObject(memberItems);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // GET: MemberItems/Delete/5
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
                MemberItems memberItems = db.MemberItems.Find(id);
                if (memberItems == null)
                {
                    return HttpNotFound();
                }

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberItemsController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return View(memberItems);
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemsController-Delete(id)";
                logMessage.Message = string.Format("id : {0}", id);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        // POST: MemberItems/Delete/5
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

                MemberItems memberItems = db.MemberItems.Find(id);
                db.MemberItems.Remove(memberItems);
                db.SaveChanges();

                // 관리자 접근 로그 
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "INFO";
                logMessage.Logger = "MemberItemsController-DeleteConfirm(id)";
                logMessage.Message = string.Format("id : {0}", id);
                Logging.RunLog(logMessage);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = this.Session["AdminID"].ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "MemberItemsController-DeleteConfirm(id)";
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
