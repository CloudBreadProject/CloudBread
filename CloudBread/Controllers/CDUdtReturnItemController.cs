/**
* @file CDUdtReturnItemController.cs
* @brief Return purchased item and roll back item and status. Call this API in unique situation. \n
* Update or delete MemberItems, update MemberItemPurchases and update MemberGameInfoes. \n
* @author Dae Woo Kim
* @param string DeleteORUpdate  - if itemid exists in memberitem inventory and need to delete, set "DELETE". this operation will update delete flag of table  or set "UPDATE"
* @param MemberItems table object
* @param MemberItemPurchases table object
* @param MemberGameInfoes table object
* @return string "3" - affected rows.
* @see uspUdtReturnItem SP, BehaviorID : B30
* @todo change SP to updelete auto method
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
    public class CBUdtReturnItemController : ApiController
    {
        
        public class InputParams
        {
            public string DeleteORUpdate { get; set; }
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
            public string MemberItemPurchaseID_MemberItemPurchases { get; set; }
            public string MemberID_MemberItemPurchases { get; set; }
            public string ItemListID_MemberItemPurchases { get; set; }
            public string PurchaseQuantity_MemberItemPurchases { get; set; }
            public string PurchasePrice_MemberItemPurchases { get; set; }
            public string PGinfo1_MemberItemPurchases { get; set; }
            public string PGinfo2_MemberItemPurchases { get; set; }
            public string PGinfo3_MemberItemPurchases { get; set; }
            public string PGinfo4_MemberItemPurchases { get; set; }
            public string PGinfo5_MemberItemPurchases { get; set; }
            public string PurchaseDeviceID_MemberItemPurchases { get; set; }
            public string PurchaseDeviceIPAddress_MemberItemPurchases { get; set; }
            public string PurchaseDeviceMACAddress_MemberItemPurchases { get; set; }
            public string PurchaseDT_MemberItemPurchases { get; set; }
            public string PurchaseCancelYN_MemberItemPurchases { get; set; }
            public string PurchaseCancelDT_MemberItemPurchases { get; set; }
            public string PurchaseCancelingStatus_MemberItemPurchases { get; set; }
            public string PurchaseCancelReturnedAmount_MemberItemPurchases { get; set; }
            public string PurchaseCancelDeviceID_MemberItemPurchases { get; set; }
            public string PurchaseCancelDeviceIPAddress_MemberItemPurchases { get; set; }
            public string PurchaseCancelDeviceMACAddress_MemberItemPurchases { get; set; }
            public string sCol1_MemberItemPurchases { get; set; }
            public string sCol2_MemberItemPurchases { get; set; }
            public string sCol3_MemberItemPurchases { get; set; }
            public string sCol4_MemberItemPurchases { get; set; }
            public string sCol5_MemberItemPurchases { get; set; }
            public string sCol6_MemberItemPurchases { get; set; }
            public string sCol7_MemberItemPurchases { get; set; }
            public string sCol8_MemberItemPurchases { get; set; }
            public string sCol9_MemberItemPurchases { get; set; }
            public string sCol10_MemberItemPurchases { get; set; }
            public string MemberID_MemberGameInfoes { get; set; }
            public string Level_MemberGameInfoes { get; set; }
            public string Exps_MemberGameInfoes { get; set; }
            public string Points_MemberGameInfoes { get; set; }
            public string UserSTAT1_MemberGameInfoes { get; set; }
            public string UserSTAT2_MemberGameInfoes { get; set; }
            public string UserSTAT3_MemberGameInfoes { get; set; }
            public string UserSTAT4_MemberGameInfoes { get; set; }
            public string UserSTAT5_MemberGameInfoes { get; set; }
            public string UserSTAT6_MemberGameInfoes { get; set; }
            public string UserSTAT7_MemberGameInfoes { get; set; }
            public string UserSTAT8_MemberGameInfoes { get; set; }
            public string UserSTAT9_MemberGameInfoes { get; set; }
            public string UserSTAT10_MemberGameInfoes { get; set; }
            public string sCol1_MemberGameInfoes { get; set; }
            public string sCol2_MemberGameInfoes { get; set; }
            public string sCol3_MemberGameInfoes { get; set; }
            public string sCol4_MemberGameInfoes { get; set; }
            public string sCol5_MemberGameInfoes { get; set; }
            public string sCol6_MemberGameInfoes { get; set; }
            public string sCol7_MemberGameInfoes { get; set; }
            public string sCol8_MemberGameInfoes { get; set; }
            public string sCol9_MemberGameInfoes { get; set; }
            public string sCol10_MemberGameInfoes { get; set; }

        }

        public string Post(InputParams p)
        {
            string result = "";

            // Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = CBAuth.getMemberID(p.MemberID_MemberGameInfoes, claimsPrincipal);
            p.MemberID_MemberGameInfoes = sid;
            p.MemberID_MemberItemPurchases = sid;
            p.MemberID_MemberItems = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                // task start log
                //logMessage.memberID = p.MemberID_MemberItems;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBUdtReturnItemController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspUdtReturnItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@InsertORUpdate", SqlDbType.NVarChar, -1).Value = p.DeleteORUpdate.ToUpper();
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

                        command.Parameters.Add("@MemberItemPurchaseID_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.MemberItemPurchaseID_MemberItemPurchases;
                        command.Parameters.Add("@MemberID_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.MemberID_MemberItemPurchases;
                        command.Parameters.Add("@ItemListID_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.ItemListID_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseQuantity_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseQuantity_MemberItemPurchases;
                        command.Parameters.Add("@PurchasePrice_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchasePrice_MemberItemPurchases;
                        command.Parameters.Add("@PGinfo1_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PGinfo1_MemberItemPurchases;
                        command.Parameters.Add("@PGinfo2_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PGinfo2_MemberItemPurchases;
                        command.Parameters.Add("@PGinfo3_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PGinfo3_MemberItemPurchases;
                        command.Parameters.Add("@PGinfo4_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PGinfo4_MemberItemPurchases;
                        command.Parameters.Add("@PGinfo5_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PGinfo5_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseDeviceID_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseDeviceID_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseDeviceIPAddress_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseDeviceIPAddress_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseDeviceMACAddress_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseDeviceMACAddress_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseDT_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseDT_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseCancelYN_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelYN_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseCancelDT_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelDT_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseCancelingStatus_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelingStatus_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseCancelReturnedAmount_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelReturnedAmount_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseCancelDeviceID_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelDeviceID_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseCancelDeviceIPAddress_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelDeviceIPAddress_MemberItemPurchases;
                        command.Parameters.Add("@PurchaseCancelDeviceMACAddress_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelDeviceMACAddress_MemberItemPurchases;
                        command.Parameters.Add("@sCol1_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.sCol1_MemberItemPurchases;
                        command.Parameters.Add("@sCol2_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.sCol2_MemberItemPurchases;
                        command.Parameters.Add("@sCol3_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.sCol3_MemberItemPurchases;
                        command.Parameters.Add("@sCol4_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.sCol4_MemberItemPurchases;
                        command.Parameters.Add("@sCol5_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.sCol5_MemberItemPurchases;
                        command.Parameters.Add("@sCol6_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.sCol6_MemberItemPurchases;
                        command.Parameters.Add("@sCol7_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.sCol7_MemberItemPurchases;
                        command.Parameters.Add("@sCol8_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.sCol8_MemberItemPurchases;
                        command.Parameters.Add("@sCol9_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.sCol9_MemberItemPurchases;
                        command.Parameters.Add("@sCol10_MemberItemPurchases", SqlDbType.NVarChar, -1).Value = p.sCol10_MemberItemPurchases;
                        command.Parameters.Add("@MemberID_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.MemberID_MemberGameInfoes;
                        command.Parameters.Add("@Level_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.Level_MemberGameInfoes;
                        command.Parameters.Add("@Exps_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.Exps_MemberGameInfoes;
                        command.Parameters.Add("@Points_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.Points_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT1_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT1_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT2_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT2_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT3_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT3_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT4_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT4_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT5_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT5_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT6_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT6_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT7_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT7_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT8_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT8_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT9_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT9_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT10_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT10_MemberGameInfoes;
                        command.Parameters.Add("@sCol1_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol1_MemberGameInfoes;
                        command.Parameters.Add("@sCol2_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol2_MemberGameInfoes;
                        command.Parameters.Add("@sCol3_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol3_MemberGameInfoes;
                        command.Parameters.Add("@sCol4_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol4_MemberGameInfoes;
                        command.Parameters.Add("@sCol5_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol5_MemberGameInfoes;
                        command.Parameters.Add("@sCol6_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol6_MemberGameInfoes;
                        command.Parameters.Add("@sCol7_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol7_MemberGameInfoes;
                        command.Parameters.Add("@sCol8_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol8_MemberGameInfoes;
                        command.Parameters.Add("@sCol9_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol9_MemberGameInfoes;
                        command.Parameters.Add("@sCol10_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol10_MemberGameInfoes;

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

                        // task end log
                        logMessage.memberID = p.MemberID_MemberItems;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBUdtReturnItemController";
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
                logMessage.Logger = "CBUdtReturnItemController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
