using System.Collections.Generic;
using Abp.MultiTenancy;
using abp_angularjs.Assets;
using abp_angularjs.Authorization.Users;

namespace abp_angularjs.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }

        public List<Asset> Assets { get; set; }
    }
}