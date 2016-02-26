using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CloudBread.globals
{
    public static class globalVal
    {
        public static string DBConnectionString = WebConfigurationManager.ConnectionStrings["CloudBreadDBConString"].ConnectionString;
        public static string StorageConnectionString = WebConfigurationManager.ConnectionStrings["CloudBreadStorageConString"].ConnectionString;
        public static string CloudBreadLoggerSetting = WebConfigurationManager.AppSettings["CloudBreadLoggerSetting"].ToString();
        public static string CloudBreadCryptSetting = WebConfigurationManager.AppSettings["CloudBreadCryptSetting"].ToString();
        public static int conRetryCount = int.Parse(WebConfigurationManager.AppSettings["CloudBreadconRetryCount"]);    /// adding v2.0.0
        public static int conRetryFromSeconds = int.Parse(WebConfigurationManager.AppSettings["CloudBreadconRetryFromSeconds"]);     /// adding v2.0.0
        public static string CloudBreadSocketKeyText = WebConfigurationManager.AppSettings["CloudBreadSocketKeyText"];     /// adding v2.0.0
        public static string CloudBreadSocketKeyIV = WebConfigurationManager.AppSettings["CloudBreadSocketKeyIV"];     /// adding v2.0.0

    }
}