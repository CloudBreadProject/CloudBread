/**
* @file CBUdtMemberGameInfoStageController.cs
* @brief update membergameinfo and membergameinfostage \n
* during the game, used for update both membergameinfo and membergameinfostage table \n
* @author Dae Woo Kim
* @param string InsertORUpdate  - if game stage exists, then "UPDATE". if not, "INSERT".
* @param MemberGameInfoes and MemberGameInfoStages object
* @return string value "2" affected rows count
* @see uspUdtMemberGameInfoStage SP, BehaviorID : B44, B45
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
    public class CBUdtMemberGameInfoStageController : ApiController
    {
        public HttpResponseMessage Post(UdtMemberGameInfoStageInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<UdtMemberGameInfoStageInputParams>(decrypted);
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
            p.MemberID_MemberGameInfoStages = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();
            RowcountResult rowcountResult = new RowcountResult();

            try
            {
                // task start log
                //logMessage.memberID = p.MemberID_MemberGameInfoes;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBUdtMemberGameInfoStageController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspUdtMemberGameInfoStage", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@InsertORUpdate", SqlDbType.NVarChar, -1).Value = p.InsertORUpdate.ToUpper();
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
                        command.Parameters.Add("@MemberGameInfoStageID_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoStageID_MemberGameInfoStages;
                        command.Parameters.Add("@MemberID_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.MemberID_MemberGameInfoStages;
                        command.Parameters.Add("@StageName_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageName_MemberGameInfoStages;
                        command.Parameters.Add("@StageStatus_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStatus_MemberGameInfoStages;
                        command.Parameters.Add("@Category1_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Category1_MemberGameInfoStages;
                        command.Parameters.Add("@Category2_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Category2_MemberGameInfoStages;
                        command.Parameters.Add("@Category3_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Category3_MemberGameInfoStages;
                        command.Parameters.Add("@Mission1_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Mission1_MemberGameInfoStages;
                        command.Parameters.Add("@Mission2_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Mission2_MemberGameInfoStages;
                        command.Parameters.Add("@Mission3_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Mission3_MemberGameInfoStages;
                        command.Parameters.Add("@Mission4_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Mission4_MemberGameInfoStages;
                        command.Parameters.Add("@Mission5_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Mission5_MemberGameInfoStages;
                        command.Parameters.Add("@Points_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Points_MemberGameInfoStages;
                        command.Parameters.Add("@StageStat1_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStat1_MemberGameInfoStages;
                        command.Parameters.Add("@StageStat2_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStat2_MemberGameInfoStages;
                        command.Parameters.Add("@StageStat3_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStat3_MemberGameInfoStages;
                        command.Parameters.Add("@StageStat4_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStat4_MemberGameInfoStages;
                        command.Parameters.Add("@StageStat5_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStat5_MemberGameInfoStages;
                        command.Parameters.Add("@sCol1_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol1_MemberGameInfoStages;
                        command.Parameters.Add("@sCol2_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol2_MemberGameInfoStages;
                        command.Parameters.Add("@sCol3_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol3_MemberGameInfoStages;
                        command.Parameters.Add("@sCol4_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol4_MemberGameInfoStages;
                        command.Parameters.Add("@sCol5_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol5_MemberGameInfoStages;
                        command.Parameters.Add("@sCol6_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol6_MemberGameInfoStages;
                        command.Parameters.Add("@sCol7_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol7_MemberGameInfoStages;
                        command.Parameters.Add("@sCol8_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol8_MemberGameInfoStages;
                        command.Parameters.Add("@sCol9_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol9_MemberGameInfoStages;
                        command.Parameters.Add("@sCol10_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol10_MemberGameInfoStages;

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
                        logMessage.memberID = p.MemberID_MemberGameInfoes;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBUdtMemberGameInfoStageController";
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
                logMessage.memberID = p.MemberID_MemberGameInfoes;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBUdtMemberGameInfoStageController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }
    }
}
