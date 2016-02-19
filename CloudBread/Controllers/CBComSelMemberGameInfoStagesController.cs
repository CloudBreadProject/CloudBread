/**
* @file ComSelMemberGameInfoStagesController.cs
* @brief Common API for get member game stage for memberID - return MemberGameInfoStages info. \n
* @author Dae Woo Kim
* @param string memberID - log purpose
* @param string MemberGameInfoStageID 
* @return MemberGameInfoStages table object
* @see uspComSelMemberGameInfoStages SP, BehaviorID : B63
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
    [MobileAppController]
    public class CBComSelMemberGameInfoStagesController : ApiController
    {

        public class InputParams {
            public string MemberID { get; set; }    // log purpose
            public string MemberGameInfoStageID { get; set; }
        }

        public class Model
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

        public List<Model> Post(InputParams p)
        {
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            List<Model> result = new List<Model>();

            try
            {

                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspComSelMemberGameInfoStages", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberGameInfoStageID", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoStageID;
                        connection.Open();

                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                Model workItem = new Model()
                                {
                                    MemberGameInfoStageID = dreader[0].ToString(),
                                    MemberID = dreader[1].ToString(),
                                    StageName = dreader[2].ToString(),
                                    StageStatus = dreader[3].ToString(),
                                    Category1 = dreader[4].ToString(),
                                    Category2 = dreader[5].ToString(),
                                    Category3 = dreader[6].ToString(),
                                    Mission1 = dreader[7].ToString(),
                                    Mission2 = dreader[8].ToString(),
                                    Mission3 = dreader[9].ToString(),
                                    Mission4 = dreader[10].ToString(),
                                    Mission5 = dreader[11].ToString(),
                                    Points = dreader[12].ToString(),
                                    StageStat1 = dreader[13].ToString(),
                                    StageStat2 = dreader[14].ToString(),
                                    StageStat3 = dreader[15].ToString(),
                                    StageStat4 = dreader[16].ToString(),
                                    StageStat5 = dreader[17].ToString(),
                                    sCol1 = dreader[18].ToString(),
                                    sCol2 = dreader[19].ToString(),
                                    sCol3 = dreader[20].ToString(),
                                    sCol4 = dreader[21].ToString(),
                                    sCol5 = dreader[22].ToString(),
                                    sCol6 = dreader[23].ToString(),
                                    sCol7 = dreader[24].ToString(),
                                    sCol8 = dreader[25].ToString(),
                                    sCol9 = dreader[26].ToString(),
                                    sCol10 = dreader[27].ToString()
                                };
                                result.Add(workItem);
                            }
                            dreader.Close();
                        }
                        connection.Close();
                    }
                    return result;
                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.MemberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "ComSelMemberGameInfoStagesController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
