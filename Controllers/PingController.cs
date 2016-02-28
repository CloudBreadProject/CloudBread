/**
* @file PingController.cs
* @brief CloudBread app ping test API and authentication check \n
* @author Dae Woo Kim
*/

using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using System.Security.Claims;
using CloudBreadAuth;


namespace CloudBread.Controllers
{

    [MobileAppController]
    public class PingController : ApiController
    {

        /// GET api/ping - return ping test string
        public string Get()
        {

            CloudBreadRedis.CBRedis.FillAllRankFromDB("test");

            return "Hello"; 
        }

        // POST api/ping - return current authentication member SID
        public string Post()
        {
            string sid;
            // Get the sid or memberID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            sid = CBAuth.getMemberID("non-auth member", claimsPrincipal);

            return "Hello " + sid ;
        }

    }
}
