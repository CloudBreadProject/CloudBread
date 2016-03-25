/**
* @file CBUdtGameEventMemberToItemController.cs
* @brief Save event item to member's MemberItems table  \n
* @author Dae Woo Kim
* @param string InsertORUpdate  - if itemid exists in memberitem inventory, then "UPDATE". if not, "INSERT".
* @param MemberItems object
* @param GameEventMember object
* @return string "2" - affected rows. in case of string "0" - event not exists
* @see uspUdtGameEventMemberToItem SP, BehaviorID : B13
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
using CloudBread.Models;

namespace CloudBread.Controllers
{
    [MobileAppController]
    public class CBUdtGameEventMemberToItemController : ApiController
    {
        public HttpResponseMessage Post(UdtGameEventMemberToItemInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<UdtGameEventMemberToItemInputParams>(decrypted);
                }
                catch (Exception ex)
                {
                    ex = (Exception)Activator.CreateInstance(ex.GetType(), "Decrypt Error", ex);
                    throw ex;
                }
            }

            // Get the sid or memberID of the current user.
            string sid = CBAuth.getMemberID(p.MemberID_MemberItems, this.User as ClaimsPrincipal);
            p.MemberID_MemberItems = sid;
            p.MemberID_GameEventMember = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();
            RowcountResult rowcountResult = new RowcountResult();

            try
            {
                // start task log
                //logMessage.memberID = p.MemberID_MemberItems;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBUdtGameEventMemberToItemController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspUdtGameEventMemberToItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@InsertORUpdate ", SqlDbType.NVarChar, -1).Value = p.InsertORUpdate.ToUpper();
                        command.Parameters.Add("@MemberItemID_MemberItems ", SqlDbType.NVarChar, -1).Value = p.MemberItemID_MemberItems;
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
                        command.Parameters.Add("@eventID_GameEventMember", SqlDbType.NVarChar, -1).Value = p.eventID_GameEventMember;
                        command.Parameters.Add("@MemberID_GameEventMember ", SqlDbType.NVarChar, -1).Value = p.MemberID_GameEventMember;
                        command.Parameters.Add("@sCol1_GameEventMember ", SqlDbType.NVarChar, -1).Value = p.sCol1_GameEventMember;
                        command.Parameters.Add("@sCol2_GameEventMember ", SqlDbType.NVarChar, -1).Value = p.sCol2_GameEventMember;
                        command.Parameters.Add("@sCol3_GameEventMember ", SqlDbType.NVarChar, -1).Value = p.sCol3_GameEventMember;
                        command.Parameters.Add("@sCol4_GameEventMember ", SqlDbType.NVarChar, -1).Value = p.sCol4_GameEventMember;
                        command.Parameters.Add("@sCol5_GameEventMember ", SqlDbType.NVarChar, -1).Value = p.sCol5_GameEventMember;
                        command.Parameters.Add("@sCol6_GameEventMember ", SqlDbType.NVarChar, -1).Value = p.sCol6_GameEventMember;
                        command.Parameters.Add("@sCol7_GameEventMember ", SqlDbType.NVarChar, -1).Value = p.sCol7_GameEventMember;
                        command.Parameters.Add("@sCol8_GameEventMember ", SqlDbType.NVarChar, -1).Value = p.sCol8_GameEventMember;
                        command.Parameters.Add("@sCol9_GameEventMember ", SqlDbType.NVarChar, -1).Value = p.sCol9_GameEventMember;
                        command.Parameters.Add("@sCol10_GameEventMember ", SqlDbType.NVarChar, -1).Value = p.sCol10_GameEventMember;

                        connection.OpenWithRetry(retryPolicy);
                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                rowcountResult.result = dreader[0].ToString();
                            }
                            dreader.Close();
                        }
                        connection.Close();

                        // end task log
                        logMessage.memberID = p.MemberID_MemberItems;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBUdtGameEventMemberToItemController";
                        logMessage.Message = jsonParam;
                        Logging.RunLog(logMessage);

                        /// Encrypt the result response
                        if (globalVal.CloudBreadCryptSetting == "AES256")
                        {
                            try
                            {
                                encryptedResult.token = Crypto.AES_encrypt(JsonConvert.SerializeObject(rowcountResult), globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                                response = Request.CreateResponse(HttpStatusCode.OK, encryptedResult);
                                return response;
                            }
                            catch (Exception ex)
                            {
                                ex = (Exception)Activator.CreateInstance(ex.GetType(), "Encrypt Error", ex);
                                throw ex;
                            }
                        }

                        response = Request.CreateResponse(HttpStatusCode.OK, rowcountResult);
                        return response;
                    }
                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.MemberID_MemberItems;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBUdtGameEventMemberToItemController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }
    }
}
