/**
* @file CBUdtSendGiftController.cs
* @brief Send item to another member. Update or delete(not actual delete - DeleteYN flag change) MemberItems, insert to GiftDepositories.  \n
* First of all, check member inventory and set first param, "DeleteORUpdate" branching memberitems
* @author Dae Woo Kim
* @param string DeleteORUpdate - branching memberitems table
* @param MemberItems table object
* @param GiftDepository table object 
* @return string "2" - affected rows
* @see uspUdtSendGift SP, BehaviorID : B36
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
    public class CBUdtSendGiftController : ApiController
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
            public string GiftDepositoryID_GiftDepository { get; set; }
            public string ItemListID_GiftDepository { get; set; }
            public string ItemCount_GiftDepository { get; set; }
            public string FromMemberID_GiftDepository { get; set; }
            public string ToMemberID_GiftDepository { get; set; }
            public string sCol1_GiftDepository { get; set; }
            public string sCol2_GiftDepository { get; set; }
            public string sCol3_GiftDepository { get; set; }
            public string sCol4_GiftDepository { get; set; }
            public string sCol5_GiftDepository { get; set; }
            public string sCol6_GiftDepository { get; set; }
            public string sCol7_GiftDepository { get; set; }
            public string sCol8_GiftDepository { get; set; }
            public string sCol9_GiftDepository { get; set; }
            public string sCol10_GiftDepository { get; set; }


        }

        public string Post(InputParams p)
        {
            string result = "";

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                // task start log
                //logMessage.memberID = p.MemberID_MemberItem;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBUdtSendGiftController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspUdtSendGift", connection))
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
                        command.Parameters.Add("@GiftDepositoryID_GiftDepository", SqlDbType.NVarChar, -1).Value = p.GiftDepositoryID_GiftDepository;
                        command.Parameters.Add("@ItemListID_GiftDepository", SqlDbType.NVarChar, -1).Value = p.ItemListID_GiftDepository;
                        command.Parameters.Add("@ItemCount_GiftDepository", SqlDbType.NVarChar, -1).Value = p.ItemCount_GiftDepository;
                        command.Parameters.Add("@FromMemberID_GiftDepository", SqlDbType.NVarChar, -1).Value = p.FromMemberID_GiftDepository;
                        command.Parameters.Add("@ToMemberID_GiftDepository", SqlDbType.NVarChar, -1).Value = p.ToMemberID_GiftDepository;
                        command.Parameters.Add("@sCol1_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol1_GiftDepository;
                        command.Parameters.Add("@sCol2_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol2_GiftDepository;
                        command.Parameters.Add("@sCol3_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol3_GiftDepository;
                        command.Parameters.Add("@sCol4_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol4_GiftDepository;
                        command.Parameters.Add("@sCol5_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol5_GiftDepository;
                        command.Parameters.Add("@sCol6_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol6_GiftDepository;
                        command.Parameters.Add("@sCol7_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol7_GiftDepository;
                        command.Parameters.Add("@sCol8_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol8_GiftDepository;
                        command.Parameters.Add("@sCol9_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol9_GiftDepository;
                        command.Parameters.Add("@sCol10_GiftDepository", SqlDbType.NVarChar, -1).Value = p.sCol10_GiftDepository;


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
                        logMessage.memberID = p.MemberID_MemberItem;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBUdtSendGiftController";
                        logMessage.Message = jsonParam;
                        Logging.RunLog(logMessage);

                        return result;
                    }

                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.MemberID_MemberItem;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBUdtSendGiftController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
