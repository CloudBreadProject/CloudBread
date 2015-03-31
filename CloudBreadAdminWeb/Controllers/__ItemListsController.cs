using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Diagnostics;
using PagedList;
using CloudBreadAdminWeb;
using CloudBreadAdminWeb.globals;
using CloudBreadLib.BAL.Crypto;
using CloudBreadLib.BAL.UserTime;

namespace CloudBreadAdminWeb.Controllers
{
    public class __ItemListsController : Controller
    {
        private CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        
        //복호화 처리
        public IPagedList<ItemLists> DecryptResult(IPagedList<ItemLists> result)
        {
            foreach (var item in result)
            {
                item.GetType().GetProperties().ToList().ForEach(p =>
                {
                    if (p.Name == "IteamUpdateAdminID" || p.Name == "IteamCreateAdminID" || p.Name == "Id" || p.Name == "Version" || p.Name == "Deleted" || p.Name == "CreatedAt" || p.Name == "UpdatedAt" || p.Name == "HideYN" || p.Name == "DeleteYN")       // 복호화 안하고 통과 시킬 녀석들
                    {
                        // 추가 처리
                    }
                    else
                    {
                        Debug.WriteLine((p.GetValue(item, null) ?? "").ToString());
                        p.SetValue(item, Crypto.AES_decrypt(p.GetValue(item, null).ToString(), globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV), null);   // null인 경우 오류, DB기본값 체크 필요
                        Debug.WriteLine((p.GetValue(item, null) ?? "").ToString());
                    }
                });
            }

            return result;

        }

        public ItemLists DecryptResult(ItemLists result)
        {
            result.GetType().GetProperties().ToList().ForEach(p =>
                {
                    if (p.Name == "IteamUpdateAdminID" || p.Name == "IteamCreateAdminID" || p.Name == "Id" || p.Name == "Version" || p.Name == "Deleted" || p.Name == "CreatedAt" || p.Name == "UpdatedAt" || p.Name == "HideYN" || p.Name == "DeleteYN")       // 복호화 안하고 통과 시킬 녀석들
                    {
                        // 추가 처리
                    }
                    else
                    {
                        //Debug.WriteLine((p.GetValue(result, null) ?? "").ToString());
                        p.SetValue(result, Crypto.AES_decrypt((p.GetValue(result, null) ?? "").ToString(), globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV), null);
                        //Debug.WriteLine((p.GetValue(result, null) ?? "").ToString());
                    }
                });

            return result;

        }

        public ItemLists EncryptResult(ItemLists result)
        {
            result.GetType().GetProperties().ToList().ForEach(p =>
            {
                // Edit이나 Create의 경우는 필드를 체크할 필요 없음. - EF의 특성임.

                // Edit에서 암호화 시작
                if (p.Name == "IteamUpdateAdminID" || p.Name == "IteamCreateAdminID" || p.Name == "Id" || p.Name == "Version" || p.Name == "Deleted" || p.Name == "CreatedAt" || p.Name == "UpdatedAt" || p.Name == "HideYN" || p.Name == "DeleteYN")       // 현재 암호화 안하고 통과 시킬 녀석들
                {
                    // 추가 처리
                }
                else
                {
                    // edit을 저장하려 할 경우 EF에서 빈문자열의 경우 null이 들어오고 처리 되지 않는다. 
                    // view에서 annotation?
                    // EF의 특성

                    // string이 아닌 datetimeoffset 등에서도 오류가 발생한다. int도 발생하겠지.
                    // reflection 쓰지 말까...
                    Debug.WriteLine(p.PropertyType.FullName);
                    Debug.WriteLine((p.GetValue(result, null) ?? "").ToString());
                    
                    p.SetValue(result, Crypto.AES_encrypt((p.GetValue(result, null) ?? "").ToString(), globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV), null);   // null인 경우 오류 가능성. 빈문자열로 치환해 암호화 한다.

                    //switch (p.PropertyType.FullName)
                    //{
                    //    case "System.String":
                    //        p.SetValue(result, Crypto.AES_encrypt((p.GetValue(result, null) ?? "").ToString(), globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV), null);   // null인 경우 오류 가능성. 빈문자열로 치환해 암호화 한다.
                    //        Debug.WriteLine((p.GetValue(result, null) ?? "").ToString());
                    //        break;
                    //    case "System.DateTimeOffset":
                    //        // 날짜 형식의 필드. 암호화 안한다. 이때도 변환하지 않고 통과.    
                    //        //p.SetValue(result, Crypto.AES_encrypt(p.GetValue(result, null), globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV), null);   // null인 경우 오류 가능성
                    //        //Debug.WriteLine((p.GetValue(result, null) ?? "").ToString());
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
                //p.SetValue(result, Crypto.AES_encrypt(p.GetValue(result, null).ToString(), globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV), null);   // null인 경우 오류 가능성
                //Debug.WriteLine((p.GetValue(result, null) ?? "").ToString());
                
            });

            return result;

        }



        // GET: ItemLists
        //public ActionResult Index(string sortOrder)   // 정렬 방식 테스트
        public ActionResult Index(string searchString, string SearchCondition, string currentFilter, int? page)  // 검색 방식 테스트 + 페이징
        {
            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "itemname_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            // 정렬 처리
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "ItemName" : "";
            //var result = from s in db.ItemLists
            //               select s;
            //switch (sortOrder)
            //{
            //    case "ItemName":
            //        result = result.OrderBy(s => s.ItemName);
            //        break;
            //    default:
            //        result = result.OrderBy(s => s.ItemName);
            //        break;
            //}

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
                    case "ItemDescription" :
                        queryString = queryString.Where(s => s.ItemDescription.Contains(searchString));
                        break;
                    default:
                        break;
                }   
                //result = result.Where(s => s.ItemName.Contains(searchString)
                //                       || s.ItemListID.Contains(searchString)
                //                        || s.ItemDescription.Contains(searchString));
            }

            //switch (sortOrder)
            //{
            //    case "itemname_desc":
            //        result = result.OrderByDescending(s => s.ItemName);
            //        break;
            //    default:  // Name ascending 
            //        result = result.OrderByDescending(s => s.ItemName);
            //        break;
            //}



            // 기본 order 처리 - ItemName으로 정렬 처리 - ToPagedList의 제약 조건
            queryString = queryString.OrderBy(s => s.ItemName);

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            //IQueryable로 할까 리포지토리 IEnumerable로 할까.
            // ToPagedList에 제약이 있네. IQueryable

            var result = queryString.ToPagedList(pageNumber, pageSize);
            // 데이터 복호화를 Controller에서 하는게 좋을까 View에서 하는게 좋을까. 

            // result에 대해 복호화 작업 수행. reflection? 
            //int i = 0;
            if (globalVal.CloudBreadCryptSetting == "AES256")
            {
                DecryptResult(result);      //PagedList 타입
            }

            
            //return View(queryString.ToPagedList(pageNumber, pageSize)); 
            return View(result); 
            
        }

        // GET: ItemLists/Details/5
        public ActionResult Details(string id)
        {
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
                DecryptResult(itemLists);       //ItemLists 타입
            }

            //UTC를 UserTimeZone으로 변환
            itemLists.CreatedAt = UserTime.GetUserTime(itemLists.CreatedAt.DateTime, Session["AdminTimeZone"].ToString());
            itemLists.UpdatedAt = UserTime.GetUserTime(itemLists.UpdatedAt.DateTime, Session["AdminTimeZone"].ToString());

            return View(itemLists);
        }

        // GET: ItemLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemLists/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemListID, ItemName, ItemDescription, ItemPrice, ItemSellPrice, ItemCategory1, ItemCategory2, ItemCategory3, IteamCreateAdminID, IteamUpdateAdminID, HideYN, DeleteYN, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, CreatedAt, UpdatedAt")] ItemLists itemLists)
        {
            if (ModelState.IsValid)
            {
                db.ItemLists.Add(itemLists);

                // 사용자에게 보여준 시각을 UTC로 변환
                itemLists.CreatedAt = itemLists.CreatedAt.UtcDateTime;
                itemLists.UpdatedAt = itemLists.UpdatedAt.UtcDateTime;

                // Insert : 암호화 처리
                if (globalVal.CloudBreadCryptSetting == "AES256")
                {
                    EncryptResult(itemLists);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itemLists);
        }

        // GET: ItemLists/Edit/5
        public ActionResult Edit(string id)
        {
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

            return View(itemLists);
        }

        // POST: ItemLists/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemListID, ItemName, ItemDescription, ItemPrice, ItemSellPrice, ItemCategory1, ItemCategory2, ItemCategory3, IteamCreateAdminID, IteamUpdateAdminID, HideYN, DeleteYN, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, CreatedAt, UpdatedAt")] ItemLists itemLists)
        {
            if (ModelState.IsValid)
            {
                if (globalVal.CloudBreadCryptSetting == "AES256")
                {
                    EncryptResult(itemLists);
                }

                db.Entry(itemLists).State = EntityState.Modified;

                //암호화 처리 후 저장
                

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itemLists);
        }

        // GET: ItemLists/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLists itemLists = db.ItemLists.Find(id);
            if (itemLists == null)
            {
                return HttpNotFound();
            }
            return View(itemLists);
        }

        // POST: ItemLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ItemLists itemLists = db.ItemLists.Find(id);
            db.ItemLists.Remove(itemLists);
            db.SaveChanges();
            return RedirectToAction("Index");
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
