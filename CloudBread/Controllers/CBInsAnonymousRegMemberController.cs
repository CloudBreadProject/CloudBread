/**
* @file CBInsAnonymousRegMemberController.cs
* @brief First of all, you must consider using 3rd party(Facebook, Microsoft ID, Google ID and Twitter ID) or Active Directory authentication. \n
* Private anonymous member registration controller. \n
* Mobile client POST members and MemberGameInfoes object as json format. \n
* Insert on members and MemberGameInfoes table. \n
* Send memberID as guid(or 3rd party provider generated unique value) from client or unique value to fill out of info as blank values. \n
* This API is identically same with InsRegMember API.
* @author Dae Woo Kim
* @param members and MemberGameInfoes object
* @return string value "2" affected rows count
* @see uspInsAnonymousRegMember SP, BehaviorID : B03
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
    public class CBInsAnonymousRegMemberController : ApiController
    {
        
        public class InputParams
        {
            public string MembersMemberID { get; set; }
            public string MembersMemberPWD { get; set; }
            public string MembersEmailAddress { get; set; }
            public string MembersEmailConfirmedYN { get; set; }
            public string MembersPhoneNumber1 { get; set; }
            public string MembersPhoneNumber2 { get; set; }
            public string MembersPINumber { get; set; }
            public string MembersName1 { get; set; }
            public string MembersName2 { get; set; }
            public string MembersName3 { get; set; }
            public string MembersDOB { get; set; }
            public string MembersRecommenderID { get; set; }
            public string MembersMemberGroup { get; set; }
            public string MembersLastDeviceID { get; set; }
            public string MembersLastIPaddress { get; set; }
            public string MembersLastLoginDT { get; set; }
            public string MembersLastLogoutDT { get; set; }
            public string MembersLastMACAddress { get; set; }
            public string MembersAccountBlockYN { get; set; }
            public string MembersAccountBlockEndDT { get; set; }
            public string MembersAnonymousYN { get; set; }
            public string Members3rdAuthProvider { get; set; }
            public string Members3rdAuthID { get; set; }
            public string Members3rdAuthParam { get; set; }
            public string MembersPushNotificationID { get; set; }
            public string MembersPushNotificationProvider { get; set; }
            public string MembersPushNotificationGroup { get; set; }
            public string MemberssCol1 { get; set; }
            public string MemberssCol2 { get; set; }
            public string MemberssCol3 { get; set; }
            public string MemberssCol4 { get; set; }
            public string MemberssCol5 { get; set; }
            public string MemberssCol6 { get; set; }
            public string MemberssCol7 { get; set; }
            public string MemberssCol8 { get; set; }
            public string MemberssCol9 { get; set; }
            public string MemberssCol10 { get; set; }
            public string MembersTimeZoneID { get; set; }
            public string MemberGameInfoesLevel { get; set; }
            public string MemberGameInfoesExps { get; set; }
            public string MemberGameInfoesPoints { get; set; }
            public string MemberGameInfoesUserSTAT1 { get; set; }
            public string MemberGameInfoesUserSTAT2 { get; set; }
            public string MemberGameInfoesUserSTAT3 { get; set; }
            public string MemberGameInfoesUserSTAT4 { get; set; }
            public string MemberGameInfoesUserSTAT5 { get; set; }
            public string MemberGameInfoesUserSTAT6 { get; set; }
            public string MemberGameInfoesUserSTAT7 { get; set; }
            public string MemberGameInfoesUserSTAT8 { get; set; }
            public string MemberGameInfoesUserSTAT9 { get; set; }
            public string MemberGameInfoesUserSTAT10 { get; set; }
            public string MemberGameInfoessCol1 { get; set; }
            public string MemberGameInfoessCol2 { get; set; }
            public string MemberGameInfoessCol3 { get; set; }
            public string MemberGameInfoessCol4 { get; set; }
            public string MemberGameInfoessCol5 { get; set; }
            public string MemberGameInfoessCol6 { get; set; }
            public string MemberGameInfoessCol7 { get; set; }
            public string MemberGameInfoessCol8 { get; set; }
            public string MemberGameInfoessCol9 { get; set; }
            public string MemberGameInfoessCol10 { get; set; }

        }
        public string Post(InputParams p)
        {
            string result = "";
           
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(p);

            try
            {
                // task start log
                //logMessage.memberID = p.MembersMemberID;
                //logMessage.Level = "INFO";
                //logMessage.Logger = "CBInsAnonymousRegMemberController";
                //logMessage.Message = jsonParam;
                //Logging.RunLog(logMessage);

                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspInsAnonymousRegMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@MembersMemberID", SqlDbType.NVarChar, -1).Value = p.MembersMemberID;
                        command.Parameters.Add("@MembersMemberPWD", SqlDbType.NVarChar, -1).Value = p.MembersMemberPWD;
                        command.Parameters.Add("@MembersEmailAddress", SqlDbType.NVarChar, -1).Value = p.MembersEmailAddress;
                        command.Parameters.Add("@MembersEmailConfirmedYN", SqlDbType.NVarChar, -1).Value = p.MembersEmailConfirmedYN;
                        command.Parameters.Add("@MembersPhoneNumber1", SqlDbType.NVarChar, -1).Value = p.MembersPhoneNumber1;
                        command.Parameters.Add("@MembersPhoneNumber2", SqlDbType.NVarChar, -1).Value = p.MembersPhoneNumber2;
                        command.Parameters.Add("@MembersPINumber", SqlDbType.NVarChar, -1).Value = p.MembersPINumber;
                        command.Parameters.Add("@MembersName1", SqlDbType.NVarChar, -1).Value = p.MembersName1;
                        command.Parameters.Add("@MembersName2", SqlDbType.NVarChar, -1).Value = p.MembersName2;
                        command.Parameters.Add("@MembersName3", SqlDbType.NVarChar, -1).Value = p.MembersName3;
                        command.Parameters.Add("@MembersDOB", SqlDbType.NVarChar, -1).Value = p.MembersDOB;
                        command.Parameters.Add("@MembersRecommenderID", SqlDbType.NVarChar, -1).Value = p.MembersRecommenderID;
                        command.Parameters.Add("@MembersMemberGroup", SqlDbType.NVarChar, -1).Value = p.MembersMemberGroup;
                        command.Parameters.Add("@MembersLastDeviceID", SqlDbType.NVarChar, -1).Value = p.MembersLastDeviceID;
                        command.Parameters.Add("@MembersLastIPaddress", SqlDbType.NVarChar, -1).Value = p.MembersLastIPaddress;
                        command.Parameters.Add("@MembersLastLoginDT", SqlDbType.NVarChar, -1).Value = p.MembersLastLoginDT;
                        command.Parameters.Add("@MembersLastLogoutDT", SqlDbType.NVarChar, -1).Value = p.MembersLastLogoutDT;
                        command.Parameters.Add("@MembersLastMACAddress", SqlDbType.NVarChar, -1).Value = p.MembersLastMACAddress;
                        command.Parameters.Add("@MembersAccountBlockYN", SqlDbType.NVarChar, -1).Value = p.MembersAccountBlockYN;
                        command.Parameters.Add("@MembersAccountBlockEndDT", SqlDbType.NVarChar, -1).Value = p.MembersAccountBlockEndDT;
                        command.Parameters.Add("@MembersAnonymousYN", SqlDbType.NVarChar, -1).Value = p.MembersAnonymousYN;
                        command.Parameters.Add("@Members3rdAuthProvider", SqlDbType.NVarChar, -1).Value = p.Members3rdAuthProvider;
                        command.Parameters.Add("@Members3rdAuthID", SqlDbType.NVarChar, -1).Value = p.Members3rdAuthID;
                        command.Parameters.Add("@Members3rdAuthParam", SqlDbType.NVarChar, -1).Value = p.Members3rdAuthParam;
                        command.Parameters.Add("@MembersPushNotificationID", SqlDbType.NVarChar, -1).Value = p.MembersPushNotificationID;
                        command.Parameters.Add("@MembersPushNotificationProvider", SqlDbType.NVarChar, -1).Value = p.MembersPushNotificationProvider;
                        command.Parameters.Add("@MembersPushNotificationGroup", SqlDbType.NVarChar, -1).Value = p.MembersPushNotificationGroup;
                        command.Parameters.Add("@MemberssCol1", SqlDbType.NVarChar, -1).Value = p.MemberssCol1;
                        command.Parameters.Add("@MemberssCol2", SqlDbType.NVarChar, -1).Value = p.MemberssCol2;
                        command.Parameters.Add("@MemberssCol3", SqlDbType.NVarChar, -1).Value = p.MemberssCol3;
                        command.Parameters.Add("@MemberssCol4", SqlDbType.NVarChar, -1).Value = p.MemberssCol4;
                        command.Parameters.Add("@MemberssCol5", SqlDbType.NVarChar, -1).Value = p.MemberssCol5;
                        command.Parameters.Add("@MemberssCol6", SqlDbType.NVarChar, -1).Value = p.MemberssCol6;
                        command.Parameters.Add("@MemberssCol7", SqlDbType.NVarChar, -1).Value = p.MemberssCol7;
                        command.Parameters.Add("@MemberssCol8", SqlDbType.NVarChar, -1).Value = p.MemberssCol8;
                        command.Parameters.Add("@MemberssCol9", SqlDbType.NVarChar, -1).Value = p.MemberssCol9;
                        command.Parameters.Add("@MemberssCol10", SqlDbType.NVarChar, -1).Value = p.MemberssCol10;
                        command.Parameters.Add("@MembersTimeZoneID", SqlDbType.NVarChar, -1).Value = p.MembersTimeZoneID;
                        command.Parameters.Add("@MemberGameInfoesLevel", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesLevel;
                        command.Parameters.Add("@MemberGameInfoesExps", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesExps;
                        command.Parameters.Add("@MemberGameInfoesPoints", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesPoints;
                        command.Parameters.Add("@MemberGameInfoesUserSTAT1", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesUserSTAT1;
                        command.Parameters.Add("@MemberGameInfoesUserSTAT2", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesUserSTAT2;
                        command.Parameters.Add("@MemberGameInfoesUserSTAT3", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesUserSTAT3;
                        command.Parameters.Add("@MemberGameInfoesUserSTAT4", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesUserSTAT4;
                        command.Parameters.Add("@MemberGameInfoesUserSTAT5", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesUserSTAT5;
                        command.Parameters.Add("@MemberGameInfoesUserSTAT6", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesUserSTAT6;
                        command.Parameters.Add("@MemberGameInfoesUserSTAT7", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesUserSTAT7;
                        command.Parameters.Add("@MemberGameInfoesUserSTAT8", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesUserSTAT8;
                        command.Parameters.Add("@MemberGameInfoesUserSTAT9", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesUserSTAT9;
                        command.Parameters.Add("@MemberGameInfoesUserSTAT10", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoesUserSTAT10;
                        command.Parameters.Add("@MemberGameInfoessCol1", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoessCol1;
                        command.Parameters.Add("@MemberGameInfoessCol2", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoessCol2;
                        command.Parameters.Add("@MemberGameInfoessCol3", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoessCol3;
                        command.Parameters.Add("@MemberGameInfoessCol4", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoessCol4;
                        command.Parameters.Add("@MemberGameInfoessCol5", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoessCol5;
                        command.Parameters.Add("@MemberGameInfoessCol6", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoessCol6;
                        command.Parameters.Add("@MemberGameInfoessCol7", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoessCol7;
                        command.Parameters.Add("@MemberGameInfoessCol8", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoessCol8;
                        command.Parameters.Add("@MemberGameInfoessCol9", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoessCol9;
                        command.Parameters.Add("@MemberGameInfoessCol10", SqlDbType.NVarChar, -1).Value = p.MemberGameInfoessCol10;

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
                        logMessage.memberID = p.MembersMemberID;
                        logMessage.Level = "INFO";
                        logMessage.Logger = "CBInsAnonymousRegMemberController";
                        logMessage.Message = jsonParam;
                        Logging.RunLog(logMessage);

                        return result;
                    }
                }
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = p.MembersMemberID;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBInsAnonymousRegMemberController";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }
        

    }
}
