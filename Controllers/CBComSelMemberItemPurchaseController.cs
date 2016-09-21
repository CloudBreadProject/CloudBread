/**
* @file CBComSelMemberItemPurchaseController.cs
* @brief Get 1 member item purchase data info from MemberItemPurchase table \n
* @author Dae Woo Kim
* @param string MemberID - log purpose
* @param string MemberItemPurchaseID
* @return MemberItemPurchase table object
* @see uspComSelMemberItemPurchase SP, BehaviorID : B59
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
    public class CBComSelMemberItemPurchaseController : ApiController
    {
        public HttpResponseMessage Post(ComSelMemberItemPurchaseInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<ComSelMemberItemPurchaseInputParams>(decrypted);
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

            List<ComSelMemberItemPurchaseModel> result = new List<ComSelMemberItemPurchaseModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();

            try
            {
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspComSelMemberItemPurchase", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberItemPurchaseID", SqlDbType.NVarChar, -1).Value = p.MemberItemPurchaseID;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.MemberID;
                        connection.OpenWithRetry(retryPolicy);

                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                ComSelMemberItemPurchaseModel workItem = new ComSelMemberItemPurchaseModel()
                                {
                                    MemberItemPurchaseID = dreader[0].ToString(),
                                    MemberID = dreader[1].ToString(),
                                    ItemListID = dreader[2].ToString(),
                                    PurchaseQuantity = dreader[3].ToString(),
                                    PurchasePrice = dreader[4].ToString(),
                                    PGinfo1 = dreader[5].ToString(),
                                    PGinfo2 = dreader[6].ToString(),
                                    PGinfo3 = dreader[7].ToString(),
                                    PGinfo4 = dreader[8].ToString(),
                                    PGinfo5 = dreader[9].ToString(),
                                    PurchaseDeviceID = dreader[10].ToString(),
                                    PurchaseDeviceIPAddress = dreader[11].ToString(),
                                    PurchaseDeviceMACAddress = dreader[12].ToString(),
                                    PurchaseDT = dreader[13].ToString(),
                                    PurchaseCancelYN = dreader[14].ToString(),
                                    PurchaseCancelDT = dreader[15].ToString(),
                                    PurchaseCancelingStatus = dreader[16].ToString(),
                                    PurchaseCancelReturnedAmount = dreader[17].ToString(),
                                    PurchaseCancelDeviceID = dreader[18].ToString(),
                                    PurchaseCancelDeviceIPAddress = dreader[19].ToString(),
                                    PurchaseCancelDeviceMACAddress = dreader[20].ToString(),
                                    sCol1 = dreader[21].ToString(),
                                    sCol2 = dreader[22].ToString(),
                                    sCol3 = dreader[23].ToString(),
                                    sCol4 = dreader[24].ToString(),
                                    sCol5 = dreader[25].ToString(),
                                    sCol6 = dreader[26].ToString(),
                                    sCol7 = dreader[27].ToString(),
                                    sCol8 = dreader[28].ToString(),
                                    sCol9 = dreader[29].ToString(),
                                    sCol10 = dreader[30].ToString()

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
                logMessage.Logger = "CBComSelMemberItemPurchaseController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
