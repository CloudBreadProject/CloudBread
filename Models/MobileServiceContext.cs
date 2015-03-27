using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Tables;
//using CloudBread.DataObjects;

namespace CloudBread.Models
{

    public class MobileServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        //
        // To enable Entity Framework migrations in the cloud, please ensure that the 
        // service name, set by the 'MS_MobileServiceName' AppSettings in the local 
        // Web.config, is the same as the service name when hosted in Azure.

        private const string connectionStringName = "Name=CloudBreadDBConString";

        public MobileServiceContext()
            : base(connectionStringName)
        {
        }

        //public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schema = ServiceSettingsDictionary.GetSchemaName();
            if (!string.IsNullOrEmpty(schema))
            {
                modelBuilder.HasDefaultSchema(schema);
            }

            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }

        //public System.Data.Entity.DbSet<CloudBread.DataObjects.Member> Member { get; set; }

        //public System.Data.Entity.DbSet<CloudBread.DataObjects.AdminMember> AdminMember { get; set; }

        //public System.Data.Entity.DbSet<CloudBread.DataObjects.ItemList> ItemList { get; set; }

        //public System.Data.Entity.DbSet<CloudBread.DataObjects.MemberItemPurchase> MemberItemPurchases { get; set; }

        //public System.Data.Entity.DbSet<CloudBread.DataObjects.MemberGameInfo> MemberGameInfo { get; set; }

        //public System.Data.Entity.DbSet<CloudBread.DataObjects.MemberItem> MemberItem { get; set; }

        //public System.Data.Entity.DbSet<CloudBread.DataObjects.GiftDepository> GiftDepository { get; set; }

        //public System.Data.Entity.DbSet<CloudBread.DataObjects.doMember> doMembers { get; set; }
    }

}
