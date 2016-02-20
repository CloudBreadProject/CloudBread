/**
* @file CBUdtMemberGameInfoStageController.cs
* @brief update membergameinfo and membergameinfostage \n
* during the game, used for update both membergameinfo and membergameinfostage table \n
* @author Dae Woo Kim
* @param string InsertORUpdate  - if game stage exists, then "UPDATE". if not, "INSERT".
* @param MemberGameInfoes and MemberGameInfoStages object
* @return string value "2" affected rows count
* @see uspUdtMemberGameInfoStage SP, BehaviorID : B44, B45
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
    public class CBUdtMemberGameInfoStageController : ApiController
    {
        
        public class InputParams
        {
            public string InsertORUpdate { get; set; }
            public string MemberID_MemberGameInfoes { get; set; }
            public string Level_MemberGameInfoes { get; set; }
            public string Exps_MemberGameInfoes { get; set; }
            public string Points_MemberGameInfoes { get; set; }
            public string UserSTAT1_MemberGameInfoes { get; set; }
            public string UserSTAT2_MemberGameInfoes { get; set; }
            public string UserSTAT3_MemberGameInfoes { get; set; }
            public string UserSTAT4_MemberGameInfoes { get; set; }
            public string UserSTAT5_MemberGameInfoes { get; set; }
            public string UserSTAT6_MemberGameInfoes { get; set; }
            public string UserSTAT7_MemberGameInfoes { get; set; }
            public string UserSTAT8_MemberGameInfoes { get; set; }
            public string UserSTAT9_MemberGameInfoes { get; set; }
            public string UserSTAT10_MemberGameInfoes { get; set; }
            public string sCol1_MemberGameInfoes { get; set; }
            public string sCol2_MemberGameInfoes { get; set; }
            public string sCol3_MemberGameInfoes { get; set; }
            public string sCol4_MemberGameInfoes { get; set; }
            public string sCol5_MemberGameInfoes { get; set; }
            public string sCol6_MemberGameInfoes { get; set; }
            public string sCol7_MemberGameInfoes { get; set; }
            public string sCol8_MemberGameInfoes { get; set; }
            public string sCol9_MemberGameInfoes { get; set; }
            public string sCol10_MemberGameInfoes { get; set; }
            public string MemberGameInfoStageID_MemberGameInfoStages { get; set; }
            public string MemberID_MemberGameInfoStages { get; set; }
            public string StageName_MemberGameInfoStages { get; set; }
            public string StageStatus_MemberGameInfoStages { get; set; }
            public string Category1_MemberGameInfoStages { get; set; }
            public string Category2_MemberGameInfoStages { get; set; }
            public string Category3_MemberGameInfoStages { get; set; }
            public string Mission1_MemberGameInfoStages { get; set; }
            public string Mission2_MemberGameInfoStages { get; set; }
            public string Mission3_MemberGameInfoStages { get; set; }
            public string Mission4_MemberGameInfoStages { get; set; }
            public string Mission5_MemberGameInfoStages { get; set; }
            public string Points_MemberGameInfoStages { get; set; }
            public string StageStat1_MemberGameInfoStages { get; set; }
            public string StageStat2_MemberGameInfoStages { get; set; }
            public string StageStat3_MemberGameInfoStages { get; set; }
            public string StageStat4_MemberGameInfoStages { get; set; }
            public string StageStat5_MemberGameInfoStages { get; set; }
            public string sCol1_MemberGameInfoStages { get; set; }
            public string sCol2_MemberGameInfoStages { get; set; }
            public string sCol3_MemberGameInfoStages { get; set; }
            public string sCol4_MemberGameInfoStages { get; set; }
            public string sCol5_MemberGameInfoStages { get; set; }
            public string sCol6_MemberGameInfoStages { get; set; }
            public string sCol7_MemberGameInfoStages { get; set; }
            public string sCol8_MemberGameInfoStages { get; set; }
            public string sCol9_MemberGameInfoStages { get; set; }
            public string sCol10_MemberGameInfoStages { get; set; }



        }

        public string Post(InputParams p)
        {
            string result = "";

            // Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = CBAuth.getMemberID(p.MemberID_MemberGameInfoes, claimsPrincipal);
            p.MemberID_MemberGameInfoes = sid;
            p.MemberID_MemberGameInfoStages = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                // task start log
                //logMessage.memberID = p.MemberID_MemberGameInfoes;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBUdtMemberGameInfoStageController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspUdtMemberGameInfoStage", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@InsertORUpdate", SqlDbType.NVarChar, -1).Value = p.InsertORUpdate.ToUpper();
                        command.Parameters.Add("@MemberID_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.MemberID_MemberGameInfoes;
                        command.Parameters.Add("@Level_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.Level_MemberGameInfoes;
                        command.Parameters.Add("@Exps_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.Exps_MemberGameInfoes;
                        command.Parameters.Add("@Points_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.Points_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT1_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT1_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT2_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT2_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT3_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT3_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT4_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT4_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT5_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT5_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT6_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT6_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT7_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT7_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT8_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT8_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT9_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT9_MemberGameInfoes;
                        command.Parameters.Add("@UserSTAT10_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.UserSTAT10_MemberGameInfoes;
                        command.Parameters.Add("@sCol1_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol1_MemberGameInfoes;
                        command.Parameters.Add("@sCol2_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol2_MemberGameInfoes;
                        command.Parameters.Add("@sCol3_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol3_MemberGameInfoes;
                        command.Parameters.Add("@sCol4_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol4_MemberGameInfoes;
                        command.Parameters.Add("@sCol5_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol5_MemberGameInfoes;
                        command.Parameters.Add("@sCol6_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol6_MemberGameInfoes;
                        command.Parameters.Add("@sCol7_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol7_MemberGameInfoes;
                        command.Parameters.Add("@sCol8_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol8_MemberGameInfoes;
                        command.Parameters.Add("@sCol9_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol9_MemberGameInfoes;
                        command.Parameters.Add("@sCol10_MemberGameInfoes", SqlDbType.NVarChar, -1).Value = p.sCol10_MemberGameInfoes;
                        command.Parameters.Add("@MemberGameInfoStageID_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoStageID_MemberGameInfoStages;
                        command.Parameters.Add("@MemberID_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.MemberID_MemberGameInfoStages;
                        command.Parameters.Add("@StageName_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageName_MemberGameInfoStages;
                        command.Parameters.Add("@StageStatus_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStatus_MemberGameInfoStages;
                        command.Parameters.Add("@Category1_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Category1_MemberGameInfoStages;
                        command.Parameters.Add("@Category2_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Category2_MemberGameInfoStages;
                        command.Parameters.Add("@Category3_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Category3_MemberGameInfoStages;
                        command.Parameters.Add("@Mission1_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Mission1_MemberGameInfoStages;
                        command.Parameters.Add("@Mission2_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Mission2_MemberGameInfoStages;
                        command.Parameters.Add("@Mission3_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Mission3_MemberGameInfoStages;
                        command.Parameters.Add("@Mission4_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Mission4_MemberGameInfoStages;
                        command.Parameters.Add("@Mission5_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Mission5_MemberGameInfoStages;
                        command.Parameters.Add("@Points_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.Points_MemberGameInfoStages;
                        command.Parameters.Add("@StageStat1_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStat1_MemberGameInfoStages;
                        command.Parameters.Add("@StageStat2_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStat2_MemberGameInfoStages;
                        command.Parameters.Add("@StageStat3_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStat3_MemberGameInfoStages;
                        command.Parameters.Add("@StageStat4_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStat4_MemberGameInfoStages;
                        command.Parameters.Add("@StageStat5_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.StageStat5_MemberGameInfoStages;
                        command.Parameters.Add("@sCol1_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol1_MemberGameInfoStages;
                        command.Parameters.Add("@sCol2_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol2_MemberGameInfoStages;
                        command.Parameters.Add("@sCol3_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol3_MemberGameInfoStages;
                        command.Parameters.Add("@sCol4_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol4_MemberGameInfoStages;
                        command.Parameters.Add("@sCol5_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol5_MemberGameInfoStages;
                        command.Parameters.Add("@sCol6_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol6_MemberGameInfoStages;
                        command.Parameters.Add("@sCol7_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol7_MemberGameInfoStages;
                        command.Parameters.Add("@sCol8_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol8_MemberGameInfoStages;
                        command.Parameters.Add("@sCol9_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol9_MemberGameInfoStages;
                        command.Parameters.Add("@sCol10_MemberGameInfoStages", SqlDbType.NVarChar, -1).Value = p.sCol10_MemberGameInfoStages;

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
                        logMessage.memberID = p.MemberID_MemberGameInfoes;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBUdtMemberGameInfoStageController";
                        logMessage.Message = jsonParam;
                        Logging.RunLog(logMessage);

                        return result;
                    }

                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.MemberID_MemberGameInfoes;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBUdtMemberGameInfoStageController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
