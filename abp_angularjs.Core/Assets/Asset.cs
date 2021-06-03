
using Abp.Domain.Entities;
using abp_angularjs.MultiTenancy;

namespace abp_angularjs.Assets
{
	public class Asset : Entity<int>
	{
		public string Name { get; set; }

		public  int TenantId  { get; set; }
		public Tenant Tenant { get; set; }
	}
}
