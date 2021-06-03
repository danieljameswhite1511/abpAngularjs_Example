using Abp.AutoMapper;
using abp_angularjs.Sessions.Dto;

namespace abp_angularjs.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}