/**
* @file CBSelSendEmailToMemberController.cs
* @brief Send email to a member. \n
* Mobile client POST memberID as json format. \n
* This procedure return email address of member. \n 
* Use "CloudBreadlib/BAL/SendSMTPMail" to send SMTP email in the code. \n
* @author Dae Woo Kim
* @param string memberid
* @return string emailaddress or sendmail result
* @see uspSelSendEmailToMember SP, BehaviorID : B04
* @todo implement code for SendSMTPMail in side of class with authentication
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
using CloudBreadLib.BAL.SendSMTPMail;
using Newtonsoft.Json;
using CloudBreadAuth;
using System.Security.Claims;
using Microsoft.Practices.TransientFaultHandling;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;

namespace CloudBread.Controllers
{
    [MobileAppController]
    public class CBSelSendEmailToMemberController : ApiController
    {

        string result ="";
        public class InputParams { 
            public string memberID;
        }

        public string Post(InputParams p)
        {
            // Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = CBAuth.getMemberID(p.memberID, claimsPrincipal);
            p.memberID = sid;

            // check proper authentication of member who trigger this API (Admin or member with authorized)
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspSelSendEmailToMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.memberID;
                        connection.OpenWithRetry(retryPolicy);
                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                result = dreader[0].ToString();
                            }
                            //////////////////////////////////////////////////////////////////////////////////////
                            //// mail sending module - reference CloudBreadlib/BAL/SendSMTPMail
                            //// check firewall, anti-virus and outbound traffic
                            //// in SendEmail lib, change your mail login info
                            //string s = SendSMTPMail.SendEmail(dreader[0].ToString(), "subject", "content");
                            //////////////////////////////////////////////////////////////////////////////////////

                            dreader.Close();
                        }
                        connection.Close();

                        return result;  // or return mail send result string s
                    }

                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.memberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBSelSendEmailToMemberController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw ;
            }
        }

    }
}
