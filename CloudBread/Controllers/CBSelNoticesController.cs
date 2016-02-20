/**
* @file CBSelNoticesController.cs
* @brief Get notices by between "NoticeDurationFrom" and "NoticeDurationTo" rule \n
* after member login, show this notice data for members
* @author Dae Woo Kim
* @param string memberID
* @return Notices table object
* @see uspSelNotices SP, BehaviorID : B11, B65
* @todo paging, filter by (NoticeCategory1, NoticeCategory2, NoticeCategory3, TargetGroup, TargetOS, TargetDevice) option support
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
    public class CBSelNoticesController : ApiController
    {
        
        public class InputParams { 
            public string MemberID;     // log tasking purpose 
        }

        public class Model
        {
            public string NoticeID { get; set; }
            public string NoticeCategory1 { get; set; }
            public string NoticeCategory2 { get; set; }
            public string NoticeCategory3 { get; set; }
            public string TargetGroup { get; set; }
            public string TargetOS { get; set; }
            public string TargetDevice { get; set; }
            public string NoticeImageLink { get; set; }
            public string title { get; set; }
            public string content { get; set; }
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
            //string sid = "";
            // Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            //claimsPrincipal.GetType
            //string sid = claimsPrincipal == null ? p.MemberID : CBAuth.getSID(claimsPrincipal);
            string sid = CBAuth.getMemberID(p.MemberID, claimsPrincipal);
            p.MemberID = sid;

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            List<Model> result = new List<Model>();

            try
            {
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspSelNotices", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                Model workItem = new Model()
                                {
                                    NoticeID = dreader[0].ToString(),
                                    NoticeCategory1 = dreader[1].ToString(),
                                    NoticeCategory2 = dreader[2].ToString(),
                                    NoticeCategory3 = dreader[3].ToString(),
                                    TargetGroup = dreader[4].ToString(),
                                    TargetOS = dreader[5].ToString(),
                                    TargetDevice = dreader[6].ToString(),
                                    NoticeImageLink = dreader[7].ToString(),
                                    title = dreader[8].ToString(),
                                    content = dreader[9].ToString(),
                                    sCol1 = dreader[10].ToString(),
                                    sCol2 = dreader[11].ToString(),
                                    sCol3 = dreader[12].ToString(),
                                    sCol4 = dreader[13].ToString(),
                                    sCol5 = dreader[14].ToString(),
                                    sCol6 = dreader[15].ToString(),
                                    sCol7 = dreader[16].ToString(),
                                    sCol8 = dreader[17].ToString(),
                                    sCol9 = dreader[18].ToString(),
                                    sCol10 = dreader[19].ToString()
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
                logMessage.Logger = "CBSelNoticesController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
