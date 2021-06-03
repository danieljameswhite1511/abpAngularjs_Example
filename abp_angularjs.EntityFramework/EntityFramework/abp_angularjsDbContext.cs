using System.Data.Common;
using System.Data.Entity;
using Abp.DynamicEntityProperties;
using Abp.Zero.EntityFramework;
using abp_angularjs.Assets;
using abp_angularjs.Authorization.Roles;
using abp_angularjs.Authorization.Users;
using abp_angularjs.MultiTenancy;

namespace abp_angularjs.EntityFramework
{
    public class abp_angularjsDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        public virtual IDbSet<Asset> Assets { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public abp_angularjsDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in abp_angularjsDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of abp_angularjsDbContext since ABP automatically handles it.
         */
        public abp_angularjsDbContext(string nameOrConnectionString)
            :  base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public abp_angularjsDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public abp_angularjsDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DynamicProperty>().Property(p => p.PropertyName).HasMaxLength(250);
            modelBuilder.Entity<DynamicEntityProperty>().Property(p => p.EntityFullName).HasMaxLength(250);
        }
    }
}
