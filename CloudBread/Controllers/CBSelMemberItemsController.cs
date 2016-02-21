/**
* @file CBSelMemberItemsController.cs
* @brief Get MemberItems data by paging. \n
* To get all data without paging, set big number "pageSize" param (max 9223372036854775807)
* @author Dae Woo Kim
* @param string memberID
* @param int64 page
* @param int64 pageSize
* @return MemberItems table object
* @see uspSelMemberItems SP, BehaviorID : B20, B23
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
using Microsoft.Practices.TransientFaultHandling;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;

namespace CloudBread.Controllers
{
    [MobileAppController]
    public class CBSelMemberItemsController : ApiController
    {
        public class InputParams {
            public string MemberID;
            public Int64 Page; 
            public Int64 PageSize; 
        }

        public class Model
        {
            public string ROWNUM { get; set; }
            public string ItemListsItemName { get; set; }
            public string ItemListsItemDescription { get; set; }
            public string ItemListsItemPrice { get; set; }
            public string ItemListsItemSellPrice { get; set; }
            public string ItemListsItemCategory1 { get; set; }
            public string ItemListsItemCategory2 { get; set; }
            public string ItemListsItemCategory3 { get; set; }
            public string ItemListssCol1 { get; set; }
            public string ItemListssCol2 { get; set; }
            public string ItemListssCol3 { get; set; }
            public string ItemListssCol4 { get; set; }
            public string ItemListssCol5 { get; set; }
            public string ItemListssCol6 { get; set; }
            public string ItemListssCol7 { get; set; }
            public string ItemListssCol8 { get; set; }
            public string ItemListssCol9 { get; set; }
            public string ItemListssCol10 { get; set; }
            public string MemberItemsMemberItemID { get; set; }
            public string MemberItemsMemberID { get; set; }
            public string MemberItemsItemListID { get; set; }
            public string MemberItemsItemCount { get; set; }
            public string MemberItemsItemStatus { get; set; }
            public string MemberItemssCol1 { get; set; }
            public string MemberItemssCol2 { get; set; }
            public string MemberItemssCol3 { get; set; }
            public string MemberItemssCol4 { get; set; }
            public string MemberItemssCol5 { get; set; }
            public string MemberItemssCol6 { get; set; }
            public string MemberItemssCol7 { get; set; }
            public string MemberItemssCol8 { get; set; }
            public string MemberItemssCol9 { get; set; }
            public string MemberItemssCol10 { get; set; }

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
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspSelMemberItems", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Page", SqlDbType.BigInt).Value = p.Page;
                        command.Parameters.Add("@PageSize", SqlDbType.BigInt).Value = p.PageSize;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.MemberID;
                        connection.OpenWithRetry(retryPolicy);

                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                Model workItem = new Model()
                                {
                                    ROWNUM = dreader[0].ToString(),
                                    ItemListsItemName = dreader[1].ToString(),
                                    ItemListsItemDescription = dreader[2].ToString(),
                                    ItemListsItemPrice = dreader[3].ToString(),
                                    ItemListsItemSellPrice = dreader[4].ToString(),
                                    ItemListsItemCategory1 = dreader[5].ToString(),
                                    ItemListsItemCategory2 = dreader[6].ToString(),
                                    ItemListsItemCategory3 = dreader[7].ToString(),
                                    ItemListssCol1 = dreader[8].ToString(),
                                    ItemListssCol2 = dreader[9].ToString(),
                                    ItemListssCol3 = dreader[10].ToString(),
                                    ItemListssCol4 = dreader[11].ToString(),
                                    ItemListssCol5 = dreader[12].ToString(),
                                    ItemListssCol6 = dreader[13].ToString(),
                                    ItemListssCol7 = dreader[14].ToString(),
                                    ItemListssCol8 = dreader[15].ToString(),
                                    ItemListssCol9 = dreader[16].ToString(),
                                    ItemListssCol10 = dreader[17].ToString(),
                                    MemberItemsMemberItemID = dreader[18].ToString(),
                                    MemberItemsMemberID = dreader[19].ToString(),
                                    MemberItemsItemListID = dreader[20].ToString(),
                                    MemberItemsItemCount = dreader[21].ToString(),
                                    MemberItemsItemStatus = dreader[22].ToString(),
                                    MemberItemssCol1 = dreader[23].ToString(),
                                    MemberItemssCol2 = dreader[24].ToString(),
                                    MemberItemssCol3 = dreader[25].ToString(),
                                    MemberItemssCol4 = dreader[26].ToString(),
                                    MemberItemssCol5 = dreader[27].ToString(),
                                    MemberItemssCol6 = dreader[28].ToString(),
                                    MemberItemssCol7 = dreader[29].ToString(),
                                    MemberItemssCol8 = dreader[30].ToString(),
                                    MemberItemssCol9 = dreader[31].ToString(),
                                    MemberItemssCol10 = dreader[32].ToString()

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
                logMessage.Logger = "CBSelMemberItemsController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
