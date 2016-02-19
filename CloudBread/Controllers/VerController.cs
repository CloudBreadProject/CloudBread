using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;

namespace CloudBread.Controllers
{
    // Use the MobileAppController attribute for each ApiController you want to use  
    // from your mobile clients 
    [MobileAppController]
    public class VerController : ApiController
    {
        // GET api/values
        public string Get()
        {
            return "CloudBread ver 2.0.0-dev";
        }

        // POST api/values
        //public string Post()
        //{
        //    return "Hello World!";
        //}

        // POST api/values

    }
}
