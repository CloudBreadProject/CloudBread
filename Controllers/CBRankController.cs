/**
* @file CBRankController
* @brief This API is used for CloudBread leader board - rank service \n
* @author Dae Woo Kim
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
using Newtonsoft.Json;
using CloudBreadAuth;
using CloudBreadRedis;
using System.Security.Claims;
using StackExchange.Redis;

namespace CloudBread.Controllers
{
    [MobileAppController]
    public class CBRankController : ApiController
    {
        public class MemberRankNumber
        {
            public long rank { get; set; }
        }

        [Route("api/rank/{sid}/ranknumber")]
        [HttpGet]
        public MemberRankNumber Get(string sid)
        {
            MemberRankNumber result = new MemberRankNumber();

            /// logging purpose
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(sid);

            try
            {
                /// fetch redis value by member sid
                result.rank = CBRedis.GetSortedSetRank(sid);

                return result;
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = sid;        // requested value. Not redis data value.
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBRankController-MemberRankNumber";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }

        /// Get my rank and then call this method to fetch +-10 rank(total 20) rank
        [Route("api/rankerlist/{sid}/{startrank}/{endrank}")]
        [HttpGet]
        public string GET(string sid, long startRank, long endRank)
        {
            string jsonResult = "";
            MemberRankNumber result = new MemberRankNumber();

            /// logging purpose
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(sid);
            
            try
            {
                /// fetch redis list by rank range 
                return jsonResult = CBRedis.GetSortedSetRankByRange(startRank, endRank);
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = sid;        // requested value. Not redis data value.
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBRankController-RankerListByRange";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }




    }
}
