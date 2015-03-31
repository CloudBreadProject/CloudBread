using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using CloudBreadAdminWeb.globals;

namespace CloudBreadAdminWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Azure Storage에 admin log queue와 admin log table 체크
            try
            {
                //최초 어플리케이션이 수행될때 Azure Table Storage에 CloudBreadAdminLog 테이블, 
                // Azure Queue Service에서 messagestoadminlog(소문자필수) Queue가 생성되어 있지 않으면 생성
                if (globalVal.StorageConnectionString != "")
                {
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(globalVal.StorageConnectionString);
                    CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                    var tableClient1 = storageAccount.CreateCloudTableClient();
                    var cloudTable = tableClient1.GetTableReference("CloudBreadAdminLog");
                    cloudTable.CreateIfNotExists();

                    CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                    CloudQueue queue = queueClient.GetQueueReference("messagestoadminlog");      // 큐 이름은 반드시 소문자
                    queue.CreateIfNotExists();

                }
            }
            catch (System.Exception ex)
            {
                //에러 처리
                throw ex;
            }


        }

    }
}
