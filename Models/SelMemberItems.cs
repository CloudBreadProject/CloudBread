using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudBread.Models
{
    public class SelMemberItemsInputParams
    {
        public string MemberID { get; set; }
        public Int64 Page { get; set; }
        public Int64 PageSize { get; set; }
        public string token { get; set; }
    }

    public class SelMemberItemsModel
    {
        public string ROWNUM { get; set; }
        public string ItemListsItemName { get; set; }
        public string ItemListsItemDescription { get; set; }
        public string ItemListsItemPrice { get; set; }
        public string ItemListsItemSellPrice { get; set; }
        public string ItemListsItemCategory1 { get; set; }
        public string ItemListsItemCategory2 { get; set; }
        public string ItemListsItemCategory3 { get; set; }
        public string ItemListssCol1 { get; set; }
        public string ItemListssCol2 { get; set; }
        public string ItemListssCol3 { get; set; }
        public string ItemListssCol4 { get; set; }
        public string ItemListssCol5 { get; set; }
        public string ItemListssCol6 { get; set; }
        public string ItemListssCol7 { get; set; }
        public string ItemListssCol8 { get; set; }
        public string ItemListssCol9 { get; set; }
        public string ItemListssCol10 { get; set; }
        public string MemberItemsMemberItemID { get; set; }
        public string MemberItemsMemberID { get; set; }
        public string MemberItemsItemListID { get; set; }
        public string MemberItemsItemCount { get; set; }
        public string MemberItemsItemStatus { get; set; }
        public string MemberItemssCol1 { get; set; }
        public string MemberItemssCol2 { get; set; }
        public string MemberItemssCol3 { get; set; }
        public string MemberItemssCol4 { get; set; }
        public string MemberItemssCol5 { get; set; }
        public string MemberItemssCol6 { get; set; }
        public string MemberItemssCol7 { get; set; }
        public string MemberItemssCol8 { get; set; }
        public string MemberItemssCol9 { get; set; }
        public string MemberItemssCol10 { get; set; }
    }
}