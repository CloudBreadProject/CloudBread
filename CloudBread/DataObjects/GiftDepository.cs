using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.WindowsAzure.Mobile.Service;    //추가 

namespace CloudBread.DataObjects
{
    public class GiftDepository : EntityData 
    {
        public string GiftDepositoryID { get; set; }
        
        public string ItemName { get; set; }

        public string ItemListID { get; set; }

        public string ItemCount { get; set; }

        public string FromMemberID { get; set; }

        public string ToMemberID { get; set; }

        public string SentMemberYN { get; set; }

        public string HideYN { get; set; }

        public string DeleteYN { get; set; }



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