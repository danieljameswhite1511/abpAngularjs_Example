using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using abp_angularjs.Configuration.Dto;

namespace abp_angularjs.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : abp_angularjsAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
