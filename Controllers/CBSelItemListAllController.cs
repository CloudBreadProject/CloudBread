/**
* @file CBSelItemListAllController.cs
* @brief Get ItemLists table data by paging. \n
* To get all data without paging, set big number "pageSize" param (max 9223372036854775807).
* @author Dae Woo Kim
* @param string memberID - log purpose
* @param int64 page
* @param int64 pageSize
* @return itemlists table object
* @see uspSelItemListAll SP, BehaviorID : B19
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
    public class CBSelItemListAllController : ApiController
    {
        public HttpResponseMessage Post(SelItemListAllInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<SelItemListAllInputParams>(decrypted);
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

            List<SelItemListAllModel> result = new List<SelItemListAllModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();

            try
            {
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspSelItemListAll", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Page", SqlDbType.BigInt).Value = p.Page;
                        command.Parameters.Add("@PageSize", SqlDbType.BigInt).Value = p.PageSize;
                        connection.OpenWithRetry(retryPolicy);

                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                SelItemListAllModel workItem = new SelItemListAllModel()
                                {
                                    ROWNUM = dreader[0].ToString(),
                                    ItemListID = dreader[1].ToString(),
                                    ItemName = dreader[2].ToString(),
                                    ItemDescription = dreader[3].ToString(),
                                    ItemPrice = dreader[4].ToString(),
                                    ItemSellPrice = dreader[5].ToString(),
                                    ItemCategory1 = dreader[6].ToString(),
                                    ItemCategory2 = dreader[7].ToString(),
                                    ItemCategory3 = dreader[8].ToString(),
                                    sCol1 = dreader[9].ToString(),
                                    sCol2 = dreader[10].ToString(),
                                    sCol3 = dreader[11].ToString(),
                                    sCol4 = dreader[12].ToString(),
                                    sCol5 = dreader[13].ToString(),
                                    sCol6 = dreader[14].ToString(),
                                    sCol7 = dreader[15].ToString(),
                                    sCol8 = dreader[16].ToString(),
                                    sCol9 = dreader[17].ToString(),
                                    sCol10 = dreader[18].ToString()
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
                logMessage.Logger = "CBSelItemListAllController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw ex;
            }
        }
    }
}
