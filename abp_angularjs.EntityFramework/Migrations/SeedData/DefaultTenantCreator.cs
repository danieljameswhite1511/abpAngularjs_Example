using System.Linq;
using abp_angularjs.EntityFramework;
using abp_angularjs.MultiTenancy;

namespace abp_angularjs.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly abp_angularjsDbContext _context;

        public DefaultTenantCreator(abp_angularjsDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
