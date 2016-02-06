/**
* @file CBSelSendEmailToMemberController.cs
* @brief send email to a member \n
* mobile client POST memberID as json format \n
* this procedure return email address of member \n 
* use "CloudBreadlib/BAL/SendSMTPMail" to send SMTP email in the code \n
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
            // check proper authentication of member who trigger this API (Admin or member with authorized)
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspSelSendEmailToMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.memberID;
                        connection.Open();
                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                result = dreader[0].ToString();
                            }
                            //////////////////////////////////////////////////////////////////////////////////////
                            // mail sending module - reference CloudBreadlib/BAL/SendSMTPMail
                            // check firewall, anti-virus and outbound traffic
                            // in SendEmail lib, change your mail login info
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
