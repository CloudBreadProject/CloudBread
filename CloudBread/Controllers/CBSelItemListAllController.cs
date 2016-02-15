/**
* @file CBSelItemListAllController.cs
* @brief Get ItemLists table data by paging. \n
* To get all data without paging, set big number "pageSize" param (max 9223372036854775807).
* @author Dae Woo Kim
* @param string memberID - log purpose
* @param int64 page
* @param int64 pageSize
* @return itemlists table object
* @see uspSelItemListAll SP, BehaviorID : B19
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
    public class CBSelItemListAllController : ApiController
    {
        
        public class InputParams {
            public string MemberID;     // log purpose
            public Int64 Page; 
            public Int64 PageSize;}

        public class Model
        {
            public string ROWNUM { get; set; }
            public string ItemListID { get; set; }
            public string ItemName { get; set; }
            public string ItemDescription { get; set; }
            public string ItemPrice { get; set; }
            public string ItemSellPrice { get; set; }
            public string ItemCategory1 { get; set; }
            public string ItemCategory2 { get; set; }
            public string ItemCategory3 { get; set; }
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
                    using (SqlCommand command = new SqlCommand("CloudBread.uspSelItemListAll", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Page", SqlDbType.BigInt).Value = p.Page;
                        command.Parameters.Add("@PageSize", SqlDbType.BigInt).Value = p.PageSize;
                        connection.Open();

                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                Model workItem = new Model()
                                {
                                    ROWNUM = dreader[0].ToString(),
                                    ItemListID = dreader[1].ToString(),
                                    ItemName = dreader[2].ToString(),
                                    ItemDescription = dreader[3].ToString(),
                                    ItemPrice = dreader[4].ToString(),
                                    ItemSellPrice = dreader[5].ToString(),
                                    ItemCategory1 = dreader[6].ToString(),
                                    ItemCategory2 = dreader[7].ToString(),
                                    ItemCategory3 = dreader[8].ToString(),
                                    sCol1 = dreader[9].ToString(),
                                    sCol2 = dreader[10].ToString(),
                                    sCol3 = dreader[11].ToString(),
                                    sCol4 = dreader[12].ToString(),
                                    sCol5 = dreader[13].ToString(),
                                    sCol6 = dreader[14].ToString(),
                                    sCol7 = dreader[15].ToString(),
                                    sCol8 = dreader[16].ToString(),
                                    sCol9 = dreader[17].ToString(),
                                    sCol10 = dreader[18].ToString()
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
                logMessage.Logger = "CBSelItemListAllController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw ex;
            }
        }

    }
}
