/**
* @file CBSocketTokenValidCheckController
* @brief This API is used for CloudBread-Socket request token validation from Redis cache service \n
* https://github.com/CloudBreadProject/CloudBread-Socket \n
* @param string memberid, string token.guid
* @return string memberid, string token.guid, string genDateUTC
* @author Dae Woo Kim
******************************************************************
* @todo limit access this API only CloudBread-Socket poejct. \n
* direct connect from Socket Server to redis = this api will be stop development until CloudBread v3.
******************************************************************
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

namespace CloudBread.Controllers
{
    //[MobileAppController]     // only access by CloudBread.Socket project.
    public class CBSocketTokenValidCheckController : ApiController
    {
        public class Result
        {
            public string guid { get; set; }
            public string sid { get; set; }
            public string genDateUTC { get; set; }
        }

        // Payload object
        public class Token
        {
            public string guid { get; set; }
            public string sid { get; set; }     // logging purpose
        }

        /// @brief added route for change attrbute.
        [Route("api/CBSocketTokenValidCheck")]
        [HttpPost]
        // GET api/CBSocketTokenValidCheck
        public Result POST(Token token)
        {

            /// Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = CBAuth.getMemberID("debug", claimsPrincipal);
            // payload.sid = sid;

            // return
            Result r = new Result();
            string redisResult = "";

            /// logging purpose
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(token);

            try
            {
                /// fetch redis value by requested token.guid
                redisResult = CBRedis.GetRedisKeyValue(token.guid);

                if (redisResult == null)
                {
                    // does not exist on Redis.
                    r.guid = "";
                    r.sid = "";
                    r.genDateUTC = "";
                }
                else
                {
                    // Deserialize json
                    r = JsonConvert.DeserializeObject<Result>(redisResult);
                }

                return r;
            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = token.sid;        // requested value. Not redis data value.
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBSocketTokenValidCheck";
                logMessage.Message = jsonParam;
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }
    }
}
