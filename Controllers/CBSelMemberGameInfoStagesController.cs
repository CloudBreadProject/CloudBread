/**
* @file CBSelMemberGameInfoStagesController.cs
* @brief Get game stages list from MemberGameInfoStages table. \n
* @author Dae Woo Kim
* @param string memberID
* @return MemberGameInfoStages table object
* @see uspSelMemberGameInfoStages SP, BehaviorID : B46
* @todo paging, filter option support
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
    public class CBSelMemberGameInfoStagesController : ApiController
    {
        public HttpResponseMessage Post(SelMemberGameInfoStagesInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<SelMemberGameInfoStagesInputParams>(decrypted);
                }
                catch (Exception ex)
                {
                    ex = (Exception)Activator.CreateInstance(ex.GetType(), "Decrypt Error", ex);
                    throw ex;
                }
            }

            // Get the sid or memberID of the current user.
            string sid = CBAuth.getMemberID(p.memberID, this.User as ClaimsPrincipal);
            p.memberID = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            List<SelMemberGameInfoStagesModel> result = new List<SelMemberGameInfoStagesModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();

            try
            {
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspSelMemberGameInfoStages", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.memberID;
                        connection.OpenWithRetry(retryPolicy);

                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                SelMemberGameInfoStagesModel workItem = new SelMemberGameInfoStagesModel()
                                {
                                    MemberGameInfoStageID = dreader[0].ToString(),
                                    MemberID = dreader[1].ToString(),
                                    StageName = dreader[2].ToString(),
                                    StageStatus = dreader[3].ToString(),
                                    Category1 = dreader[4].ToString(),
                                    Category2 = dreader[5].ToString(),
                                    Category3 = dreader[6].ToString(),
                                    Mission1 = dreader[7].ToString(),
                                    Mission2 = dreader[8].ToString(),
                                    Mission3 = dreader[9].ToString(),
                                    Mission4 = dreader[10].ToString(),
                                    Mission5 = dreader[11].ToString(),
                                    Points = dreader[12].ToString(),
                                    StageStat1 = dreader[13].ToString(),
                                    StageStat2 = dreader[14].ToString(),
                                    StageStat3 = dreader[15].ToString(),
                                    StageStat4 = dreader[16].ToString(),
                                    StageStat5 = dreader[17].ToString(),
                                    sCol1 = dreader[18].ToString(),
                                    sCol2 = dreader[19].ToString(),
                                    sCol3 = dreader[20].ToString(),
                                    sCol4 = dreader[21].ToString(),
                                    sCol5 = dreader[22].ToString(),
                                    sCol6 = dreader[23].ToString(),
                                    sCol7 = dreader[24].ToString(),
                                    sCol8 = dreader[25].ToString(),
                                    sCol9 = dreader[26].ToString(),
                                    sCol10 = dreader[27].ToString()

                                };
                                result.Add(workItem);
                            }
                            dreader.Close();
                        }
                        connection.Close();
                    }

                    /// Encrypt the result response
                    if (globalVal.CloudBreadCryptSetting == "AES256")
                    {
                        try
                        {
                            encryptedResult.token = Crypto.AES_encrypt(JsonConvert.SerializeObject(result), globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                            response = Request.CreateResponse(HttpStatusCode.OK, encryptedResult);
                            return response;
                        }
                        catch (Exception ex)
                        {
                            ex = (Exception)Activator.CreateInstance(ex.GetType(), "Encrypt Error", ex);
                            throw ex;
                        }
                    }

                    response = Request.CreateResponse(HttpStatusCode.OK, result);
                    return response;
                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.memberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBSelMemberGameInfoStagesController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
