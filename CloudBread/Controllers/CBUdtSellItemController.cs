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
    public class CBUdtSellItemController : ApiController
    {
        
        public class InputParams
        {
            public string DeleteORUpdate { get; set; }
            public string MemberItemID_MemberItem { get; set; }
            public string MemberID_MemberItem { get; set; }
            public string ItemListID_MemberItem { get; set; }
            public string ItemCount_MemberItem { get; set; }
            public string ItemStatus_MemberItem { get; set; }
            public string sCol1_MemberItem { get; set; }
            public string sCol2_MemberItem { get; set; }
            public string sCol3_MemberItem { get; set; }
            public string sCol4_MemberItem { get; set; }
            public string sCol5_MemberItem { get; set; }
            public string sCol6_MemberItem { get; set; }
            public string sCol7_MemberItem { get; set; }
            public string sCol8_MemberItem { get; set; }
            public string sCol9_MemberItem { get; set; }
            public string sCol10_MemberItem { get; set; }
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

        }

        public string Post(InputParams p)
        {
            string result = "";
            ////////////////////////////////////////////////////////////////////////
            //보유 아이템 판매 컨트롤러 시작
            ////////////////////////////////////////////////////////////////////////
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                // 진입로그
                //logMessage.memberID = p.MemberID_MemberItem;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBUdtSellItemController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspUdtSellItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@DeleteORUpdate", SqlDbType.NVarChar, -1).Value = p.DeleteORUpdate.ToUpper();
                        command.Parameters.Add("@MemberItemID_MemberItem", SqlDbType.NVarChar, -1).Value = p.MemberItemID_MemberItem;
                        command.Parameters.Add("@MemberID_MemberItem", SqlDbType.NVarChar, -1).Value = p.MemberID_MemberItem;
                        command.Parameters.Add("@ItemListID_MemberItem", SqlDbType.NVarChar, -1).Value = p.ItemListID_MemberItem;
                        command.Parameters.Add("@ItemCount_MemberItem", SqlDbType.NVarChar, -1).Value = p.ItemCount_MemberItem;
                        command.Parameters.Add("@ItemStatus_MemberItem", SqlDbType.NVarChar, -1).Value = p.ItemStatus_MemberItem;
                        command.Parameters.Add("@sCol1_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol1_MemberItem;
                        command.Parameters.Add("@sCol2_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol2_MemberItem;
                        command.Parameters.Add("@sCol3_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol3_MemberItem;
                        command.Parameters.Add("@sCol4_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol4_MemberItem;
                        command.Parameters.Add("@sCol5_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol5_MemberItem;
                        command.Parameters.Add("@sCol6_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol6_MemberItem;
                        command.Parameters.Add("@sCol7_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol7_MemberItem;
                        command.Parameters.Add("@sCol8_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol8_MemberItem;
                        command.Parameters.Add("@sCol9_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol9_MemberItem;
                        command.Parameters.Add("@sCol10_MemberItem", SqlDbType.NVarChar, -1).Value = p.sCol10_MemberItem;
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

                        //완료 로그
                        logMessage.memberID = p.MemberID_MemberItem;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBUdtSellItemController";
                        logMessage.Message = jsonParam;
                        Logging.RunLog(logMessage);

                        return result;
                    }

                }
            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = p.MemberID_MemberItem;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBUdtSellItemController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
