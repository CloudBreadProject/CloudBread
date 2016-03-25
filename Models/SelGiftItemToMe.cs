using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBread.Models
{
    public class SelGiftItemToMeInputParams
    {
        public string MemberID { get; set; }
        public string token { get; set; }
    }

    public class SelGiftItemToMeModel
    {
        public string GiftDepositoryID { get; set; }
        public string ItemListID { get; set; }
        public string ItemCount { get; set; }
        public string FromMemberID { get; set; }
        public string ToMemberID { get; set; }
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