/**
* @file Global.asax.cs
* @brief CloudBread startup task processor. \n
* @author Dae Woo Kim
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using CloudBread.globals;
using CloudBreadRedis;

namespace CloudBread
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                /// On start up, CreateIfNotExists CloudBreadLog table on Azure Table Storage
                /// On start up, CreateIfNotExists messagestolog table on Azure Queue Service
                if (globalVal.StorageConnectionString != "")
                {
                    /// this table is used for CloudBread game log saving
                    /// Azure Storage connection retry policy
                    var retryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(2), 10);
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(globalVal.StorageConnectionString);
                    CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                    tableClient.DefaultRequestOptions.RetryPolicy = retryPolicy;
                    var cloudTable = tableClient.GetTableReference("CloudBreadLog");
                    cloudTable.CreateIfNotExists();
                    cloudTable = tableClient.GetTableReference("CloudBreadErrorLog");
                    cloudTable.CreateIfNotExists();

                    /// this queue is used for CloudBread queue method game log saving
                    CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                    queueClient.DefaultRequestOptions.RetryPolicy = retryPolicy;
                    CloudQueue queue = queueClient.GetQueueReference("messagestolog");      /// must be lowercase
                    queue.CreateIfNotExists();

                    /// this queue is used for CloudBread queue method game log saving
                    queue = queueClient.GetQueueReference("cloudbread-batch");      /// must be lowercase
                    queue.CreateIfNotExists();

                }

                // Regarding to configuration, check startup fill or not
                if (globalVal.CloudBreadFillRedisRankSetOnStartup)
                {
                    // execute redis rank fill task
                    CBRedis.FillAllRankFromDB();
                }
                

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
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