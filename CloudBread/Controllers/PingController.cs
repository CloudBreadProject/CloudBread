using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using System.Security.Claims;

namespace CloudBread.Controllers
{
    // Use the MobileAppController attribute for each ApiController you want to use  
    // from your mobile clients 
    [MobileAppController]
    public class PingController : ApiController
    {
        // GET api/values
        public string Get()
        {
            return "Hello";
        }

        // POST api/values
        public string Post()
        {
            string sid;
            // Get the SID of the current user.
            var claimsPrincipal = this.User as ClaimsPrincipal;
            if(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                sid = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                sid = "no-auth" ; 
            }

            return sid ;
        }

        // POST api/values

    }
}
