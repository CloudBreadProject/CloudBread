/**
* @file CBComUdtItemList1Controller.cs
* @brief Common API for a item data update on ItemLists table. \n
* Set parameter null or remove json property for no change on column data. \m
* This API is deplicated. Not a user mode API to update item table. - CloudBread 2.0.0-beta
* @author Dae Woo Kim
* @param memberID - log purpose
* @param ItemLists table object
* @return string "1" - affected rows
* @see uspComUdtItemList1 SP, BehaviorID : B56
*/

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using Microsoft.Azure.Mobile.Server;
//using Microsoft.Azure.Mobile.Server.Config;

//using System.Threading.Tasks;
//using System.Diagnostics;
//using Logger.Logging;
//using CloudBread.globals;
//using CloudBreadLib.BAL.Crypto;
//using System.Data;
//using System.Data.Sql;
//using System.Data.SqlClient;
//using Newtonsoft.Json;
//using CloudBreadAuth;
//using System.Security.Claims;
//using Microsoft.Practices.TransientFaultHandling;
//using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;

//namespace CloudBread.Controllers
//{
//    [MobileAppController]
//    public class CBComUdtItemList1Controller : ApiController
//    {
        
//        public class InputParams
//        {
//            public string MemberID { get; set; }     // log purpose
//            public string ItemListID { get; set; }
//            public string ItemName { get; set; }
//            public string ItemDescription { get; set; }
//            public string ItemPrice { get; set; }
//            public string ItemSellPrice { get; set; }
//            public string ItemCategory1 { get; set; }
//            public string ItemCategory2 { get; set; }
//            public string ItemCategory3 { get; set; }
//            public string sCol1 { get; set; }
//            public string sCol2 { get; set; }
//            public string sCol3 { get; set; }
//            public string sCol4 { get; set; }
//            public string sCol5 { get; set; }
//            public string sCol6 { get; set; }
//            public string sCol7 { get; set; }
//            public string sCol8 { get; set; }
//            public string sCol9 { get; set; }
//            public string sCol10 { get; set; }

//        }

//        public string Post(InputParams p)
//        {
//            string result = "";

//            // Get the sid or memberID of the current user.
//            var claimsPrincipal = this.User as ClaimsPrincipal;
//            string sid = CBAuth.getMemberID(p.MemberID, claimsPrincipal);
//            p.MemberID = sid;

//            Logging.CBLoggers logMessage = new Logging.CBLoggers();
//            string jsonParam = JsonConvert.SerializeObject(p);

//            try
//            {
//                // task start log
//                //logMessage.memberID = p.MemberID;
//                //logMessage.Level = "INFO";
//                //logMessage.Logger = "CBComUdtItemList1Controller";
//                //logMessage.Message = jsonParam;
//                //Logging.RunLog(logMessage);

//                /// Database connection retry policy
//                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
//                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
//                {
//                    using (SqlCommand command = new SqlCommand("uspComUdtItemList1", connection))
//                    {

//                        command.CommandType = CommandType.StoredProcedure;
//                        command.Parameters.Add("@ItemListID ", SqlDbType.NVarChar, -1).Value = p.ItemListID;
//                        command.Parameters.Add("@ItemName ", SqlDbType.NVarChar, -1).Value = p.ItemName;
//                        command.Parameters.Add("@ItemDescription ", SqlDbType.NVarChar, -1).Value = p.ItemDescription;
//                        command.Parameters.Add("@ItemPrice ", SqlDbType.NVarChar, -1).Value = p.ItemPrice;
//                        command.Parameters.Add("@ItemSellPrice ", SqlDbType.NVarChar, -1).Value = p.ItemSellPrice;
//                        command.Parameters.Add("@ItemCategory1 ", SqlDbType.NVarChar, -1).Value = p.ItemCategory1;
//                        command.Parameters.Add("@ItemCategory2 ", SqlDbType.NVarChar, -1).Value = p.ItemCategory2;
//                        command.Parameters.Add("@ItemCategory3 ", SqlDbType.NVarChar, -1).Value = p.ItemCategory3;
//                        command.Parameters.Add("@sCol1 ", SqlDbType.NVarChar, -1).Value = p.sCol1;
//                        command.Parameters.Add("@sCol2 ", SqlDbType.NVarChar, -1).Value = p.sCol2;
//                        command.Parameters.Add("@sCol3 ", SqlDbType.NVarChar, -1).Value = p.sCol3;
//                        command.Parameters.Add("@sCol4 ", SqlDbType.NVarChar, -1).Value = p.sCol4;
//                        command.Parameters.Add("@sCol5 ", SqlDbType.NVarChar, -1).Value = p.sCol5;
//                        command.Parameters.Add("@sCol6 ", SqlDbType.NVarChar, -1).Value = p.sCol6;
//                        command.Parameters.Add("@sCol7 ", SqlDbType.NVarChar, -1).Value = p.sCol7;
//                        command.Parameters.Add("@sCol8 ", SqlDbType.NVarChar, -1).Value = p.sCol8;
//                        command.Parameters.Add("@sCol9 ", SqlDbType.NVarChar, -1).Value = p.sCol9;
//                        command.Parameters.Add("@sCol10 ", SqlDbType.NVarChar, -1).Value = p.sCol10;

//                        connection.OpenWithRetry(retryPolicy);
//                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
//                        {
//                            while (dreader.Read())
//                            {
//                                result = dreader[0].ToString();
//                            }
//                            dreader.Close();
//                        }
//                        connection.Close();

//                        // task end log
//                        logMessage.memberID = p.MemberID;
//                        logMessage.Level = "INFO";
//                        logMessage.Logger = "CBComUdtItemList1Controller";
//                        logMessage.Message = jsonParam;
//                        Logging.RunLog(logMessage);

//                        return result;
//                    }

//                }
//            }

//            catch (Exception ex)
//            {
//                // error log
//                logMessage.memberID = p.MemberID;
//                logMessage.Level = "ERROR";
//                logMessage.Logger = "CBComUdtItemList1Controller";
//                logMessage.Message = jsonParam;
//                logMessage.Exception = ex.ToString();
//                Logging.RunLog(logMessage);

//                throw;
//            }
//        }

//    }
//}
