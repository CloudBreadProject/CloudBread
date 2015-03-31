using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CloudBreadAdminWeb.globals
{
    public static class globalVal
    {
        public static string DBConnectionString = WebConfigurationManager.ConnectionStrings["CloudBreadDBConString"].ConnectionString;
        public static string StorageConnectionString = WebConfigurationManager.ConnectionStrings["CloudBreadStorageConString"].ConnectionString;
        public static string CloudBreadLoggerSetting = WebConfigurationManager.AppSettings["CloudBreadLoggerSetting"].ToString();
        public static string CloudBreadCryptSetting = WebConfigurationManager.AppSettings["CloudBreadCryptSetting"].ToString();
        public static string CloudBreadCryptKey = WebConfigurationManager.AppSettings["CloudBreadCryptKey"].ToString();       // 16자가 아니면 padding 처리
        public static string CloudBreadCryptIV = WebConfigurationManager.AppSettings["CloudBreadCryptIV"].ToString();     // 16자가 아니면 padding 처리
        public static int CloudBreadAdminWebListPageSize = int.Parse(WebConfigurationManager.AppSettings["CloudBreadAdminWebListPageSize"].ToString());
    }
}