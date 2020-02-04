using DatabaseLayer.ModelsDB;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DatabaseLayer.Context
{
    public class ECommerceDB : DbContext
    {
        public ECommerceDB() : base("ECommerceSiteDB")
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<ProductDB> productDB { get; set; }
        public DbSet<RegisterDB> registerDB { get; set; }
        public DbSet<AddressDB> addressDB { get; set; }
        public DbSet<UserProductDBModel> userProductDBModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
