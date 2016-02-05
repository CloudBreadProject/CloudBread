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
    public class CBComSelMemberGameInfoesController : ApiController
    {
        
        public class InputParams { public string MemberID;}

        public class Model
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

        public List<Model> Post(InputParams p)
        {
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            List<Model> result = new List<Model>();

            try
            {

                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspComSelMemberGameInfoes", connection))
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
                                    MemberID = dreader[0].ToString(),
                                    Level = dreader[1].ToString(),
                                    Exps = dreader[2].ToString(),
                                    Points = dreader[3].ToString(),
                                    UserSTAT1 = dreader[4].ToString(),
                                    UserSTAT2 = dreader[5].ToString(),
                                    UserSTAT3 = dreader[6].ToString(),
                                    UserSTAT4 = dreader[7].ToString(),
                                    UserSTAT5 = dreader[8].ToString(),
                                    UserSTAT6 = dreader[9].ToString(),
                                    UserSTAT7 = dreader[10].ToString(),
                                    UserSTAT8 = dreader[11].ToString(),
                                    UserSTAT9 = dreader[12].ToString(),
                                    UserSTAT10 = dreader[13].ToString(),
                                    sCol1 = dreader[14].ToString(),
                                    sCol2 = dreader[15].ToString(),
                                    sCol3 = dreader[16].ToString(),
                                    sCol4 = dreader[17].ToString(),
                                    sCol5 = dreader[18].ToString(),
                                    sCol6 = dreader[19].ToString(),
                                    sCol7 = dreader[20].ToString(),
                                    sCol8 = dreader[21].ToString(),
                                    sCol9 = dreader[22].ToString(),
                                    sCol10 = dreader[23].ToString()

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
                //에러로그
                logMessage.memberID = p.MemberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBComSelMemberGameInfoesController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
