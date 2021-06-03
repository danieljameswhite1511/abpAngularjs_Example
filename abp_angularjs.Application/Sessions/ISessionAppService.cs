using System.Threading.Tasks;
using Abp.Application.Services;
using abp_angularjs.Sessions.Dto;

namespace abp_angularjs.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
