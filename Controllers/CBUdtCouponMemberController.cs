/**
* @file CBUdtCouponMemberController.cs
* @brief Update MemberItems and CouponMember table by using coupon. \n
* "DupeYN" value is set "Y", multiple members can use this coupon.
* "DupeYN" value is set "N", only one member can use it. - set "DeleteYN" to "Y"
* @author Dae Woo Kim
* @param string InsertORUpdate  - if itemid exists in memberitem inventory, then "UPDATE". if not, "INSERT".
* @param MemberItems object
* @param CouponMember object
* @return string "2" or "3" - affected rows. 2 or 3 is depend on "DupeYN" value.
* @see uspUdtCouponMember SP, BehaviorID : B15
* @todo change SP to upsert auto method
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;

using System.Threading.Tasks;
using System.Diagnostics;
using Logger.Logging;
using CloudBread.globals;
using CloudBreadLib.BAL.Crypto;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Newtonsoft.Json;
using CloudBreadAuth;
using System.Security.Claims;
using Microsoft.Practices.TransientFaultHandling;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;

namespace CloudBread.Controllers
{
    [MobileAppController]
    public class CBUdtCouponMemberController : ApiController
    {
        
        public class InputParams
        {
            public string InsertORUpdate { get; set; }
            public string CouponID_Coupon { get; set; }
            public string MemberItemID_MemberItems { get; set; }
            public string MemberID_MemberItems { get; set; }
            public string ItemListID_MemberItems { get; set; }
            public string ItemCount_MemberItems { get; set; }
            public string ItemStatus_MemberItems { get; set; }
            public string sCol1_MemberItems { get; set; }
            public string sCol2_MemberItems { get; set; }
            public string sCol3_MemberItems { get; set; }
            public string sCol4_MemberItems { get; set; }
            public string sCol5_MemberItems { get; set; }
            public string sCol6_MemberItems { get; set; }
            public string sCol7_MemberItems { get; set; }
            public string sCol8_MemberItems { get; set; }
            public string sCol9_MemberItems { get; set; }
            public string sCol10_MemberItems { get; set; }
            public string CouponID_CouponMember { get; set; }
            public string MemberID_CouponMember { get; set; }
            public string sCol1_CouponMember { get; set; }
            public string sCol2_CouponMember { get; set; }
            public string sCol3_CouponMember { get; set; }
            public string sCol4_CouponMember { get; set; }
            public string sCol5_CouponMember { get; set; }
            public string sCol6_CouponMember { get; set; }
            public string sCol7_CouponMember { get; set; }
            public string sCol8_CouponMember { get; set; }
            public string sCol9_CouponMember { get; set; }
            public string sCol10_CouponMember { get; set; }

        }

        public string Post(InputParams p)
        {
            string result = "";

            // Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = CBAuth.getMemberID(p.MemberID_CouponMember, claimsPrincipal);
            p.MemberID_CouponMember = sid;
            p.MemberID_MemberItems = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                // start task log
                //logMessage.memberID = p.MemberID_MemberItems;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBUdtCouponMemberController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspUdtCouponMember", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@InsertORUpdate", SqlDbType.NVarChar, -1).Value = p.InsertORUpdate.ToUpper();
                        command.Parameters.Add("@CouponID_Coupon", SqlDbType.NVarChar, -1).Value = p.CouponID_Coupon;
                        command.Parameters.Add("@MemberItemID_MemberItems", SqlDbType.NVarChar, -1).Value = p.MemberItemID_MemberItems;
                        command.Parameters.Add("@MemberID_MemberItems", SqlDbType.NVarChar, -1).Value = p.MemberID_MemberItems;
                        command.Parameters.Add("@ItemListID_MemberItems", SqlDbType.NVarChar, -1).Value = p.ItemListID_MemberItems;
                        command.Parameters.Add("@ItemCount_MemberItems", SqlDbType.NVarChar, -1).Value = p.ItemCount_MemberItems;
                        command.Parameters.Add("@ItemStatus_MemberItems", SqlDbType.NVarChar, -1).Value = p.ItemStatus_MemberItems;
                        command.Parameters.Add("@sCol1_MemberItems", SqlDbType.NVarChar, -1).Value = p.sCol1_MemberItems;
                        command.Parameters.Add("@sCol2_MemberItems", SqlDbType.NVarChar, -1).Value = p.sCol2_MemberItems;
                        command.Parameters.Add("@sCol3_MemberItems", SqlDbType.NVarChar, -1).Value = p.sCol3_MemberItems;
                        command.Parameters.Add("@sCol4_MemberItems", SqlDbType.NVarChar, -1).Value = p.sCol4_MemberItems;
                        command.Parameters.Add("@sCol5_MemberItems", SqlDbType.NVarChar, -1).Value = p.sCol5_MemberItems;
                        command.Parameters.Add("@sCol6_MemberItems", SqlDbType.NVarChar, -1).Value = p.sCol6_MemberItems;
                        command.Parameters.Add("@sCol7_MemberItems", SqlDbType.NVarChar, -1).Value = p.sCol7_MemberItems;
                        command.Parameters.Add("@sCol8_MemberItems", SqlDbType.NVarChar, -1).Value = p.sCol8_MemberItems;
                        command.Parameters.Add("@sCol9_MemberItems", SqlDbType.NVarChar, -1).Value = p.sCol9_MemberItems;
                        command.Parameters.Add("@sCol10_MemberItems", SqlDbType.NVarChar, -1).Value = p.sCol10_MemberItems;
                        command.Parameters.Add("@CouponID_CouponMember", SqlDbType.NVarChar, -1).Value = p.CouponID_CouponMember;
                        command.Parameters.Add("@MemberID_CouponMember", SqlDbType.NVarChar, -1).Value = p.MemberID_CouponMember;
                        command.Parameters.Add("@sCol1_CouponMember", SqlDbType.NVarChar, -1).Value = p.sCol1_CouponMember;
                        command.Parameters.Add("@sCol2_CouponMember", SqlDbType.NVarChar, -1).Value = p.sCol2_CouponMember;
                        command.Parameters.Add("@sCol3_CouponMember", SqlDbType.NVarChar, -1).Value = p.sCol3_CouponMember;
                        command.Parameters.Add("@sCol4_CouponMember", SqlDbType.NVarChar, -1).Value = p.sCol4_CouponMember;
                        command.Parameters.Add("@sCol5_CouponMember", SqlDbType.NVarChar, -1).Value = p.sCol5_CouponMember;
                        command.Parameters.Add("@sCol6_CouponMember", SqlDbType.NVarChar, -1).Value = p.sCol6_CouponMember;
                        command.Parameters.Add("@sCol7_CouponMember", SqlDbType.NVarChar, -1).Value = p.sCol7_CouponMember;
                        command.Parameters.Add("@sCol8_CouponMember", SqlDbType.NVarChar, -1).Value = p.sCol8_CouponMember;
                        command.Parameters.Add("@sCol9_CouponMember", SqlDbType.NVarChar, -1).Value = p.sCol9_CouponMember;
                        command.Parameters.Add("@sCol10_CouponMember", SqlDbType.NVarChar, -1).Value = p.sCol10_CouponMember;

                        connection.OpenWithRetry(retryPolicy);
                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                result = dreader[0].ToString();
                            }
                            dreader.Close();
                        }
                        connection.Close();

                        // end task log
                        logMessage.memberID = p.MemberID_MemberItems;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBUdtCouponMemberController";
                        logMessage.Message = jsonParam;
                        Logging.RunLog(logMessage);

                        return result;
                    }

                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.MemberID_MemberItems;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBUdtCouponMemberController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
