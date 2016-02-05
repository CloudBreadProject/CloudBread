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
    public class CBComSelMemberController : ApiController
    {
        
        public class InputParams { 
            public string memberID;
        }

        public class Model
        {
            public string MemberID { get; set; }
            public string MemberPWD { get; set; }
            public string EmailAddress { get; set; }
            public string EmailConfirmedYN { get; set; }
            public string PhoneNumber1 { get; set; }
            public string PhoneNumber2 { get; set; }
            public string PINumber { get; set; }
            public string Name1 { get; set; }
            public string Name2 { get; set; }
            public string Name3 { get; set; }
            public string DOB { get; set; }
            public string RecommenderID { get; set; }
            public string MemberGroup { get; set; }
            public string LastDeviceID { get; set; }
            public string LastIPaddress { get; set; }
            public string LastLoginDT { get; set; }
            public string LastLogoutDT { get; set; }
            public string LastMACAddress { get; set; }
            public string AccountBlockYN { get; set; }
            public string AccountBlockEndDT { get; set; }
            public string AnonymousYN { get; set; }
            public string _3rdAuthProvider { get; set; }
            public string _3rdAuthID { get; set; }
            public string _3rdAuthParam { get; set; }
            public string PushNotificationID { get; set; }
            public string PushNotificationProvider { get; set; }
            public string PushNotificationGroup { get; set; }
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
                    using (SqlCommand command = new SqlCommand("CloudBread.uspComSelMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.memberID;
                        connection.Open();

                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                Model workItem = new Model()
                                {
                                    MemberID = dreader[0].ToString(),
                                    MemberPWD = dreader[1].ToString(),
                                    EmailAddress = dreader[2].ToString(),
                                    EmailConfirmedYN = dreader[3].ToString(),
                                    PhoneNumber1 = dreader[4].ToString(),
                                    PhoneNumber2 = dreader[5].ToString(),
                                    PINumber = dreader[6].ToString(),
                                    Name1 = dreader[7].ToString(),
                                    Name2 = dreader[8].ToString(),
                                    Name3 = dreader[9].ToString(),
                                    DOB = dreader[10].ToString(),
                                    RecommenderID = dreader[11].ToString(),
                                    MemberGroup = dreader[12].ToString(),
                                    LastDeviceID = dreader[13].ToString(),
                                    LastIPaddress = dreader[14].ToString(),
                                    LastLoginDT = dreader[15].ToString(),
                                    LastLogoutDT = dreader[16].ToString(),
                                    LastMACAddress = dreader[17].ToString(),

                                    AccountBlockYN = dreader[18].ToString(),
                                    AccountBlockEndDT = dreader[19].ToString(),
                                    AnonymousYN = dreader[20].ToString(),

                                    _3rdAuthProvider = dreader[21].ToString(),
                                    _3rdAuthID = dreader[22].ToString(),
                                    _3rdAuthParam = dreader[23].ToString(),
                                    PushNotificationID = dreader[24].ToString(),
                                    PushNotificationProvider = dreader[25].ToString(),
                                    PushNotificationGroup = dreader[26].ToString(),

                                    sCol1 = dreader[27].ToString(),
                                    sCol2 = dreader[28].ToString(),
                                    sCol3 = dreader[29].ToString(),
                                    sCol4 = dreader[30].ToString(),
                                    sCol5 = dreader[31].ToString(),
                                    sCol6 = dreader[32].ToString(),
                                    sCol7 = dreader[33].ToString(),
                                    sCol8 = dreader[34].ToString(),
                                    sCol9 = dreader[35].ToString(),
                                    sCol10 = dreader[36].ToString()
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
                logMessage.memberID = p.memberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBComSelMemberController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
