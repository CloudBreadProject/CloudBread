using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using CloudBread.DataObjects;
using CloudBread.Models;
using Owin;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using CloudBread.globals;
using CloudBreadRedis;
using System;
using System.Web;


namespace CloudBread
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            /// adding for API routing
            config.MapHttpAttributeRoutes();

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }


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
            catch (Exception ex)
            {
                /// web.config or portal application setting - connection string setting error
                //throw ex;
            }

            app.UseWebApi(config);
        }
    }

    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false }
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }

            base.Seed(context);
        }
    }
}

