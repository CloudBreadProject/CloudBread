using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBread.Models
{
    public class SelItemListAllInputParams
    {
        public string MemberID { get; set; }     // log purpose
        public Int64 Page { get; set; }
        public Int64 PageSize { get; set; }
        public string token { get; set; }
    }

    public class SelItemListAllModel
    {
        public string ROWNUM { get; set; }
        public string ItemListID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemPrice { get; set; }
        public string ItemSellPrice { get; set; }
        public string ItemCategory1 { get; set; }
        public string ItemCategory2 { get; set; }
        public string ItemCategory3 { get; set; }
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