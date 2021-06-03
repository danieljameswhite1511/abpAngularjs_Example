using Abp.Authorization;
using abp_angularjs.Authorization.Roles;
using abp_angularjs.Authorization.Users;

namespace abp_angularjs.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
