using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using abp_angularjs.EntityFramework;

namespace abp_angularjs.Migrator
{
    [DependsOn(typeof(abp_angularjsDataModule))]
    public class abp_angularjsMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<abp_angularjsDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}