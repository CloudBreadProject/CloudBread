using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;        //추가하나? 인증 없는 페이지인가?
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
    //필요한가? 인증 없는 페이지인가?
    [MobileAppController]
    public class CBUdtConfirmedEmailAddressController : ApiController
    {
        string result;

        public class InputParams { 
            public string memberID; 
            public string memberPWD;
        }

        public string Post(InputParams p)
        {
            // 메일 주소로 메일 전송 - 회원 가입 확인(메일 주소 체크) 등
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspUdtConfirmedEmailAddress", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.memberID;
                        command.Parameters.Add("@MemberPWD", SqlDbType.NVarChar, -1).Value = p.memberPWD;
                        connection.Open();
                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                result =  dreader[0].ToString();
                            }

                            dreader.Close();
                        }
                        connection.Close();

                        return result;
                    }

                }
            }

            catch (Exception ex)
            {
                //에러로그
                logMessage.memberID = p.memberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBUdtConfirmedEmailAddressController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

    }
}
