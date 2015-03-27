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
using CloudBreadLib.BAL.SendSMTPMail;
using Newtonsoft.Json;

namespace CloudBread.Controllers
{
    public class CBSelSendEmailToMemberController : ApiController
    {
        string result ="";
        public ApiServices Services { get; set; }

        public class InputParams { 
            public string memberID;
        }

        public string Post(InputParams p)
        {
            // 메일 주소로 메일 전송 - 회원 가입 확인(메일 주소 체크) 등 - 관리자 또는 특수 조건 하에 회원 호출
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspSelSendEmailToMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID", SqlDbType.NVarChar, -1).Value = p.memberID;
                        connection.Open();
                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                result = dreader[0].ToString();
                            }
                            //////////////////////////////////////////////////////////////////////////////////////
                            // 메일 전송 루틴 - CloudBreadlib/BAL/SendSMTPMail 참조
                            //방화벽, 안티바이러스 등 outbound 체크
                            //SendEmail 찾아가서 인증 정보 등 변경할 것
                            //string s = SendSMTPMail.SendEmail(dreader[0].ToString(), "제목", "내용");
                            //Debug.WriteLine(s);
                            //////////////////////////////////////////////////////////////////////////////////////

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
                logMessage.Logger = "CBSelSendEmailToMemberController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw ;
            }
        }

    }
}
