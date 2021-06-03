using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using abp_angularjs.Roles.Dto;

namespace abp_angularjs.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
