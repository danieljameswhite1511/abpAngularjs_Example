using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using abp_angularjs.Authorization;
using abp_angularjs.Authorization.Roles;
using abp_angularjs.Authorization.Users;
using abp_angularjs.Editions;
using abp_angularjs.MultiTenancy.Dto;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace abp_angularjs.MultiTenancy
{
    [AbpAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantAppService : AsyncCrudAppService<Tenant, TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>, ITenantAppService
    {
	    private readonly IMapper _mapper;
	    private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly IRepository<Tenant, int> _repository;

        public TenantAppService(
            IMapper mapper,
            IRepository<Tenant, int> repository,

            TenantManager tenantManager,
            EditionManager editionManager,
            UserManager userManager,

            RoleManager roleManager,
            IAbpZeroDbMigrator abpZeroDbMigrator
        ) : base(repository)
        {
	        _mapper = mapper;
	        _tenantManager = tenantManager;
            _editionManager = editionManager;
            _roleManager = roleManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _userManager = userManager;
            _repository = repository;
        }



        public override async Task<TenantDto> GetAsync(EntityDto<int> input)
        {
	        var tenant = _repository.GetAllIncluding(x => x.Assets).SingleOrDefault(x => x.Id == input.Id);

	        
		        var tenantDto = _mapper.Map<Tenant, TenantDto>(tenant);
				return tenantDto;
	  
	       

	      
        }
        public override async Task<TenantDto> CreateAsync(CreateTenantDto input)
        {
            CheckCreatePermission();

            //Create tenant
            var tenant = ObjectMapper.Map<Tenant>(input);
            tenant.ConnectionString = input.ConnectionString.IsNullOrEmpty()
                ? null
                : SimpleStringCipher.Instance.Encrypt(input.ConnectionString);

            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            await _tenantManager.CreateAsync(tenant);
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new tenant's id.

            //Create tenant database
            _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

            //We are working entities of new tenant, so changing tenant filter
            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                //Create static roles for new tenant
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync(); //To get static role ids

                //grant all permissions to admin role
                var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                //Create admin user for the tenant
                var adminUser = User.CreateTenantAdminUser(tenant.Id, input.AdminEmailAddress, User.DefaultPassword);
                CheckErrors(await _userManager.CreateAsync(adminUser));
                await CurrentUnitOfWork.SaveChangesAsync(); //To get admin user's id

                //Assign admin user to role!
                CheckErrors(await _userManager.AddToRoleAsync(adminUser.Id, adminRole.Name));
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return MapToEntityDto(tenant);
        }

        protected override void MapToEntity(TenantDto updateInput, Tenant entity)
        {
            //Manually mapped since TenantDto contains non-editable properties too.
            entity.Name = updateInput.Name;
            entity.TenancyName = updateInput.TenancyName;
            entity.IsActive = updateInput.IsActive;
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            var tenant = await _tenantManager.GetByIdAsync(input.Id);
            await _tenantManager.DeleteAsync(tenant);
        }

        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}