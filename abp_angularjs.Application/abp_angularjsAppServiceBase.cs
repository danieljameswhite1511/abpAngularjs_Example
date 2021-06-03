using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using abp_angularjs.Authorization.Users;
using abp_angularjs.MultiTenancy;
using abp_angularjs.Users;
using Microsoft.AspNet.Identity;

namespace abp_angularjs
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class abp_angularjsAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected abp_angularjsAppServiceBase()
        {
            LocalizationSourceName = abp_angularjsConsts.LocalizationSourceName;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}