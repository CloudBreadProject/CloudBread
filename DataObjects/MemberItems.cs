using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.WindowsAzure.Mobile.Service;    //추가

namespace CloudBread.DataObjects
{
    public class MemberItem : EntityData 
    {
        // id 컬럼 자동생성

        public string MemberItemID { get; set; }
        

        public string MemberID { get; set; }

        public string ItemListID { get; set; }

        public string ItemCount { get; set; }
        

        public string ItemStatus { get; set; }
        

        // public string GiftToMember { get; set; }     // 테이블 방식으로 변경

        // public string GiftFromMember { get; set; }

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