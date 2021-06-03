using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using abp_angularjs.Roles.Dto;
using abp_angularjs.Users.Dto;

namespace abp_angularjs.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}