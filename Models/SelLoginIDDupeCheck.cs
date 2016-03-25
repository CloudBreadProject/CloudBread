using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBread.Models
{
    public class SelLoginIDDupeCheckInputParams
    {
        public string memberID { get; set; }
        public string findID { get; set; }
        public string category { get; set; }
        public string token { get; set; }
    }

    //return json
    public class SelLoginIDDupeCheckResult
    {
        public string result { get; set; }
    }
}