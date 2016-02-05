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
    public class CBComSelMemberItemController : ApiController
    {
        
        public class InputParams {
            public string MemberID;     // 로그 식별
            public string MemberItemID;
        }

        public class Model
        {
            public string MemberItemID { get; set; }
            public string MemberID { get; set; }
            public string ItemListID { get; set; }
            public string ItemCount { get; set; }
            public string ItemStatus { get; set; }
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
                    using (SqlCommand command = new SqlCommand("CloudBread.uspComSelMemberItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberItemID", SqlDbType.NVarChar, -1).Value = p.MemberItemID;
                        connection.Open();

                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                Model workItem = new Model()
                                {
                                    MemberItemID = dreader[0].ToString(),
                                    MemberID = dreader[1].ToString(),
                                    ItemListID = dreader[2].ToString(),
                                    ItemCount = dreader[3].ToString(),
                                    ItemStatus = dreader[4].ToString(),
                                    sCol1 = dreader[5].ToString(),
                                    sCol2 = dreader[6].ToString(),
                                    sCol3 = dreader[7].ToString(),
                                    sCol4 = dreader[8].ToString(),
                                    sCol5 = dreader[9].ToString(),
                                    sCol6 = dreader[10].ToString(),
                                    sCol7 = dreader[11].ToString(),
                                    sCol8 = dreader[12].ToString(),
                                    sCol9 = dreader[13].ToString(),
                                    sCol10 = dreader[14].ToString()
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
                logMessage.Logger = "CBComSelMemberItemController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
