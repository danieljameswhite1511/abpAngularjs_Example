using System.Linq;
using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Modules;
using abp_angularjs.Assets;
using abp_angularjs.Authorization.Roles;
using abp_angularjs.Authorization.Users;
using abp_angularjs.MultiTenancy;
using abp_angularjs.MultiTenancy.Dto;
using abp_angularjs.Roles.Dto;
using abp_angularjs.Users.Dto;

namespace abp_angularjs
{
    [DependsOn(typeof(abp_angularjsCoreModule), typeof(AbpAutoMapperModule))]
    public class abp_angularjsApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
	        Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
	        {
		        config.CreateMap<Tenant, TenantDto>().ReverseMap();
				config.CreateMap<Asset, AssetDto>().ReverseMap();

	        });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            // TODO: Is there somewhere else to store these, with the dto classes
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Role and permission
                cfg.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
                cfg.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

                cfg.CreateMap<CreateRoleDto, Role>();
                cfg.CreateMap<RoleDto, Role>();
                cfg.CreateMap<Role, RoleDto>().ForMember(x => x.GrantedPermissions,
                    opt => opt.MapFrom(x => x.Permissions.Where(p => p.IsGranted)));

                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<UserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
            });
        }
    }
}
