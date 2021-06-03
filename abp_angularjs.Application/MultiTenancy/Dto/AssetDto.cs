
using Abp.AutoMapper;
using abp_angularjs.Assets;

namespace abp_angularjs.MultiTenancy.Dto
{
	
	public class AssetDto
	{
		public string Name { get; set; }

		public int TenantId { get; set; }
	}
}
