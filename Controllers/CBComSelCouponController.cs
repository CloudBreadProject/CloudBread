/**
* @file CBComSelCouponController.cs
* @brief Get 1 coupon data from Coupon table \n
* @author Dae Woo Kim
* @param string CouponID 
* @param string MemberID
* @return Coupon table object
* @see uspComSelCoupon SP, BehaviorID : B71
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
    public class CBComSelCouponController : ApiController
    {
        public HttpResponseMessage Post(ComSelCouponInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<ComSelCouponInputParams>(decrypted);
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

            List<ComSelCouponModel> result = new List<ComSelCouponModel>();
            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();

            try
            {
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspComSelCoupon", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@CouponID", SqlDbType.NVarChar, -1).Value = p.CouponID;
                        connection.OpenWithRetry(retryPolicy);

                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                ComSelCouponModel workItem = new ComSelCouponModel()
                                {
                                    CouponID = dreader[0].ToString(),
                                    CouponCategory1 = dreader[1].ToString(),
                                    CouponCategory2 = dreader[2].ToString(),
                                    CouponCategory3 = dreader[3].ToString(),
                                    ItemListID = dreader[4].ToString(),
                                    ItemCount = dreader[5].ToString(),
                                    ItemStatus = dreader[6].ToString(),
                                    TargetGroup = dreader[7].ToString(),
                                    TargetOS = dreader[8].ToString(),
                                    TargetDevice = dreader[9].ToString(),
                                    Title = dreader[10].ToString(),
                                    Content = dreader[11].ToString(),
                                    sCol1 = dreader[12].ToString(),
                                    sCol2 = dreader[13].ToString(),
                                    sCol3 = dreader[14].ToString(),
                                    sCol4 = dreader[15].ToString(),
                                    sCol5 = dreader[16].ToString(),
                                    sCol6 = dreader[17].ToString(),
                                    sCol7 = dreader[18].ToString(),
                                    sCol8 = dreader[19].ToString(),
                                    sCol9 = dreader[20].ToString(),
                                    sCol10 = dreader[21].ToString(),
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
                logMessage.Logger = "CBComSelCouponController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }
    }
}
