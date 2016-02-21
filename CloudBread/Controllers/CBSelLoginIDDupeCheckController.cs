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

namespace CloudBread.Controllers
{
    [MobileAppController]
    public class CBSelLoginIDDupeCheckController : ApiController
    {

        public class InputParams { public string memberID;}

        //return json
        public class Result { public string result; }

        public Result Post(InputParams p)      // //return json
        {
            //return json
            //string result = "";
            Result r = new Result();

            // Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = CBAuth.getMemberID(p.memberID, claimsPrincipal);
            p.memberID = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try 
	        {
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using(SqlCommand command = new SqlCommand("uspSelLoginIDDupeCheck", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.memberID;
                        connection.OpenWithRetry(retryPolicy);
                        using(SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                // change
                                r.result = dreader[0].ToString();
                            }
                            dreader.Close();
                        }
                        connection.Close();

                        // return json
                        //return result;
                        return r;
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
