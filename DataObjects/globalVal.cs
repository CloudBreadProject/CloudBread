using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Configuration;
using System.Configuration;

namespace CloudBread.globals
{
    public static class globalVal
    {
        public static string DBConnectionString = ConfigurationManager.ConnectionStrings["CloudBreadDBConString"].ConnectionString;
        public static string StorageConnectionString = ConfigurationManager.ConnectionStrings["CloudBreadStorageConString"].ConnectionString;
        public static string CloudBreadLoggerSetting = ConfigurationManager.AppSettings["CloudBreadLoggerSetting"].ToString();
        public static string CloudBreadCryptSetting = ConfigurationManager.AppSettings["CloudBreadCryptSetting"].ToString();
        public static int conRetryCount = int.Parse(ConfigurationManager.AppSettings["CloudBreadconRetryCount"]);    /// adding v2.0.0
        public static int conRetryFromSeconds = int.Parse(ConfigurationManager.AppSettings["CloudBreadconRetryFromSeconds"]);     /// adding v2.0.0
        public static string CloudBreadSocketKeyText = ConfigurationManager.AppSettings["CloudBreadSocketKeyText"];     /// adding v2.0.0
        public static string CloudBreadSocketKeyIV = ConfigurationManager.AppSettings["CloudBreadSocketKeyIV"];     /// adding v2.0.0

        public static string CloudBreadSocketRedisServer = ConfigurationManager.AppSettings["CloudBreadSocketRedisServer"];     /// adding v2.0.0
        public static string CloudBreadRankRedisServer = ConfigurationManager.AppSettings["CloudBreadRankRedisServer"];     /// adding v2.0.0
        public static string CloudBreadRankSortedSet = ConfigurationManager.AppSettings["CloudBreadRankSortedSet"];     /// adding v2.0.0 
        public static bool CloudBreadFillRedisRankSetOnStartup = bool.Parse(ConfigurationManager.AppSettings["CloudBreadFillRedisRankSetOnStartup"]); /// adding v2.0.0

        public static string CloudBreadGameLogRedisServer = ConfigurationManager.AppSettings["CloudBreadGameLogRedisServer"];     /// adding v2.0.0
        public static int CloudBreadGameLogExpTimeDays = int.Parse(ConfigurationManager.AppSettings["CloudBreadGameLogExpTimeDays"]);     /// adding v2.0.0
        


    }
}