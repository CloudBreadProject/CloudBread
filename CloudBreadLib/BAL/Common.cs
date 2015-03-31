using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBreadLib.BAL.UserTime
{
    public class UserTime
    {
        public static DateTime GetUserTime(DateTime utcTime, string timeZoneById)
        {
            //DateTime timeUtc = DateTime.UtcNow;
            // timeZoneById = "Korea Standard Time";
            //utcTime = utcTime.UtcDateTime;
            try
            {
                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneById);
                DateTime userTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, userTimeZone);
                return userTime;

                //Debug.WriteLine("The date and time are {0} {1}.",
                //                    userTime,
                //                    userTimeZone.IsDaylightSavingTime(userTime) ?
                //                            userTimeZone.DaylightName : userTimeZone.StandardName);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public static DateTime GetUserTime(string utcTime, string timeZoneById)
        {
            //DateTime timeUtc = DateTime.UtcNow;
            // timeZoneById = "Korea Standard Time";
            //utcTime = utcTime.UtcDateTime;
            try
            {
                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneById);
                DateTime userTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(utcTime), userTimeZone);
                return userTime;

                //Debug.WriteLine("The date and time are {0} {1}.",
                //                    userTime,
                //                    userTimeZone.IsDaylightSavingTime(userTime) ?
                //                            userTimeZone.DaylightName : userTimeZone.StandardName);
            }
            catch (Exception)
            {
                throw;
            }

        }

        

        public static DateTime SetUtcTime(DateTime userTime, string timeZoneById)
        {
            //DateTime userTime = DateTime.Parse("2011-01-01 12:00:00");
            // timeZoneById = "Korea Standard Time";
            //kind 오류?
            try
            {
                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneById);
                DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(userTime, userTimeZone);
                return utcTime;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
