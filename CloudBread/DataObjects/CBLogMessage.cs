using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBread.DataObjects
{
    public class CBLogMessage
    {
        public string DBConnectionString { get; set; }
        public string StorageConnectionString { get; set; }
        public string CloudBreadLoggerSetting { get; set; }
        public string category { get; set; }
        public string level { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string exception { get; set; }


    }
}