/**
* @file CBUdtConfirmedEmailAddressController.cs
* @brief !not implemented \n
* Confirm email address of member. \n
* Need todo : this controller made for email address validate and confirmation. \n
* This controller could be accessed with mobile browser(without appkey authentication). \n
* It has to be seperated in mobile app appkey authentication and provide hashed string for member email address param. \n 
* @author Dae Woo Kim
* @param todo : hashed member unique value from member's email click
* @return string "1" - rows affected
* @see uspUdtConfirmedEmailAddress SP, BehaviorID : B05
* @todo implement code logic
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

namespace CloudBread.Controllers
{
    // detour mobile app appkey authentication
    [MobileAppController]
    public class CBUdtConfirmedEmailAddressController : ApiController
    {
        string result;

        public class InputParams { 
            public string memberID;         // todo list
            public string memberPWD;
        }

        public string Post(InputParams p)
        {
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspUdtConfirmedEmailAddress", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.memberID;
                        command.Parameters.Add("@MemberPWD", SqlDbType.NVarChar, -1).Value = p.memberPWD;
                        connection.Open();
                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                result =  dreader[0].ToString();
                            }

                            dreader.Close();
                        }
                        connection.Close();

                        return result;
                    }

                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.memberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBUdtConfirmedEmailAddressController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
