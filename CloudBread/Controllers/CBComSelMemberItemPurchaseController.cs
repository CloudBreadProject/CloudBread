/**
* @file CBComSelMemberItemPurchaseController.cs
* @brief Get 1 member item purchase data info from MemberItemPurchase table \n
* @author Dae Woo Kim
* @param string MemberID - log purpose
* @param string MemberItemPurchaseID
* @return MemberItemPurchase table object
* @see uspComSelMemberItemPurchase SP, BehaviorID : B59
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
    public class CBComSelMemberItemPurchaseController : ApiController
    {
        
        public class InputParams {
            public string MemberID;     // log purpose
            public string MemberItemPurchaseID;
        }

        public class Model
        {
            public string MemberItemPurchaseID { get; set; }
            public string MemberID { get; set; }
            public string ItemListID { get; set; }
            public string PurchaseQuantity { get; set; }
            public string PurchasePrice { get; set; }
            public string PGinfo1 { get; set; }
            public string PGinfo2 { get; set; }
            public string PGinfo3 { get; set; }
            public string PGinfo4 { get; set; }
            public string PGinfo5 { get; set; }
            public string PurchaseDeviceID { get; set; }
            public string PurchaseDeviceIPAddress { get; set; }
            public string PurchaseDeviceMACAddress { get; set; }
            public string PurchaseDT { get; set; }
            public string PurchaseCancelYN { get; set; }
            public string PurchaseCancelDT { get; set; }
            public string PurchaseCancelingStatus { get; set; }
            public string PurchaseCancelReturnedAmount { get; set; }
            public string PurchaseCancelDeviceID { get; set; }
            public string PurchaseCancelDeviceIPAddress { get; set; }
            public string PurchaseCancelDeviceMACAddress { get; set; }
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
                    using (SqlCommand command = new SqlCommand("uspComSelMemberItemPurchase", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberItemPurchaseID", SqlDbType.NVarChar, -1).Value = p.MemberItemPurchaseID;
                        connection.Open();

                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                Model workItem = new Model()
                                {
                                    MemberItemPurchaseID = dreader[0].ToString(),
                                    MemberID = dreader[1].ToString(),
                                    ItemListID = dreader[2].ToString(),
                                    PurchaseQuantity = dreader[3].ToString(),
                                    PurchasePrice = dreader[4].ToString(),
                                    PGinfo1 = dreader[5].ToString(),
                                    PGinfo2 = dreader[6].ToString(),
                                    PGinfo3 = dreader[7].ToString(),
                                    PGinfo4 = dreader[8].ToString(),
                                    PGinfo5 = dreader[9].ToString(),
                                    PurchaseDeviceID = dreader[10].ToString(),
                                    PurchaseDeviceIPAddress = dreader[11].ToString(),
                                    PurchaseDeviceMACAddress = dreader[12].ToString(),
                                    PurchaseDT = dreader[13].ToString(),
                                    PurchaseCancelYN = dreader[14].ToString(),
                                    PurchaseCancelDT = dreader[15].ToString(),
                                    PurchaseCancelingStatus = dreader[16].ToString(),
                                    PurchaseCancelReturnedAmount = dreader[17].ToString(),
                                    PurchaseCancelDeviceID = dreader[18].ToString(),
                                    PurchaseCancelDeviceIPAddress = dreader[19].ToString(),
                                    PurchaseCancelDeviceMACAddress = dreader[20].ToString(),
                                    sCol1 = dreader[21].ToString(),
                                    sCol2 = dreader[22].ToString(),
                                    sCol3 = dreader[23].ToString(),
                                    sCol4 = dreader[24].ToString(),
                                    sCol5 = dreader[25].ToString(),
                                    sCol6 = dreader[26].ToString(),
                                    sCol7 = dreader[27].ToString(),
                                    sCol8 = dreader[28].ToString(),
                                    sCol9 = dreader[29].ToString(),
                                    sCol10 = dreader[30].ToString()

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
                logMessage.Logger = "CBComSelMemberItemPurchaseController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
