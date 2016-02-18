using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using CloudBread.globals;

namespace CloudBread
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                //최초 어플리케이션이 수행될때 Azure Table Storage에 CloudBreadLog 테이블, 
                // Azure Queue Service에서 messagestolog(소문자필수) Queue가 생성되어 있지 않으면 생성
                if (globalVal.StorageConnectionString != "")
                {
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(globalVal.StorageConnectionString);
                    CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                    var tableClient1 = storageAccount.CreateCloudTableClient();
                    var cloudTable = tableClient1.GetTableReference("CloudBreadLog");
                    cloudTable.CreateIfNotExists();

                    CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                    CloudQueue queue = queueClient.GetQueueReference("messagestolog");      // 큐 이름은 반드시 소문자
                    queue.CreateIfNotExists();

                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}