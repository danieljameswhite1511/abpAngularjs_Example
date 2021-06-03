using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using abp_angularjs.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace abp_angularjs.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<abp_angularjs.EntityFramework.abp_angularjsDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "abp_angularjs";
        }

        protected override void Seed(abp_angularjs.EntityFramework.abp_angularjsDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
