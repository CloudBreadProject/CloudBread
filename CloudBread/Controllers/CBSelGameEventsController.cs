/**
* @file CBSelGameEventsController.cs
* @brief Get remained game events API for memberID. \n
* Date between "GameEvents.EventDurationFrom" and "GameEvents.EventDurationTo" rule. \n
* After finish the game event duration, member could not join the finished event. \n
* @author Dae Woo Kim
* @param string memberID
* @return GameEvents list table object
* @see uspSelGameEvents SP, BehaviorID : B12, B67
* @todo paging, filter by (EventCategory1, EventCategory2, EventCategory3, TargetGroup, TargetOS, TargetDevice) option support
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
    public class CBSelGameEventsController : ApiController
    {
        
        public class InputParams { public string MemberID; }

        public class Model
        {
            public string GameEventID { get; set; }
            public string eventCategory1 { get; set; }
            public string eventCategory2 { get; set; }
            public string eventCategory3 { get; set; }
            public string ItemListID { get; set; }
            public string ItemCount { get; set; }
            public string Itemstatus { get; set; }
            public string TargetGroup { get; set; }
            public string TargetOS { get; set; }
            public string TargetDevice { get; set; }
            public string EventImageLink { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
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
            // Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = CBAuth.getMemberID(p.MemberID, claimsPrincipal);
            p.MemberID = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            List<Model> result = new List<Model>();

            try
            {

                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspSelGameEvents", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.MemberID;
                        connection.Open();

                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                Model workItem = new Model()
                                {
                                    GameEventID = dreader[0].ToString(),
                                    eventCategory1 = dreader[1].ToString(),
                                    eventCategory2 = dreader[2].ToString(),
                                    eventCategory3 = dreader[3].ToString(),
                                    ItemListID = dreader[4].ToString(),
                                    ItemCount = dreader[5].ToString(),
                                    Itemstatus = dreader[6].ToString(),
                                    TargetGroup = dreader[7].ToString(),
                                    TargetOS = dreader[8].ToString(),
                                    TargetDevice = dreader[9].ToString(),
                                    EventImageLink = dreader[10].ToString(),
                                    Title = dreader[11].ToString(),
                                    Content = dreader[12].ToString(),
                                    sCol1 = dreader[13].ToString(),
                                    sCol2 = dreader[14].ToString(),
                                    sCol3 = dreader[15].ToString(),
                                    sCol4 = dreader[16].ToString(),
                                    sCol5 = dreader[17].ToString(),
                                    sCol6 = dreader[18].ToString(),
                                    sCol7 = dreader[19].ToString(),
                                    sCol8 = dreader[20].ToString(),
                                    sCol9 = dreader[21].ToString(),
                                    sCol10 = dreader[22].ToString()
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
                logMessage.Logger = "CBSelGameEventsController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
