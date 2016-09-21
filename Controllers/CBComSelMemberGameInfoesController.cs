/**
* @file CBComSelMemberGameInfoesController.cs
* @brief Common API for Get member game infoes of memberID - return MemberGameInfoes info. \n
* @author Dae Woo Kim
* @param string memberID
* @return MemberGameInfoes table object
* @see uspComSelMemberGameInfoes SP, BehaviorID : B37, B57
* @todo paging, filter by (EventCategory1, EventCategory2, EventCategory3, TargetGroup, TargetOS, TargetDevice) option support
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
    public class CBComSelMemberGameInfoesController : ApiController
    {
        public HttpResponseMessage Post(ComSelMemberGameInfoesInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<ComSelMemberGameInfoesInputParams>(decrypted);

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

            List<ComSelMemberGameInfoesModel> result = new List<ComSelMemberGameInfoesModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();

            try
            {
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspComSelMemberGameInfoes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.MemberID;
                        connection.OpenWithRetry(retryPolicy);

                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                ComSelMemberGameInfoesModel workItem = new ComSelMemberGameInfoesModel()
                                {
                                    MemberID = dreader[0].ToString(),
                                    Level = dreader[1].ToString(),
                                    Exps = dreader[2].ToString(),
                                    Points = dreader[3].ToString(),
                                    UserSTAT1 = dreader[4].ToString(),
                                    UserSTAT2 = dreader[5].ToString(),
                                    UserSTAT3 = dreader[6].ToString(),
                                    UserSTAT4 = dreader[7].ToString(),
                                    UserSTAT5 = dreader[8].ToString(),
                                    UserSTAT6 = dreader[9].ToString(),
                                    UserSTAT7 = dreader[10].ToString(),
                                    UserSTAT8 = dreader[11].ToString(),
                                    UserSTAT9 = dreader[12].ToString(),
                                    UserSTAT10 = dreader[13].ToString(),
                                    sCol1 = dreader[14].ToString(),
                                    sCol2 = dreader[15].ToString(),
                                    sCol3 = dreader[16].ToString(),
                                    sCol4 = dreader[17].ToString(),
                                    sCol5 = dreader[18].ToString(),
                                    sCol6 = dreader[19].ToString(),
                                    sCol7 = dreader[20].ToString(),
                                    sCol8 = dreader[21].ToString(),
                                    sCol9 = dreader[22].ToString(),
                                    sCol10 = dreader[23].ToString()

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
                logMessage.memberID = p.MemberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBComSelMemberGameInfoesController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
