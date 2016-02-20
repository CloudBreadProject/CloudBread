/**
* @file CBComUdtMemberGameInfoStagesController.cs
* @brief Common API for a game stage update on MemberGameInfoStages table. \n
* Set parameter null or remove json property for no change on column data.
* @author Dae Woo Kim
* @param MemberGameInfoStages object
* @return string "1" - affected rows
* @see uspComUdtMemberGameInfoStages SP, BehaviorID : B64
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

namespace CloudBread.Controllers
{
    [MobileAppController]
    public class CBComUdtMemberGameInfoStagesController : ApiController
    {
        public class InputParams
        {
            public string MemberGameInfoStageID { get; set; }
            public string MemberID { get; set; }
            public string StageName { get; set; }
            public string StageStatus { get; set; }
            public string Category1 { get; set; }
            public string Category2 { get; set; }
            public string Category3 { get; set; }
            public string Mission1 { get; set; }
            public string Mission2 { get; set; }
            public string Mission3 { get; set; }
            public string Mission4 { get; set; }
            public string Mission5 { get; set; }
            public string Points { get; set; }
            public string StageStat1 { get; set; }
            public string StageStat2 { get; set; }
            public string StageStat3 { get; set; }
            public string StageStat4 { get; set; }
            public string StageStat5 { get; set; }
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
                //logMessage.Logger = "CBComUdtMemberGameInfoStagesController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspComUdtMemberGameInfoStages", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberGameInfoStageID", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoStageID;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.MemberID;
                        command.Parameters.Add("@StageName", SqlDbType.NVarChar, -1).Value = p.StageName;
                        command.Parameters.Add("@StageStatus", SqlDbType.NVarChar, -1).Value = p.StageStatus;
                        command.Parameters.Add("@Category1", SqlDbType.NVarChar, -1).Value = p.Category1;
                        command.Parameters.Add("@Category2", SqlDbType.NVarChar, -1).Value = p.Category2;
                        command.Parameters.Add("@Category3", SqlDbType.NVarChar, -1).Value = p.Category3;
                        command.Parameters.Add("@Mission1", SqlDbType.NVarChar, -1).Value = p.Mission1;
                        command.Parameters.Add("@Mission2", SqlDbType.NVarChar, -1).Value = p.Mission2;
                        command.Parameters.Add("@Mission3", SqlDbType.NVarChar, -1).Value = p.Mission3;
                        command.Parameters.Add("@Mission4", SqlDbType.NVarChar, -1).Value = p.Mission4;
                        command.Parameters.Add("@Mission5", SqlDbType.NVarChar, -1).Value = p.Mission5;
                        command.Parameters.Add("@Points", SqlDbType.NVarChar, -1).Value = p.Points;
                        command.Parameters.Add("@StageStat1", SqlDbType.NVarChar, -1).Value = p.StageStat1;
                        command.Parameters.Add("@StageStat2", SqlDbType.NVarChar, -1).Value = p.StageStat2;
                        command.Parameters.Add("@StageStat3", SqlDbType.NVarChar, -1).Value = p.StageStat3;
                        command.Parameters.Add("@StageStat4", SqlDbType.NVarChar, -1).Value = p.StageStat4;
                        command.Parameters.Add("@StageStat5", SqlDbType.NVarChar, -1).Value = p.StageStat5;
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

                        connection.Open();
                        using (SqlDataReader dreader = command.ExecuteReader())
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
                        logMessage.Logger = "CBComUdtMemberGameInfoStagesController";
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
                logMessage.Logger = "CBComUdtMemberGameInfoStagesController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}


