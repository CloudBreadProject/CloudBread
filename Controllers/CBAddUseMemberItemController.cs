/**
* @file CBAddUseMemberItemController.cs
* @brief add, update and remove memberitems and update MemberGameInfo. \n
* You can implement member item add+status change / use+status change / drop, remove + status change. \n
* First of all, check member inventory and set first param, "InsertORUpdateORDelete" branching memberitems.
* @author Dae Woo Kim
* @param string InsertORUpdateORDelete - branching memberitems table
* @param MemberItems table object
* @param MemberGameInfoes table object 
* @return string "2" - affected rows
* @see uspAddUseMemberItem SP, BehaviorID : B24, B41
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
    public class CBAddUseMemberItemController : ApiController
    {
        public HttpResponseMessage Post(AddUseMemberItemInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<AddUseMemberItemInputParams>(decrypted);
                }
                catch (Exception ex)
                {
                    ex = (Exception)Activator.CreateInstance(ex.GetType(), "Decrypt Error", ex);
                    throw ex;
                }
            }

            // Get the sid or memberID of the current user.
            string sid = CBAuth.getMemberID(p.MemberID_MemberGameInfoes, this.User as ClaimsPrincipal);
            p.MemberID_MemberGameInfoes = sid;
            p.MemberID_MemberItem = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();
            RowcountResult rowcountResult = new RowcountResult();

            try
            {
                // start task log
                //logMessage.memberID = p.MemberID_MemberItem;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBAddUseMemberItemController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspAddUseMemberItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@InsertORUpdateORDelete", SqlDbType.NVarChar, -1).Value = p.InsertORUpdateORDelete.ToUpper();
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
                                rowcountResult.result = dreader[0].ToString();
                            }
                            dreader.Close();
                        }
                        connection.Close();

                        // end task log
                        logMessage.memberID = p.MemberID_MemberItem;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBAddUseMemberItemController";
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
                logMessage.Logger = "CBAddUseMemberItemController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }
    }
}
