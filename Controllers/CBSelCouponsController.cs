/**
* @file CBSelCouponsController.cs
* @brief Get coupon list API of memberID  \n
*************************************************************************** 
* This API is deplicated by business logic bug in CloudBread 2.0.1 release.
****************************************************************************
* @author Dae Woo Kim
* @param string memberID 
* @return coupons table object
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
    public class CBSelCouponsController : ApiController
    {
        
        public class InputParams { public string MemberID; }

        public class Model
        {
            public string CouponID { get; set; }
            public string CouponCategory1 { get; set; }
            public string CouponCategory2 { get; set; }
            public string CouponCategory3 { get; set; }
            public string ItemListID { get; set; }
            public string ItemCount { get; set; }
            public string ItemStatus { get; set; }
            public string TargetGroup { get; set; }
            public string TargetOS { get; set; }
            public string TargetDevice { get; set; }
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
                /// Database connection retry policy
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(globalVal.conRetryCount, TimeSpan.FromSeconds(globalVal.conRetryFromSeconds));
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("uspSelCoupons", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.MemberID;
                        connection.OpenWithRetry(retryPolicy);

                        using (SqlDataReader dreader = command.ExecuteReaderWithRetry(retryPolicy))
                        {
                            while (dreader.Read())
                            {
                                Model workItem = new Model()
                                {
                                    CouponID = dreader[0].ToString(),
                                    CouponCategory1 = dreader[1].ToString(),
                                    CouponCategory2 = dreader[2].ToString(),
                                    CouponCategory3 = dreader[3].ToString(),
                                    ItemListID = dreader[4].ToString(),
                                    ItemCount = dreader[5].ToString(),
                                    ItemStatus = dreader[6].ToString(),
                                    TargetGroup = dreader[7].ToString(),
                                    TargetOS = dreader[8].ToString(),
                                    TargetDevice = dreader[9].ToString(),
                                    Title = dreader[10].ToString(),
                                    Content = dreader[11].ToString(),
                                    sCol1 = dreader[12].ToString(),
                                    sCol2 = dreader[13].ToString(),
                                    sCol3 = dreader[14].ToString(),
                                    sCol4 = dreader[15].ToString(),
                                    sCol5 = dreader[16].ToString(),
                                    sCol6 = dreader[17].ToString(),
                                    sCol7 = dreader[18].ToString(),
                                    sCol8 = dreader[19].ToString(),
                                    sCol9 = dreader[20].ToString(),
                                    sCol10 = dreader[21].ToString()

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
                logMessage.Logger = "CBSelCouponsController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
