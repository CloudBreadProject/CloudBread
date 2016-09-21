using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBread.Models
{
    public class ComSelMemberGameInfoStagesInputParams
    {
        public string MemberID { get; set; }    // log purpose
        public string MemberGameInfoStageID { get; set; }
        public string token { get; set; }
    }

    public class ComSelMemberGameInfoStagesModel
    {
        public string MemberGameInfoStageID { get; set; }
        public string MemberID { get; set; }
        public string StageName { get; set; }
        public string StageStatus { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Mission1 { get; set; }
        public string Mission2 { get; set; }
        public string Mission3 { get; set; }
        public string Mission4 { get; set; }
        public string Mission5 { get; set; }
        public string Points { get; set; }
        public string StageStat1 { get; set; }
        public string StageStat2 { get; set; }
        public string StageStat3 { get; set; }
        public string StageStat4 { get; set; }
        public string StageStat5 { get; set; }
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