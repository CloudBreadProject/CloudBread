/**
* @file CBComUdtMemberGameInfoStagesController.cs
* @brief Common API for a game stage update on MemberGameInfoStages table. \n
* Set parameter null or remove json property for no change on column data.
* @author Dae Woo Kim
* @param MemberGameInfoStages object
* @return string "1" - affected rows
* @see uspComUdtMemberGameInfoStages SP, BehaviorID : B64
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
    public class CBComUdtMemberGameInfoStagesController : ApiController
    {
        public HttpResponseMessage Post(ComUdtMemberGameInfoStagesInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<ComUdtMemberGameInfoStagesInputParams>(decrypted);
                }
                catch (Exception ex)
                {
                    ex = (Exception)Activator.CreateInstance(ex.GetType(), "Decrypt Error", ex);
                    throw ex;
                }
            }

            // Get the sid or memberID of the current user.
            string sid = CBAuth.getMemberID(p.MemberID, this.User as ClaimsPrincipal);
            p.MemberID = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();
            RowcountResult rowcountResult = new RowcountResult();

            try
            {
                // task start log
                //logMessage.memberID = p.MemberID;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBComUdtMemberGameInfoStagesController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspComUdtMemberGameInfoStages", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberGameInfoStageID", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoStageID;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.MemberID;
                        command.Parameters.Add("@StageName", SqlDbType.NVarChar, -1).Value = p.StageName;
                        command.Parameters.Add("@StageStatus", SqlDbType.NVarChar, -1).Value = p.StageStatus;
                        command.Parameters.Add("@Category1", SqlDbType.NVarChar, -1).Value = p.Category1;
                        command.Parameters.Add("@Category2", SqlDbType.NVarChar, -1).Value = p.Category2;
                        command.Parameters.Add("@Category3", SqlDbType.NVarChar, -1).Value = p.Category3;
                        command.Parameters.Add("@Mission1", SqlDbType.NVarChar, -1).Value = p.Mission1;
                        command.Parameters.Add("@Mission2", SqlDbType.NVarChar, -1).Value = p.Mission2;
                        command.Parameters.Add("@Mission3", SqlDbType.NVarChar, -1).Value = p.Mission3;
                        command.Parameters.Add("@Mission4", SqlDbType.NVarChar, -1).Value = p.Mission4;
                        command.Parameters.Add("@Mission5", SqlDbType.NVarChar, -1).Value = p.Mission5;
                        command.Parameters.Add("@Points", SqlDbType.NVarChar, -1).Value = p.Points;
                        command.Parameters.Add("@StageStat1", SqlDbType.NVarChar, -1).Value = p.StageStat1;
                        command.Parameters.Add("@StageStat2", SqlDbType.NVarChar, -1).Value = p.StageStat2;
                        command.Parameters.Add("@StageStat3", SqlDbType.NVarChar, -1).Value = p.StageStat3;
                        command.Parameters.Add("@StageStat4", SqlDbType.NVarChar, -1).Value = p.StageStat4;
                        command.Parameters.Add("@StageStat5", SqlDbType.NVarChar, -1).Value = p.StageStat5;
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
                                rowcountResult.result = dreader[0].ToString();
                            }
                            dreader.Close();
                        }
                        connection.Close();

                        // task end log
                        logMessage.memberID = p.MemberID;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBComUdtMemberGameInfoStagesController";
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
                logMessage.memberID = p.MemberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBComUdtMemberGameInfoStagesController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}


