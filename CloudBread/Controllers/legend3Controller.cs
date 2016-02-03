using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CloudBread.Controllers
{
    public class legend3Controller : ApiController
    {
        // GET: api/legend3
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/legend3/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/legend3
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/legend3/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/legend3/5
        public void Delete(int id)
        {
        }
    }
}
