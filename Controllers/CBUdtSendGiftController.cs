/**
* @file CBUdtSendGiftController.cs
* @brief Send item to another member. Update or delete(not actual delete - DeleteYN flag change) MemberItems, insert to GiftDepositories.  \n
* First of all, check member inventory and set first param, "DeleteORUpdate" branching memberitems
* @author Dae Woo Kim
* @param string DeleteORUpdate - branching memberitems table
* @param MemberItems table object
* @param GiftDepository table object 
* @return string "2" - affected rows
* @see uspUdtSendGift SP, BehaviorID : B36
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
    public class CBUdtSendGiftController : ApiController
    {
        public HttpResponseMessage Post(UdtSendGiftInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<UdtSendGiftInputParams>(decrypted);
                }
                catch (Exception ex)
                {
                    ex = (Exception)Activator.CreateInstance(ex.GetType(), "Decrypt Error", ex);
                    throw ex;
                }
            }

            // Get the sid or memberID of the current user.
            string sid = CBAuth.getMemberID(p.MemberID_MemberItem, this.User as ClaimsPrincipal);
            p.MemberID_MemberItem = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();
            RowcountResult rowcountResult = new RowcountResult();

            try
            {
                // task start log
                //logMessage.memberID = p.MemberID_MemberItem;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBUdtSendGiftController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspUdtSendGift", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DeleteORUpdate", SqlDbType.NVarChar, -1).Value = p.DeleteORUpdate.ToUpper();
                        command.Parameters.Add("@MemberItemID_MemberItem", SqlDbType.NVarChar, -1).Value = p.MemberItemID_MemberItem;
                        command.Parameters.Add("@MemberID_MemberItem", SqlDbType.NVarChar, -1).Value = p.MemberID_MemberItem;
                        command.Parameters.Add("@ItemListID_MemberItem", SqlDbType.NVarChar, -1).Value = p.ItemListID_MemberItem;
                        command.Parameters.Add("@ItemCount_MemberItem", SqlDbType.NVarChar, -1).Value = p.ItemCount_MemberItem;
                        command.Parameters.Add("@ItemStatus_MemberItem", SqlDbType.NVarChar, -1).Value = p.ItemStatus_MemberItem;
                        command.Parameters.Add("@sCol1_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol1_MemberItem;
                        command.Parameters.Add("@sCol2_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol2_MemberItem;
                        command.Parameters.Add("@sCol3_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol3_MemberItem;
                        command.Parameters.Add("@sCol4_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol4_MemberItem;
                        command.Parameters.Add("@sCol5_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol5_MemberItem;
                        command.Parameters.Add("@sCol6_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol6_MemberItem;
                        command.Parameters.Add("@sCol7_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol7_MemberItem;
                        command.Parameters.Add("@sCol8_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol8_MemberItem;
                        command.Parameters.Add("@sCol9_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol9_MemberItem;
                        command.Parameters.Add("@sCol10_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol10_MemberItem;
                        command.Parameters.Add("@GiftDepositoryID_GiftDepository", SqlDbType.NVarChar, -1).Value = p.GiftDepositoryID_GiftDepository;
                        command.Parameters.Add("@ItemListID_GiftDepository", SqlDbType.NVarChar, -1).Value = p.ItemListID_GiftDepository;
                        command.Parameters.Add("@ItemCount_GiftDepository", SqlDbType.NVarChar, -1).Value = p.ItemCount_GiftDepository;
                        command.Parameters.Add("@FromMemberID_GiftDepository", SqlDbType.NVarChar, -1).Value = p.FromMemberID_GiftDepository;
                        command.Parameters.Add("@ToMemberID_GiftDepository", SqlDbType.NVarChar, -1).Value = p.ToMemberID_GiftDepository;
                        command.Parameters.Add("@sCol1_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol1_GiftDepository;
                        command.Parameters.Add("@sCol2_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol2_GiftDepository;
                        command.Parameters.Add("@sCol3_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol3_GiftDepository;
                        command.Parameters.Add("@sCol4_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol4_GiftDepository;
                        command.Parameters.Add("@sCol5_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol5_GiftDepository;
                        command.Parameters.Add("@sCol6_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol6_GiftDepository;
                        command.Parameters.Add("@sCol7_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol7_GiftDepository;
                        command.Parameters.Add("@sCol8_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol8_GiftDepository;
                        command.Parameters.Add("@sCol9_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol9_GiftDepository;
                        command.Parameters.Add("@sCol10_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol10_GiftDepository;

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

                        // task end log
                        logMessage.memberID = p.MemberID_MemberItem;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBUdtSendGiftController";
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
                logMessage.memberID = p.MemberID_MemberItem;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBUdtSendGiftController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }
    }
}
