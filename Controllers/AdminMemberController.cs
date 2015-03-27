using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using CloudBread.DataObjects;
using CloudBread.Models;

namespace CloudBread.Controllers
{
    public class AdminMemberController : TableController<AdminMember>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<AdminMember>(context, Request, Services);
        }

        // GET tables/AdminMember
        public IQueryable<AdminMember> GetAllAdminMember()
        {
            return Query(); 
        }

        // GET tables/AdminMember/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<AdminMember> GetAdminMember(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/AdminMember/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<AdminMember> PatchAdminMember(string id, Delta<AdminMember> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/AdminMember/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostAdminMember(AdminMember item)
        {
            AdminMember current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/AdminMember/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAdminMember(string id)
        {
             return DeleteAsync(id);
        }

    }
}