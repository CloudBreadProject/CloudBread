using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;

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
    public class CBSelCouponsController : ApiController
    {
        public ApiServices Services { get; set; }
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
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            List<Model> result = new List<Model>();

            try
            {
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspSelCoupons", connection))
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
                                    CouponID = dreader[0].ToString(),
                                    CouponCategory1 = dreader[1].ToString(),
                                    ItemListID = dreader[2].ToString(),
                                    ItemCount = dreader[3].ToString(),
                                    ItemStatus = dreader[4].ToString(),
                                    TargetGroup = dreader[5].ToString(),
                                    TargetOS = dreader[6].ToString(),
                                    TargetDevice = dreader[7].ToString(),
                                    Title = dreader[8].ToString(),
                                    Content = dreader[9].ToString(),
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
                //에러로그
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
