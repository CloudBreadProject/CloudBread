/**
* @file CBSelItem1Controller.cs
* @brief Get 1 item data API from ItemLists table  \n
* same with "CBSelItem1Controller"
* @author Dae Woo Kim
* @param string MemberID - log purpose
* @param string ItemListID
* @return itemlists table object
* @see uspSelItem1 SP, BehaviorID : B26
* @todo duplicate with "CBComSelItemList1Controller"
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
    public class CBSelItem1Controller : ApiController
    {
        public HttpResponseMessage Post(SelItem1InputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<SelItem1InputParams>(decrypted);
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

            List<SelItem1Model> result = new List<SelItem1Model>();
            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();

            try
            {
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspSelItem1", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ItemListID", SqlDbType.NVarChar, -1).Value = p.ItemListID;
                        connection.OpenWithRetry(retryPolicy);

                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                SelItem1Model workItem = new SelItem1Model()
                                {
                                    ItemListID = dreader[0].ToString(),
                                    ItemName = dreader[1].ToString(),
                                    ItemDescription = dreader[2].ToString(),
                                    ItemPrice = dreader[3].ToString(),
                                    ItemSellPrice = dreader[4].ToString(),
                                    ItemCategory1 = dreader[5].ToString(),
                                    ItemCategory2 = dreader[6].ToString(),
                                    ItemCategory3 = dreader[7].ToString(),
                                    sCol1 = dreader[8].ToString(),
                                    sCol2 = dreader[9].ToString(),
                                    sCol3 = dreader[10].ToString(),
                                    sCol4 = dreader[11].ToString(),
                                    sCol5 = dreader[12].ToString(),
                                    sCol6 = dreader[13].ToString(),
                                    sCol7 = dreader[14].ToString(),
                                    sCol8 = dreader[15].ToString(),
                                    sCol9 = dreader[16].ToString(),
                                    sCol10 = dreader[17].ToString(),
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
                logMessage.Logger = "CBSelItem1Controller";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }
    }
}
