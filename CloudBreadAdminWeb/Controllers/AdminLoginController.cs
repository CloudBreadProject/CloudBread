using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CloudBreadAdminWeb.AdminMemberLogin;
using CloudBreadAdminWeb.AdminMemberLogout;

//추가
using System.Threading.Tasks;
using CloudBreadAdminWeb;
using System.Diagnostics;
using CloudBreadAdminWeb.globals;
using CloudBreadLib.BAL.Crypto;
using CloudBreadLib.BAL.UserTime;
using Logger.Logging;
using Newtonsoft.Json;

namespace CloudBreadAdminWeb.AdminMemberLogin.Models.Controllers
{
    public class AdminLoginController : Controller
    {
        // 로거 개체 생성
        Logging.CBLoggers logMessage = new Logging.CBLoggers();


        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }
 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.AdminMemberLogin user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<AdminMemberLogin.Model> result = user.CBAdminLogin(user.UserName, user.Password, Request.ServerVariables["REMOTE_ADDR"]);
                    if (result.Count != 0)
                    {
                        if (result[0].AdminMemberID != null)
                        {
                        Session.Add("AdminID", result[0].AdminMemberID.ToString());
                        Session.Add("AdminGroup", result[0].AdminGroup.ToString());
                        Session.Add("AdminTimeZone", result[0].TimeZoneID.ToString());

                        // 관리자 접근 로그 
                        logMessage.memberID = this.Session["AdminID"].ToString();
                        logMessage.Level = "INFO";
                        logMessage.Logger = "AdminLoginController-Login(id)";
                        logMessage.Message = string.Format("ID : {0}", user.UserName);
                        Logging.RunLog(logMessage);

                        return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "로그인 정보가 틀립니다.");
                        }
                    }
                    ModelState.AddModelError("", "로그인 정보가 틀립니다.");
                    
                }
                return View(user);

            }
            catch (Exception ex)
            {
                //에러로그
                if (this.Session["AdminID"] != null)
                {
                    logMessage.memberID = this.Session["AdminID"].ToString();
                }
                else
                {
                    logMessage.memberID = "";
                }
                logMessage.memberID = (this.Session["AdminID"] ?? "").ToString();
                logMessage.Level = "ERROR";
                logMessage.Logger = "AdminLoginController-Login(id)";
                logMessage.Message = string.Format("ID : {0}", user.UserName);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
            
        }

        [HttpGet]
        public ActionResult Logout(CloudBreadAdminWeb.AdminMemberLogout.Models.AdminMemberLogout Admin)
        {
            Admin.AdminMemberID = Session["AdminID"].ToString();
            try
            {
                // Logout 시각 입력 필요
                string result = Admin.CBAdminLogout(Admin.AdminMemberID).ToString();
                if (result != "0")
                {
                    // 관리자 접근 로그 
                    logMessage.memberID = this.Session["AdminID"].ToString();   
                    logMessage.Level = "INFO";
                    logMessage.Logger = "AdminLoginController-Logout(id)";
                    logMessage.Message = string.Format("ID : {0}", Admin.AdminMemberID);
                    Logging.RunLog(logMessage);

                    Session.Clear();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "로그아웃 실패");
                }

                return View();
            }
            catch (Exception ex)
            {
                //에러로그
                if (this.Session["AdminID"] != null)
                {
                    logMessage.memberID = this.Session["AdminID"].ToString();
                }
                else
                {
                    logMessage.memberID = "";
                }
                logMessage.Level = "ERROR";
                logMessage.Logger = "AdminLoginController-Login(id)";
                logMessage.Message = string.Format("ID : {0}", Admin.AdminMemberID);
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);
                throw;
            }
            
        }

    }
}