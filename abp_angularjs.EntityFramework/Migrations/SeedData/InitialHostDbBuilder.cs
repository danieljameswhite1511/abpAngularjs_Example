using abp_angularjs.EntityFramework;
using EntityFramework.DynamicFilters;

namespace abp_angularjs.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly abp_angularjsDbContext _context;

        public InitialHostDbBuilder(abp_angularjsDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
