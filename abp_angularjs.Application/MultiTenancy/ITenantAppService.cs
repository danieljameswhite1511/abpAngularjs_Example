using Abp.Application.Services;
using Abp.Application.Services.Dto;
using abp_angularjs.MultiTenancy.Dto;

namespace abp_angularjs.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
