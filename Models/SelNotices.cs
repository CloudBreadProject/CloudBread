using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBread.Models
{
    public class SelNoticesInputParams
    {
        public string MemberID { get; set; }     // log tasking purpose 
        public string token { get; set; }
    }

    public class SelNoticesModel
    {
        public string NoticeID { get; set; }
        public string NoticeCategory1 { get; set; }
        public string NoticeCategory2 { get; set; }
        public string NoticeCategory3 { get; set; }
        public string TargetGroup { get; set; }
        public string TargetOS { get; set; }
        public string TargetDevice { get; set; }
        public string NoticeImageLink { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string sCol1 { get; set; }
        public string sCol2 { get; set; }
        public string sCol3 { get; set; }
        public string sCol4 { get; set; }
        public string sCol5 { get; set; }
        public string sCol6 { get; set; }
        public string sCol7 { get; set; }
        public string sCol8 { get; set; }
        public string sCol9 { get; set; }
        public string sCol10 { get; set; }
    }
}