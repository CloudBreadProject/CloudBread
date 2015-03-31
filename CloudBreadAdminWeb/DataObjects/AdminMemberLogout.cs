using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using CloudBreadAdminWeb.globals;
using CloudBreadLib.BAL.Crypto;

namespace CloudBreadAdminWeb.AdminMemberLogout.Models
{
    public class AdminMemberLogout
    {
        [Required]
        public string AdminMemberID { get; set; }

        public class Model
        {
            public string AdminMemberID { get; set; }
        }

        public string CBAdminLogout(string adminMemberID)
        {

            string result = "";

            try
            {

                using (SqlConnection connection = new SqlConnection(globalVal.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CloudBread.uspSelAdminLogout", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@AdminMemberID", SqlDbType.NVarChar, -1).Value = adminMemberID;
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
                    }
                    return result;

                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}


