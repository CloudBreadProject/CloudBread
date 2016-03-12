/**
* @file CBComInsMemberItemPurchaseController.cs
* @brief Common API for Insert MemberItemPurchase data \n
* @author Dae Woo Kim
* @param MemberItemPurchases table object
* @return string "1" - affected rows.
* @see uspComMemberItemPurchase SP, BehaviorID : B28, B60
* @todo make notice to game manangers to check admin website
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
    public class CBComInsMemberItemPurchaseController : ApiController
    {
        public class InputParams
        {
            public string MemberItemPurchaseID { get; set; }
            public string MemberID { get; set; }
            public string ItemListID { get; set; }
            public string PurchaseQuantity { get; set; }
            public string PurchasePrice { get; set; }
            public string PGinfo1 { get; set; }
            public string PGinfo2 { get; set; }
            public string PGinfo3 { get; set; }
            public string PGinfo4 { get; set; }
            public string PGinfo5 { get; set; }
            public string PurchaseDeviceID { get; set; }
            public string PurchaseDeviceIPAddress { get; set; }
            public string PurchaseDeviceMACAddress { get; set; }
            public string PurchaseDT { get; set; }
            public string PurchaseCancelYN { get; set; }
            public string PurchaseCancelDT { get; set; }
            public string PurchaseCancelingStatus { get; set; }
            public string PurchaseCancelReturnedAmount { get; set; }
            public string PurchaseCancelDeviceID { get; set; }
            public string PurchaseCancelDeviceIPAddress { get; set; }
            public string PurchaseCancelDeviceMACAddress { get; set; }
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

        public string Post(InputParams p)
        {
            string result = "";

            // Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = CBAuth.getMemberID(p.MemberID, claimsPrincipal);
            p.MemberID = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                // task start log
                //logMessage.memberID = p.MemberID;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBComInsMemberItemPurchaseController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspComInsMemberItemPurchase", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberItemPurchaseID", SqlDbType.NVarChar, -1).Value = p.MemberItemPurchaseID;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.MemberID;
                        command.Parameters.Add("@ItemListID", SqlDbType.NVarChar, -1).Value = p.ItemListID;
                        command.Parameters.Add("@PurchaseQuantity", SqlDbType.NVarChar, -1).Value = p.PurchaseQuantity;
                        command.Parameters.Add("@PurchasePrice", SqlDbType.NVarChar, -1).Value = p.PurchasePrice;
                        command.Parameters.Add("@PGinfo1", SqlDbType.NVarChar, -1).Value = p.PGinfo1;
                        command.Parameters.Add("@PGinfo2", SqlDbType.NVarChar, -1).Value = p.PGinfo2;
                        command.Parameters.Add("@PGinfo3", SqlDbType.NVarChar, -1).Value = p.PGinfo3;
                        command.Parameters.Add("@PGinfo4", SqlDbType.NVarChar, -1).Value = p.PGinfo4;
                        command.Parameters.Add("@PGinfo5", SqlDbType.NVarChar, -1).Value = p.PGinfo5;
                        command.Parameters.Add("@PurchaseDeviceID", SqlDbType.NVarChar, -1).Value = p.PurchaseDeviceID;
                        command.Parameters.Add("@PurchaseDeviceIPAddress", SqlDbType.NVarChar, -1).Value = p.PurchaseDeviceIPAddress;
                        command.Parameters.Add("@PurchaseDeviceMACAddress", SqlDbType.NVarChar, -1).Value = p.PurchaseDeviceMACAddress;
                        command.Parameters.Add("@PurchaseDT", SqlDbType.NVarChar, -1).Value = p.PurchaseDT;
                        command.Parameters.Add("@PurchaseCancelYN", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelYN;
                        command.Parameters.Add("@PurchaseCancelDT", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelDT;
                        command.Parameters.Add("@PurchaseCancelingStatus", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelingStatus;
                        command.Parameters.Add("@PurchaseCancelReturnedAmount", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelReturnedAmount;
                        command.Parameters.Add("@PurchaseCancelDeviceID", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelDeviceID;
                        command.Parameters.Add("@PurchaseCancelDeviceIPAddress", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelDeviceIPAddress;
                        command.Parameters.Add("@PurchaseCancelDeviceMACAddress", SqlDbType.NVarChar, -1).Value = p.PurchaseCancelDeviceMACAddress;
                        command.Parameters.Add("@sCol1", SqlDbType.NVarChar, -1).Value = p.sCol1;
                        command.Parameters.Add("@sCol2", SqlDbType.NVarChar, -1).Value = p.sCol2;
                        command.Parameters.Add("@sCol3", SqlDbType.NVarChar, -1).Value = p.sCol3;
                        command.Parameters.Add("@sCol4", SqlDbType.NVarChar, -1).Value = p.sCol4;
                        command.Parameters.Add("@sCol5", SqlDbType.NVarChar, -1).Value = p.sCol5;
                        command.Parameters.Add("@sCol6", SqlDbType.NVarChar, -1).Value = p.sCol6;
                        command.Parameters.Add("@sCol7", SqlDbType.NVarChar, -1).Value = p.sCol7;
                        command.Parameters.Add("@sCol8", SqlDbType.NVarChar, -1).Value = p.sCol8;
                        command.Parameters.Add("@sCol9", SqlDbType.NVarChar, -1).Value = p.sCol9;
                        command.Parameters.Add("@sCol10", SqlDbType.NVarChar, -1).Value = p.sCol10;

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
                        logMessage.memberID = p.MemberID;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBComInsMemberItemPurchaseController";
                        logMessage.Message = jsonParam;
                        Logging.RunLog(logMessage);

                        return result;
                    }

                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.MemberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBComInsMemberItemPurchaseController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
