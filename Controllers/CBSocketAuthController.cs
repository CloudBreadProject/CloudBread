/**
* @file CBSocketAuthController.cs
* @brief This API is used for CloudBread-Socket porject as request JWT \n
* https://github.com/CloudBreadProject/CloudBread-Socket \n
* To generate token, should be authenticated by auth provider 
* @author Dae Woo Kim
* @return json token with encryption
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
using System.Security.Claims;

namespace CloudBread.Controllers
{
    [MobileAppController]
    public class CBSocketAuthController : ApiController
    {
        // return token
        public class Token
        {
            public string token { get; set; }
        }

        // Payload object
        public class Payload
        {
            public string guid { get; set; }
            public string sid { get; set; }
            public string genDateUTC { get; set; }
        }

        // GET api/CBSocketAuth
        public Token Get()
        {
            Payload payload = new Payload();

            /// Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = CBAuth.getMemberID("debug", claimsPrincipal);
            payload.sid = sid;

            /// logging purpose
            Logging.CBLoggers logMessage = new Logging.CBLoggers();
            string jsonParam = JsonConvert.SerializeObject(sid);

            try
            {
                // return token
                Token t = new Token();

                // generate paylod 
                payload.guid = Guid.NewGuid().ToString();
                payload.genDateUTC = DateTimeOffset.UtcNow.ToString();

                // token Serialize and encrypte
                t.token = Crypto.AES_encrypt(JsonConvert.SerializeObject(payload), globalVal.CloudBreadSocketKeyText, globalVal.CloudBreadSocketKeyIV);

                return t;

            }

            catch (Exception ex)
            {
                // error log
                logMessage.memberID = sid;
                logMessage.Level = "ERROR";
                logMessage.Logger = "CBSocketAuth";
                logMessage.Message = "SocketAuth error";
                logMessage.Exception = ex.ToString();
                Logging.RunLog(logMessage);

                throw;
            }
        }
    }
}
