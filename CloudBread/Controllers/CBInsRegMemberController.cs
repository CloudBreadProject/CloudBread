/**
* @file CBInsRegMemberController.cs
* @brief private member registration controller. \n
* mobile client POST members and MemberGameInfoes object as json format \n
* insert on members and MemberGameInfoes table \n
* use mobile app autehtication provider such like 'facebook id' or something \n
* @author Dae Woo Kim
* @param members and MemberGameInfoes object
* @return string value "2" affected rows count
* @see uspInsRegMember SP, BehaviorID : B02
* @todo change return value to inserted data as json
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
    public class CBInsRegMemberController : ApiController
    {
        
        public class InputParams
        {
            public string 	MemberID_Members	 { get; set; }   
            public string 	MemberPWD_Members	 { get; set; }  
            public string 	EmailAddress_Members	 { get; set; } 
            public string 	EmailConfirmedYN_Members	 { get; set; }
            public string 	PhoneNumber1_Members	 { get; set; }
            public string 	PhoneNumber2_Members	 { get; set; }
            public string 	PINumber_Members	 { get; set; }
            public string 	Name1_Members	 { get; set; }
            public string 	Name2_Members	 { get; set; }
            public string 	Name3_Members	 { get; set; }
            public string 	DOB_Members	 { get; set; }
            public string 	RecommenderID_Members	 { get; set; }
            public string 	MemberGroup_Members	 { get; set; }
            public string 	LastDeviceID_Members	 { get; set; }
            public string 	LastIPaddress_Members	 { get; set; }
            public string 	LastLoginDT_Members	 { get; set; }
            public string 	LastLogoutDT_Members	 { get; set; }
            public string 	LastMACAddress_Members	 { get; set; }
            public string 	AccountBlockYN_Members	 { get; set; }
            public string 	AccountBlockEndDT_Members	 { get; set; }
            public string 	AnonymousYN_Members	 { get; set; }
            public string 	_3rdAuthProvider_Members	 { get; set; }
            public string 	_3rdAuthID_Members	 { get; set; }
            public string 	_3rdAuthParam_Members	 { get; set; }
            public string 	PushNotificationID_Members	 { get; set; }
            public string 	PushNotificationProvider_Members	 { get; set; }
            public string 	PushNotificationGroup_Members	 { get; set; }
            public string 	sCol1_Members	 { get; set; }
            public string 	sCol2_Members	 { get; set; }
            public string 	sCol3_Members	 { get; set; }
            public string 	sCol4_Members	 { get; set; }
            public string 	sCol5_Members	 { get; set; }
            public string 	sCol6_Members	 { get; set; }
            public string 	sCol7_Members	 { get; set; }
            public string 	sCol8_Members	 { get; set; }
            public string 	sCol9_Members	 { get; set; }
            public string 	sCol10_Members	 { get; set; }
            public string 	TimeZoneID_Members	 { get; set; }
            public string 	Level_MemberGameInfoes	 { get; set; }
            public string 	Exps_MemberGameInfoes	 { get; set; }
            public string 	Points_MemberGameInfoes	 { get; set; }
            public string 	UserSTAT1_MemberGameInfoes	 { get; set; }
            public string 	UserSTAT2_MemberGameInfoes	 { get; set; }
            public string 	UserSTAT3_MemberGameInfoes	 { get; set; }
            public string 	UserSTAT4_MemberGameInfoes	 { get; set; }
            public string 	UserSTAT5_MemberGameInfoes	 { get; set; }
            public string 	UserSTAT6_MemberGameInfoes	 { get; set; }
            public string 	UserSTAT7_MemberGameInfoes	 { get; set; }
            public string 	UserSTAT8_MemberGameInfoes	 { get; set; }
            public string 	UserSTAT9_MemberGameInfoes	 { get; set; }
            public string 	UserSTAT10_MemberGameInfoes	 { get; set; }
            public string 	sCol1_MemberGameInfoes	 { get; set; }
            public string 	sCol2_MemberGameInfoes	 { get; set; }
            public string 	sCol3_MemberGameInfoes	 { get; set; }
            public string 	sCol4_MemberGameInfoes	 { get; set; }
            public string 	sCol5_MemberGameInfoes	 { get; set; }
            public string 	sCol6_MemberGameInfoes	 { get; set; }
            public string 	sCol7_MemberGameInfoes	 { get; set; }
            public string 	sCol8_MemberGameInfoes	 { get; set; }
            public string 	sCol9_MemberGameInfoes	 { get; set; }
            public string 	sCol10_MemberGameInfoes	 { get; set; }

        }

        public string Post(InputParams p)
        {
            string result = "";

            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                // task start log
                //logMessage.memberID = p.MemberID_Members;
                //logMessage.Level = "INFO"; 
                //logMessage.Logger = "CBuspInsRegMemberCheckController"; 
                //logMessage.Message = jsonParam; 
                //Logging.RunLog(logMessage);

                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspInsRegMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MemberID_Members", SqlDbType.NVarChar, -1).Value = p.MemberID_Members;
                        command.Parameters.Add("@MemberPWD_Members", SqlDbType.NVarChar, -1).Value = p.MemberPWD_Members;
                        command.Parameters.Add("@EmailAddress_Members", SqlDbType.NVarChar, -1).Value = p.EmailAddress_Members;
                        command.Parameters.Add("@EmailConfirmedYN_Members", SqlDbType.NVarChar, -1).Value = p.EmailConfirmedYN_Members;
                        command.Parameters.Add("@PhoneNumber1_Members", SqlDbType.NVarChar, -1).Value = p.PhoneNumber1_Members;
                        command.Parameters.Add("@PhoneNumber2_Members", SqlDbType.NVarChar, -1).Value = p.PhoneNumber2_Members;
                        command.Parameters.Add("@PINumber_Members", SqlDbType.NVarChar, -1).Value = p.PINumber_Members;
                        command.Parameters.Add("@Name1_Members", SqlDbType.NVarChar, -1).Value = p.Name1_Members;
                        command.Parameters.Add("@Name2_Members", SqlDbType.NVarChar, -1).Value = p.Name2_Members;
                        command.Parameters.Add("@Name3_Members", SqlDbType.NVarChar, -1).Value = p.Name3_Members;
                        command.Parameters.Add("@DOB_Members", SqlDbType.NVarChar, -1).Value = p.DOB_Members;
                        command.Parameters.Add("@RecommenderID_Members", SqlDbType.NVarChar, -1).Value = p.RecommenderID_Members;
                        command.Parameters.Add("@MemberGroup_Members", SqlDbType.NVarChar, -1).Value = p.MemberGroup_Members;
                        command.Parameters.Add("@LastDeviceID_Members", SqlDbType.NVarChar, -1).Value = p.LastDeviceID_Members;
                        command.Parameters.Add("@LastIPaddress_Members", SqlDbType.NVarChar, -1).Value = p.LastIPaddress_Members;
                        command.Parameters.Add("@LastLoginDT_Members", SqlDbType.NVarChar, -1).Value = p.LastLoginDT_Members;
                        command.Parameters.Add("@LastLogoutDT_Members", SqlDbType.NVarChar, -1).Value = p.LastLogoutDT_Members;
                        command.Parameters.Add("@LastMACAddress_Members", SqlDbType.NVarChar, -1).Value = p.LastMACAddress_Members;
                        command.Parameters.Add("@AccountBlockYN_Members", SqlDbType.NVarChar, -1).Value = p.AccountBlockYN_Members;
                        command.Parameters.Add("@AccountBlockEndDT_Members", SqlDbType.NVarChar, -1).Value = p.AccountBlockEndDT_Members;
                        command.Parameters.Add("@AnonymousYN_Members", SqlDbType.NVarChar, -1).Value = p.AnonymousYN_Members;
                        command.Parameters.Add("@3rdAuthProvider_Members", SqlDbType.NVarChar, -1).Value = p._3rdAuthProvider_Members;
                        command.Parameters.Add("@3rdAuthID_Members", SqlDbType.NVarChar, -1).Value = p._3rdAuthID_Members;
                        command.Parameters.Add("@3rdAuthParam_Members", SqlDbType.NVarChar, -1).Value = p._3rdAuthParam_Members;
                        command.Parameters.Add("@PushNotificationID_Members", SqlDbType.NVarChar, -1).Value = p.PushNotificationID_Members;
                        command.Parameters.Add("@PushNotificationProvider_Members", SqlDbType.NVarChar, -1).Value = p.PushNotificationProvider_Members;
                        command.Parameters.Add("@PushNotificationGroup_Members", SqlDbType.NVarChar, -1).Value = p.PushNotificationGroup_Members;
                        command.Parameters.Add("@sCol1_Members", SqlDbType.NVarChar, -1).Value = p.sCol1_Members;
                        command.Parameters.Add("@sCol2_Members", SqlDbType.NVarChar, -1).Value = p.sCol2_Members;
                        command.Parameters.Add("@sCol3_Members", SqlDbType.NVarChar, -1).Value = p.sCol3_Members;
                        command.Parameters.Add("@sCol4_Members", SqlDbType.NVarChar, -1).Value = p.sCol4_Members;
                        command.Parameters.Add("@sCol5_Members", SqlDbType.NVarChar, -1).Value = p.sCol5_Members;
                        command.Parameters.Add("@sCol6_Members", SqlDbType.NVarChar, -1).Value = p.sCol6_Members;
                        command.Parameters.Add("@sCol7_Members", SqlDbType.NVarChar, -1).Value = p.sCol7_Members;
                        command.Parameters.Add("@sCol8_Members", SqlDbType.NVarChar, -1).Value = p.sCol8_Members;
                        command.Parameters.Add("@sCol9_Members", SqlDbType.NVarChar, -1).Value = p.sCol9_Members;
                        command.Parameters.Add("@sCol10_Members", SqlDbType.NVarChar, -1).Value = p.sCol10_Members;
                        command.Parameters.Add("@TimeZoneID_Members", SqlDbType.NVarChar, -1).Value = p.TimeZoneID_Members;
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

                        // task end log
                        logMessage.memberID = p.MemberID_Members;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBuspInsRegMemberCheckController";
                        logMessage.Message = jsonParam;
                        Logging.RunLog(logMessage);

                        return result;
                    }
                    
                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.MemberID_Members;
                logMessage.Level = "ERROR"; 
                logMessage.Logger = "CBuspInsRegMemberCheckController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }
    }
}
