using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using abp_angularjs.EntityFramework;

namespace abp_angularjs
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(abp_angularjsCoreModule))]
    public class abp_angularjsDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<abp_angularjsDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
