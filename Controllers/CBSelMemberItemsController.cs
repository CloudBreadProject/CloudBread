/**
* @file CBSelMemberItemsController.cs
* @brief Get MemberItems data by paging. \n
* To get all data without paging, set big number "pageSize" param (max 9223372036854775807)
* @author Dae Woo Kim
* @param string memberID
* @param int64 page
* @param int64 pageSize
* @return MemberItems table object
* @see uspSelMemberItems SP, BehaviorID : B20, B23
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
    public class CBSelMemberItemsController : ApiController
    {
        public HttpResponseMessage Post(SelMemberItemsInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<SelMemberItemsInputParams>(decrypted);
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

            List<SelMemberItemsModel> result = new List<SelMemberItemsModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();

            try
            {
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspSelMemberItems", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Page", SqlDbType.BigInt).Value = p.Page;
                        command.Parameters.Add("@PageSize", SqlDbType.BigInt).Value = p.PageSize;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.MemberID;
                        connection.OpenWithRetry(retryPolicy);

                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                SelMemberItemsModel workItem = new SelMemberItemsModel()
                                {
                                    ROWNUM = dreader[0].ToString(),
                                    ItemListsItemName = dreader[1].ToString(),
                                    ItemListsItemDescription = dreader[2].ToString(),
                                    ItemListsItemPrice = dreader[3].ToString(),
                                    ItemListsItemSellPrice = dreader[4].ToString(),
                                    ItemListsItemCategory1 = dreader[5].ToString(),
                                    ItemListsItemCategory2 = dreader[6].ToString(),
                                    ItemListsItemCategory3 = dreader[7].ToString(),
                                    ItemListssCol1 = dreader[8].ToString(),
                                    ItemListssCol2 = dreader[9].ToString(),
                                    ItemListssCol3 = dreader[10].ToString(),
                                    ItemListssCol4 = dreader[11].ToString(),
                                    ItemListssCol5 = dreader[12].ToString(),
                                    ItemListssCol6 = dreader[13].ToString(),
                                    ItemListssCol7 = dreader[14].ToString(),
                                    ItemListssCol8 = dreader[15].ToString(),
                                    ItemListssCol9 = dreader[16].ToString(),
                                    ItemListssCol10 = dreader[17].ToString(),
                                    MemberItemsMemberItemID = dreader[18].ToString(),
                                    MemberItemsMemberID = dreader[19].ToString(),
                                    MemberItemsItemListID = dreader[20].ToString(),
                                    MemberItemsItemCount = dreader[21].ToString(),
                                    MemberItemsItemStatus = dreader[22].ToString(),
                                    MemberItemssCol1 = dreader[23].ToString(),
                                    MemberItemssCol2 = dreader[24].ToString(),
                                    MemberItemssCol3 = dreader[25].ToString(),
                                    MemberItemssCol4 = dreader[26].ToString(),
                                    MemberItemssCol5 = dreader[27].ToString(),
                                    MemberItemssCol6 = dreader[28].ToString(),
                                    MemberItemssCol7 = dreader[29].ToString(),
                                    MemberItemssCol8 = dreader[30].ToString(),
                                    MemberItemssCol9 = dreader[31].ToString(),
                                    MemberItemssCol10 = dreader[32].ToString()
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
                logMessage.Logger = "CBSelMemberItemsController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }
    }
}
