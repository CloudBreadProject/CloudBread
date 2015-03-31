using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//추가
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

//[DisplayFormat(ConvertEmptyStringToNull = false)]     //HTML 빈문자열을 null로 치환하지 않음.
//[Required]      //필수
//[EmailAddress]        // 이메일
//[RegularExpression(@"^[0-9]+$")]     //숫자만
//[RegularExpression(@"\b(Y|N)\b")]   //Y 또는 N
//[DataType(DataType.MultilineText)]      // 여러줄 사용
//[DataType(DataType.DateTime)]       //Datetime 형

namespace CloudBreadAdminWeb
{
    [MetadataType(typeof(AdminMembers.MetaData))]
    public partial class AdminMembers
    {
        public AdminMembers()
        {
            this.Coupon = new HashSet<Coupon>();
            this.GameEvents = new HashSet<GameEvents>();
            this.ItemLists = new HashSet<ItemLists>();      
            this.ItemLists1 = new HashSet<ItemLists>();    
            this.MemberItemPurchases = new HashSet<MemberItemPurchases>();
            this.Notices = new HashSet<Notices>();
            this.MemberAccountBlockLog = new HashSet<MemberAccountBlockLog>();
        }

        public virtual ICollection<Coupon> Coupon { get; set; }
        public virtual ICollection<GameEvents> GameEvents { get; set; }
        public virtual ICollection<ItemLists> ItemLists { get; set; }
        public virtual ICollection<ItemLists> ItemLists1 { get; set; }
        public virtual ICollection<MemberItemPurchases> MemberItemPurchases { get; set; }
        public virtual ICollection<Notices> Notices { get; set; }
        public virtual ICollection<MemberAccountBlockLog> MemberAccountBlockLog { get; set; }

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string AdminMemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            [DataType(DataType.Password)]
            public string AdminMemberPWD { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string AdminMemberEmail { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string IDCreateAdminMember { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Admin|Operator|Reader)\b")]
            [Required]
            public string AdminGroup { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string TimeZoneID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PINumber { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Name1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Name2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Name3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DOB { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string LastIPaddress { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string LastLoginDT { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string LastLogoutDT { get; set; }

            [DataType(DataType.MultilineText)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string sCol1 { get; set; }

            [DataType(DataType.MultilineText)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string sCol2 { get; set; }

            [DataType(DataType.MultilineText)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string sCol3 { get; set; }

            [DataType(DataType.MultilineText)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string sCol4 { get; set; }

            [DataType(DataType.MultilineText)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string sCol5 { get; set; }

            [DataType(DataType.MultilineText)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string sCol6 { get; set; }

            [DataType(DataType.MultilineText)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string sCol7 { get; set; }

            [DataType(DataType.MultilineText)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string sCol8 { get; set; }

            [DataType(DataType.MultilineText)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string sCol9 { get; set; }

            [DataType(DataType.MultilineText)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string sCol10 { get; set; }

            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DataType(DataType.DateTime)]
            public System.DateTime CreatedAt { get; set; }

            [DataType(DataType.DateTime)]
            public System.DateTime UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public System.DateTime DataFromRegionDT { get; set; }
        }
    }

    [MetadataType(typeof(Coupon.MetaData))]
    public partial class Coupon
    {
        public Coupon()
        {
            this.CouponMember = new HashSet<CouponMember>();
        }

        public virtual AdminMembers AdminMembers { get; set; }
        public virtual ItemLists ItemLists { get; set; }
        public virtual ICollection<CouponMember> CouponMember { get; set; }

        private sealed class MetaData
        {
            [Required]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string CouponID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string CouponCategory1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string CouponCategory2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string CouponCategory3 { get; set; }

            [Required]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemListID { get; set; }

            [Required]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemCount { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemStatus { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string TargetGroup { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string TargetOS { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string TargetDevice { get; set; }

            [Required]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Title { get; set; }

            [Required]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string Content { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DupeYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public int OrderNumber { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CouponDurationFrom { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CouponDurationTo { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string CreateAdminID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTime CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTime UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTime DataFromRegionDT { get; set; }
        }

    }

    [MetadataType(typeof(CouponMember.MetaData))]
    public partial class CouponMember
    {
        public virtual Coupon Coupon { get; set; }    // Coupon 테이블 참조
        public virtual Members Members { get; set; }    // Members 테이블 참조

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string CouponMemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string CouponID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }


    [MetadataType(typeof(GameEventMember.MetaData))]
    public partial class GameEventMember
    {
        public virtual GameEvents GameEvents { get; set; }    // GameEvents 테이블 참조
        public virtual Members Members { get; set; }    // Members 테이블 참조

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string GameEventMemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string eventID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }


    [MetadataType(typeof(GameEvents.MetaData))]
    public partial class GameEvents
    {
        public GameEvents()
        {
            this.GameEventMember = new HashSet<GameEventMember>();
        }

        public virtual AdminMembers AdminMembers { get; set; }
        public virtual ICollection<GameEventMember> GameEventMember { get; set; }
        public virtual ItemLists ItemLists { get; set; }

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string GameEventID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string EventCategory1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string EventCategory2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string EventCategory3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string ItemListID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string ItemCount { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Itemstatus { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string TargetGroup { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string TargetOS { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string TargetDevice { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string EventImageLink { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string Title { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            [Required]
            public string Content { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset EventDurationFrom { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset EventDurationTo { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"^[0-9]+$")]
            [Required]
            public int OrderNumber { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string CreateAdminID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }


    [MetadataType(typeof(GiftDepositories.MetaData))]
    public partial class GiftDepositories
    {
        public virtual ItemLists ItemLists { get; set; }
        public virtual Members Members { get; set; }        //FromMember
        public virtual Members Members1 { get; set; }       //ToMember

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string GiftDepositoryID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string ItemListID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemCount { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string FromMemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string ToMemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string SentMemberYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }

    [MetadataType(typeof(ItemLists.MetaData))]
    public partial class ItemLists{
        public ItemLists()
        {
            this.Coupon = new HashSet<Coupon>();
            this.GameEvents = new HashSet<GameEvents>();
            this.GiftDepositories = new HashSet<GiftDepositories>();
            this.MemberItemPurchases = new HashSet<MemberItemPurchases>();
            this.MemberItems = new HashSet<MemberItems>();
        }

        public virtual AdminMembers AdminMembers { get; set; }      //CreateAdminID
        public virtual AdminMembers AdminMembers1 { get; set; }     //UpdateAdminID
        public virtual ICollection<Coupon> Coupon { get; set; }
        public virtual ICollection<GameEvents> GameEvents { get; set; }
        public virtual ICollection<GiftDepositories> GiftDepositories { get; set; }
        public virtual ICollection<MemberItemPurchases> MemberItemPurchases { get; set; }
        public virtual ICollection<MemberItems> MemberItems { get; set; }

        private sealed class MetaData {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string ItemListID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string ItemName { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemDescription { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemPrice { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemSellPrice { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemCategory1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemCategory2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemCategory3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string IteamCreateAdminID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string IteamUpdateAdminID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }

    }


    [MetadataType(typeof(MemberAccountBlockLog.MetaData))]
    public partial class MemberAccountBlockLog
    {
        public virtual Members Members { get; set; }    // Members 테이블 참조
        public virtual AdminMembers AdminMembers { get; set; }    // AdminMembers 테이블 참조

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberAccountBlockID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string MemberAccountBlockReasonCategory1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string MemberAccountBlockReasonCategory2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string MemberAccountBlockReasonCategory3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberAccountBlockReason { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberAccountBlockProcess { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string CreateAdminID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }


    [MetadataType(typeof(MemberGameInfoes.MetaData))]
    public partial class MemberGameInfoes
    {
        public virtual Members Members { get; set; }    // Members 테이블 참조

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Level { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Exps { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Points { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string UserSTAT1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string UserSTAT2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string UserSTAT3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string UserSTAT4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string UserSTAT5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string UserSTAT6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string UserSTAT7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string UserSTAT8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string UserSTAT9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string UserSTAT10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }

    [MetadataType(typeof(MemberGameInfoStages.MetaData))]
    public partial class MemberGameInfoStages
    {
        public virtual Members Members { get; set; }

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberGameInfoStageID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string StageName { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string StageStatus { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Category1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Category2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Category3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Mission1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Mission2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Mission3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Mission4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Mission5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Points { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string StageStat1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string StageStat2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string StageStat3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string StageStat4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string StageStat5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }


    [MetadataType(typeof(MemberItemPurchases.MetaData))]
    public partial class MemberItemPurchases
    {
        public virtual AdminMembers AdminMembers { get; set; }
        public virtual ItemLists ItemLists { get; set; }
        public virtual Members Members { get; set; }

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberItemPurchaseID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string ItemListID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseQuantity { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string PurchasePrice { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PGinfo1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PGinfo2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PGinfo3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PGinfo4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PGinfo5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseDeviceID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseDeviceIPAddress { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseDeviceMACAddress { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string PurchaseDT { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseCancelYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseCancelDT { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseCancelingStatus { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseCancelReturnedAmount { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseCancelDeviceID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseCancelDeviceIPAddress { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseCancelDeviceMACAddress { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PurchaseCancelConfirmAdminMemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }


    [MetadataType(typeof(MemberItems.MetaData))]
    public partial class MemberItems
    {
        public virtual ItemLists ItemLists { get; set; }
        public virtual Members Members { get; set; }

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberItemID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string ItemListID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemCount { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string ItemStatus { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }

    [MetadataType(typeof(Members.MetaData))]
    public partial class Members
    {
        public Members()
        {
            this.CouponMember = new HashSet<CouponMember>();
            this.GameEventMember = new HashSet<GameEventMember>();
            this.GiftDepositories = new HashSet<GiftDepositories>();
            this.GiftDepositories1 = new HashSet<GiftDepositories>();
            this.MemberAccountBlockLog = new HashSet<MemberAccountBlockLog>();
            this.MemberGameInfoStages = new HashSet<MemberGameInfoStages>();
            this.MemberItemPurchases = new HashSet<MemberItemPurchases>();
            this.MemberItems = new HashSet<MemberItems>();
        }

        public virtual ICollection<CouponMember> CouponMember { get; set; }
        public virtual ICollection<GameEventMember> GameEventMember { get; set; }
        public virtual ICollection<GiftDepositories> GiftDepositories { get; set; }
        public virtual ICollection<GiftDepositories> GiftDepositories1 { get; set; }
        public virtual ICollection<MemberAccountBlockLog> MemberAccountBlockLog { get; set; }
        public virtual MemberGameInfoes MemberGameInfoes { get; set; }
        public virtual ICollection<MemberGameInfoStages> MemberGameInfoStages { get; set; }
        public virtual ICollection<MemberItemPurchases> MemberItemPurchases { get; set; }
        public virtual ICollection<MemberItems> MemberItems { get; set; }

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string MemberID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.Password)]
            public string MemberPWD { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string EmailAddress { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string EmailConfirmedYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PhoneNumber1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PhoneNumber2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PINumber { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Name1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Name2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Name3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DOB { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string RecommenderID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string MemberGroup { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string LastDeviceID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string LastIPaddress { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string LastLoginDT { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string LastLogoutDT { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string LastMACAddress { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string AccountBlockYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string AccountBlockEndDT { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string AnonymousYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string C3rdAuthProvider { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string C3rdAuthID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string C3rdAuthParam { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PushNotificationID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PushNotificationProvider { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string PushNotificationGroup { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string TimeZoneID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }


    [MetadataType(typeof(Notices.MetaData))]
    public partial class Notices
    {
        public virtual AdminMembers AdminMembers { get; set; }    // AdminMembers 테이블 참조

        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string NoticeID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string NoticeCategory1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string NoticeCategory2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string NoticeCategory3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string TargetGroup { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string TargetOS { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string TargetDevice { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string NoticeImageLink { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string title { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            [Required]
            public string content { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol6 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol7 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol8 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol9 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol10 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset NoticeDurationFrom { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset NoticeDurationTo { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"^[0-9]+$")]
            [Required]
            public int OrderNumber { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string CreateAdminID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string HideYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [RegularExpression(@"\b(Y|N)\b")]
            [Required]
            public string DeleteYN { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }


    [MetadataType(typeof(ServerInfo.MetaData))]
    public partial class ServerInfo
    {
        private sealed class MetaData
        {
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string InfoID { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string FEServerLists { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string SocketServerLists { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required]
            public string Version { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string ResourceLink { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string EULAText { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol1 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol2 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol3 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol4 { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            public string sCol5 { get; set; }


            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset CreatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            [Required]
            public System.DateTimeOffset UpdatedAt { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string DataFromRegion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.DateTime)]
            public System.DateTimeOffset DataFromRegionDT { get; set; }
        }
    }
}