/**
* @file CBComSelItemList1Controller.cs
* @brief Get 1 item data  \n
* same with "CBComSelItemList1Controller"
* @author Dae Woo Kim
* @param string MemberID - log purpose
* @param string ItemListID
* @return itemlists table object
* @see uspSelItem1 SP, BehaviorID : B26
* @todo duplicate with "CBComSelItemList1Controller"
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
    public class CBComSelItemList1Controller : ApiController
    {
        
        public class InputParams {
            public string MemberID;     // 로그 식별
            public string ItemListID;
        }

        public class Model
        {
            public string ItemListID { get; set; }
            public string ItemName { get; set; }
            public string ItemDescription { get; set; }
            public string ItemPrice { get; set; }
            public string ItemSellPrice { get; set; }
            public string ItemCategory1 { get; set; }
            public string ItemCategory2 { get; set; }
            public string ItemCategory3 { get; set; }
            public string IteamCreateAdminID { get; set; }
            public string IteamUpdateAdminID { get; set; }
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
                    using (SqlCommand command = new SqlCommand("CloudBread.uspComSelItemList1", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ItemListID", SqlDbType.NVarChar, -1).Value = p.ItemListID;
                        connection.Open();

                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                Model workItem = new Model()
                                {
                                    ItemListID = dreader[0].ToString(),
                                    ItemName = dreader[1].ToString(),
                                    ItemDescription = dreader[2].ToString(),
                                    ItemPrice = dreader[3].ToString(),
                                    ItemSellPrice = dreader[4].ToString(),
                                    ItemCategory1 = dreader[5].ToString(),
                                    ItemCategory2 = dreader[6].ToString(),
                                    ItemCategory3 = dreader[7].ToString(),
                                    IteamCreateAdminID = dreader[8].ToString(),
                                    IteamUpdateAdminID = dreader[9].ToString(),
                                    sCol1 = dreader[10].ToString(),
                                    sCol2 = dreader[11].ToString(),
                                    sCol3 = dreader[12].ToString(),
                                    sCol4 = dreader[13].ToString(),
                                    sCol5 = dreader[14].ToString(),
                                    sCol6 = dreader[15].ToString(),
                                    sCol7 = dreader[16].ToString(),
                                    sCol8 = dreader[17].ToString(),
                                    sCol9 = dreader[18].ToString(),
                                    sCol10 = dreader[19].ToString(),
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
                logMessage.Logger = "CBComSelItemList1Controller";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
