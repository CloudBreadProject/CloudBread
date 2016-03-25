/**
* @file CBSelLoginIDDupeCheckController.cs
* @brief Login id dupe check controller. Mobile client POST memberID as json format. \n
* Check memberid duplication in members table. Consider using 3rd party authentication.
* @author Dae Woo Kim
* @param string memberID 
* @return string value "0" or "1" : false or true
* @see uspSelLoginIDDupeCheck SP, BehaviorID : B01
* @todo return result is json format object / check change or not
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
    public class CBSelLoginIDDupeCheckController : ApiController
    {
        public HttpResponseMessage Post(SelLoginIDDupeCheckInputParams p)
        {
            // try decrypt data
            if (!string.IsNullOrEmpty(p.token) && globalVal.CloudBreadCryptSetting == "AES256")
            {
                try
                {
                    string decrypted = Crypto.AES_decrypt(p.token, globalVal.CloudBreadCryptKey, globalVal.CloudBreadCryptIV);
                    p = JsonConvert.DeserializeObject<SelLoginIDDupeCheckInputParams>(decrypted);
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

            SelLoginIDDupeCheckResult result = new SelLoginIDDupeCheckResult();
            HttpResponseMessage response = new HttpResponseMessage();
            EncryptedData encryptedResult = new EncryptedData();

            try 
	        {
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using(SqlCommand command = new SqlCommand("uspSelLoginIDDupeCheck", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@FindID", SqlDbType.NVarChar, -1).Value = p.findID;
                        command.Parameters.Add("@Category", SqlDbType.NVarChar, -1).Value = p.category;
                        connection.OpenWithRetry(retryPolicy);
                        using(SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                result.result = dreader[0].ToString();
                            }
                            dreader.Close();
                        }
                        connection.Close();

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
            }
        
	        catch (Exception ex)
	        {
                // error log
                logMessage.memberID = p.memberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBSelLoginIDDupeCheckController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
	        }
        }
    }
}
