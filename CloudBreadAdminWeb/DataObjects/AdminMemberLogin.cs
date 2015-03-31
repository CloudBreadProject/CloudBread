using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using CloudBreadAdminWeb.globals;
using CloudBreadLib.BAL.Crypto;

//Creating_a_custom_user_login_form.Models
namespace CloudBreadAdminWeb.AdminMemberLogin.Models
{
    public class AdminMemberLogin
    {
        [Required]
        [Display(Name = "로그인ID")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "암호")]
        public string Password { get; set; }

        public class Model
        {
            public string AdminMemberID { get; set; }
            public string AdminGroup { get; set; }
            public string TimeZoneID { get; set; }
        }

        public List<Model> CBAdminLogin(string userName, string passWord, string ipAddress)
        {
            List<Model> result = new List<Model>();

            try
            {

                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspSelAdminLogin", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@AdminMemberID", SqlDbType.NVarChar, -1).Value = userName;
                        command.Parameters.Add("@AdminMemberPWD", SqlDbType.NVarChar, -1).Value = Crypto.SHA512Hash(passWord);
                        command.Parameters.Add("@LastIPAddress", SqlDbType.NVarChar, -1).Value = (ipAddress);
                        connection.Open();

                        using (SqlDataReader dreader = command.ExecuteReader())
                        {
                            while (dreader.Read())
                            {
                                Model workItem = new Model()
                                {

                                    AdminMemberID = dreader[0].ToString(),
                                    AdminGroup = dreader[1].ToString(),
                                    TimeZoneID = dreader[2].ToString(),
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
                throw ex;
            }

        }
    }
}
