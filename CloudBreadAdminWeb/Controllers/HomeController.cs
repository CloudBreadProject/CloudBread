using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//추가
//using System.Web.Helpers;       //Chart 처리    - 모호한 처리라 
using CloudBreadLib.BAL.Crypto;     // Helper의 crypto와 겹친다.
using System.Threading.Tasks;
using CloudBreadAdminWeb;
using System.Diagnostics;
using PagedList;
using CloudBreadAdminWeb.globals;
using CloudBreadLib.BAL.UserTime;
using Logger.Logging;

namespace CloudBreadAdminWeb.Controllers
{
    
  
    // [RequireHttps]
    public class HomeController : Controller
    {

        public void CheckSession()
        {
            string strSession = (this.Session["AdminGroup"] ?? "").ToString();
            //특수 HOME Contorller 임. 로그인만 되면 Reader 세션도 통과
            if (strSession != "Admin" && strSession != "Operator" && strSession != "Reader")
            {
                Session.Add("LoginAlert", "로그인 하지 않았거나 접근 권한이 부족합니다.");
                Response.Redirect("/AdminLogin/Login");
            }
        }

        public ActionResult Index()
        {
            // 개발 목적 / localhost일 경우 자동 세션 처리
            //if (Request.Url.DnsSafeHost.ToString() == "localhost")
            //{
            //    Session.Add("AdminID", "CBAdmin");
            //    Session.Add("AdminGroup", "Admin");
            //    Session.Add("AdminTimeZone", "Korea Standard Time");        // 타임존 : 관리자의 timezone에서 입력
            //}

            // Index 세션체크
            CheckSession();

            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        //테스트 차트
        //public ActionResult GetChartImage()
        //{
        //    var key = new System.Web.Helpers.Chart(width: 300, height: 300)     //helper의 crypto와 CloudBreadLib의 crypto가 모호한 참조. 
        //        .AddTitle("DAU-시간")
        //        .AddSeries(
        //        chartType: "Line",
        //        name: "Employee",
        //        xValue: new[] { "Peter", "Andrew", "Julie", "Dave" },
        //        yValues: new[] { "2", "7", "5", "3" });

        //    return File(key.ToWebImage().GetBytes(), "image/jpeg");
        //}

        public CloudBreadDBAdminEntities db = new CloudBreadDBAdminEntities();

        public ActionResult GetHDAUChartImage()
        {
            // DB 접속
            using (var db = new CloudBreadDBAdminEntities())
            {
                var data = db.StatsData.SqlQuery("select top 24 * from CloudBread.StatsData where CategoryName like 'HDAU' order by CreatedAt asc");

                var key = new System.Web.Helpers.Chart(width: 300, height: 300)
                .AddTitle("DAU-시간")
                .AddSeries(
                chartType: "Line",
                name: "시간",
                xValue: data, xField: "Fields",
                yValues: data,yFields:"CountNum");

            return File(key.ToWebImage().GetBytes(), "image/jpeg");
            }
        }

        public ActionResult GetDDAUChartImage()
        {
            // DB 접속
            using (var db = new CloudBreadDBAdminEntities())
            {
                var data = db.StatsData.SqlQuery("select top 30 * from CloudBread.StatsData where CategoryName like 'DDAU' order by CreatedAt asc");

                var key = new System.Web.Helpers.Chart(width: 300, height: 300)
                    .AddTitle("DAU-일")
                    .AddSeries(
                    chartType: "Line",
                    name: "일",
                    xValue: data, xField: "Fields",
                    yValues: data, yFields: "CountNum");

                return File(key.ToWebImage().GetBytes(), "image/jpeg");
            }
        }

        public ActionResult GetCashItemChartImage()
        {
            // DB 접속
            using (var db = new CloudBreadDBAdminEntities())
            {
                var data = db.StatsData.SqlQuery("select top 30 * from CloudBread.StatsData where CategoryName like 'CASHITEM' order by CreatedAt asc");

                var key = new System.Web.Helpers.Chart(width: 300, height: 300)
                    .AddTitle("Cash아이템 매출")
                    .AddSeries(
                    chartType: "Line",
                    name: "일",
                    xValue: data, xField: "Fields",
                    yValues: data, yFields: "CountNum");

                return File(key.ToWebImage().GetBytes(), "image/jpeg");
            }
        }


    }
}