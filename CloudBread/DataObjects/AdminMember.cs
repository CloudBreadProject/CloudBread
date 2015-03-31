using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Mobile.Service;    //추가 

namespace CloudBread.DataObjects
{
    public class AdminMember : EntityData 
    {

        public string AdminMemberID { get; set; }

        public string AdminMemberPWD { get; set; }

        public string AdminMemberEmail { get; set; }

        public string IDCreateAdminMember { get; set; }

        public string AdminGroup { get; set; }

        public string PINumber { get; set; }

        public string Name1 { get; set; }

        public string Name2 { get; set; }

        public string Name3 { get; set; }

        public string DOB { get; set; }

        public string LastIPaddress { get; set; }


        public string LastLoginDT { get; set; }

        public string LastLogoutDT { get; set; }



        public string HideYN { get; set; }

        public string AccountBlockYN { get; set; }

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