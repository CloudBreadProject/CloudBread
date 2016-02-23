/**
* @file CBComUdtMemberGameInfoesController.cs
* @brief Common API for a member game info update on MemberGameInfoes table. \n
* Set parameter null or remove json property for no change on column data.
* @author Dae Woo Kim
* @param MemberGameInfoes table object
* @return string "1" - affected rows
* @see uspComUdtMemberGameInfoes SP, BehaviorID : B58
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
    public class CBComUdtMemberGameInfoesController : ApiController
    {
        
        public class InputParams
        {
            public string MemberID { get; set; }
            public string Level { get; set; }
            public string Exps { get; set; }
            public string Points { get; set; }
            public string UserSTAT1 { get; set; }
            public string UserSTAT2 { get; set; }
            public string UserSTAT3 { get; set; }
            public string UserSTAT4 { get; set; }
            public string UserSTAT5 { get; set; }
            public string UserSTAT6 { get; set; }
            public string UserSTAT7 { get; set; }
            public string UserSTAT8 { get; set; }
            public string UserSTAT9 { get; set; }
            public string UserSTAT10 { get; set; }
            public string sCol1 { get; set; }
            public string sCol2 { get; set; }
            public string sCol3 { get; set; }
            public string sCol4 { get; set; }
            public string sCol5 { get; set; }
            public string sCol6 { get; set; }
            public string sCol7 { get; set; }
            public string sCol8 { get; set; }
            public string sCol9 { get; set; }
            public string sCol10 { get; set; }

        }

        public string Post(InputParams p)
        {
            string result = "";

            // Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = CBAuth.getMemberID(p.MemberID, claimsPrincipal);
            p.MemberID = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                // task start log
                //logMessage.memberID = p.MemberID;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBComUdtMemberGameInfoesController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspComUdtMemberGameInfoes", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.MemberID;
                        command.Parameters.Add("@Level", SqlDbType.NVarChar, -1).Value = p.Level;
                        command.Parameters.Add("@Exps", SqlDbType.NVarChar, -1).Value = p.Exps;
                        command.Parameters.Add("@Points", SqlDbType.NVarChar, -1).Value = p.Points;
                        command.Parameters.Add("@UserSTAT1", SqlDbType.NVarChar, -1).Value = p.UserSTAT1;
                        command.Parameters.Add("@UserSTAT2", SqlDbType.NVarChar, -1).Value = p.UserSTAT2;
                        command.Parameters.Add("@UserSTAT3", SqlDbType.NVarChar, -1).Value = p.UserSTAT3;
                        command.Parameters.Add("@UserSTAT4", SqlDbType.NVarChar, -1).Value = p.UserSTAT4;
                        command.Parameters.Add("@UserSTAT5", SqlDbType.NVarChar, -1).Value = p.UserSTAT5;
                        command.Parameters.Add("@UserSTAT6", SqlDbType.NVarChar, -1).Value = p.UserSTAT6;
                        command.Parameters.Add("@UserSTAT7", SqlDbType.NVarChar, -1).Value = p.UserSTAT7;
                        command.Parameters.Add("@UserSTAT8", SqlDbType.NVarChar, -1).Value = p.UserSTAT8;
                        command.Parameters.Add("@UserSTAT9", SqlDbType.NVarChar, -1).Value = p.UserSTAT9;
                        command.Parameters.Add("@UserSTAT10", SqlDbType.NVarChar, -1).Value = p.UserSTAT10;
                        command.Parameters.Add("@sCol1", SqlDbType.NVarChar, -1).Value = p.sCol1;
                        command.Parameters.Add("@sCol2", SqlDbType.NVarChar, -1).Value = p.sCol2;
                        command.Parameters.Add("@sCol3", SqlDbType.NVarChar, -1).Value = p.sCol3;
                        command.Parameters.Add("@sCol4", SqlDbType.NVarChar, -1).Value = p.sCol4;
                        command.Parameters.Add("@sCol5", SqlDbType.NVarChar, -1).Value = p.sCol5;
                        command.Parameters.Add("@sCol6", SqlDbType.NVarChar, -1).Value = p.sCol6;
                        command.Parameters.Add("@sCol7", SqlDbType.NVarChar, -1).Value = p.sCol7;
                        command.Parameters.Add("@sCol8", SqlDbType.NVarChar, -1).Value = p.sCol8;
                        command.Parameters.Add("@sCol9", SqlDbType.NVarChar, -1).Value = p.sCol9;
                        command.Parameters.Add("@sCol10", SqlDbType.NVarChar, -1).Value = p.sCol10;

                        connection.OpenWithRetry(retryPolicy);
                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                result = dreader[0].ToString();
                            }
                            dreader.Close();
                        }
                        connection.Close();

                        // task end log
                        logMessage.memberID = p.MemberID;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBComUdtMemberGameInfoesController";
                        logMessage.Message = jsonParam;
                        Logging.RunLog(logMessage);

                        return result;
                    }

                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.MemberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBComUdtMemberGameInfoesController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
