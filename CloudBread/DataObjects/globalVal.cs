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

    }
}