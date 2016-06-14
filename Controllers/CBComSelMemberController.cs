/**
* @file CBComSelMemberController.cs
* @brief Get 1 member info from Members table \n
* @author Dae Woo Kim
* @param string memberID
* @return Members table object
* @see uspComSelMember SP, BehaviorID : B51
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
    public class CBComSelMemberController : ApiController
    {
        public HttpResponseMessage Post(ComSelMemberInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<ComSelMemberInputParams>(decrypted);

                }
                catch (Exception ex)
                {
                    ex = (Exception)Activator.CreateInstance(ex.GetType(), "Decrypt Error", ex);
                    throw ex;
                }
            }

            //// Get the sid or memberID of the current user.
            //string sid = CBAuth.getMemberID(p.memberID, this.User as ClaimsPrincipal);
            //p.memberID = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            List<ComSelMemberModel> result = new List<ComSelMemberModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();

            try
            {
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspComSelMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.memberID;
                        connection.OpenWithRetry(retryPolicy);

                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                ComSelMemberModel workItem = new ComSelMemberModel()
                                {
                                    MemberID = dreader[0].ToString(),
                                    MemberPWD = dreader[1].ToString(),
                                    EmailAddress = dreader[2].ToString(),
                                    EmailConfirmedYN = dreader[3].ToString(),
                                    PhoneNumber1 = dreader[4].ToString(),
                                    PhoneNumber2 = dreader[5].ToString(),
                                    PINumber = dreader[6].ToString(),
                                    Name1 = dreader[7].ToString(),
                                    Name2 = dreader[8].ToString(),
                                    Name3 = dreader[9].ToString(),
                                    DOB = dreader[10].ToString(),
                                    RecommenderID = dreader[11].ToString(),
                                    MemberGroup = dreader[12].ToString(),
                                    LastDeviceID = dreader[13].ToString(),
                                    LastIPaddress = dreader[14].ToString(),
                                    LastLoginDT = dreader[15].ToString(),
                                    LastLogoutDT = dreader[16].ToString(),
                                    LastMACAddress = dreader[17].ToString(),

                                    AccountBlockYN = dreader[18].ToString(),
                                    AccountBlockEndDT = dreader[19].ToString(),
                                    AnonymousYN = dreader[20].ToString(),

                                    _3rdAuthProvider = dreader[21].ToString(),
                                    _3rdAuthID = dreader[22].ToString(),
                                    _3rdAuthParam = dreader[23].ToString(),
                                    PushNotificationID = dreader[24].ToString(),
                                    PushNotificationProvider = dreader[25].ToString(),
                                    PushNotificationGroup = dreader[26].ToString(),

                                    sCol1 = dreader[27].ToString(),
                                    sCol2 = dreader[28].ToString(),
                                    sCol3 = dreader[29].ToString(),
                                    sCol4 = dreader[30].ToString(),
                                    sCol5 = dreader[31].ToString(),
                                    sCol6 = dreader[32].ToString(),
                                    sCol7 = dreader[33].ToString(),
                                    sCol8 = dreader[34].ToString(),
                                    sCol9 = dreader[35].ToString(),
                                    sCol10 = dreader[36].ToString()
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
                logMessage.Logger = "CBComSelMemberController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
