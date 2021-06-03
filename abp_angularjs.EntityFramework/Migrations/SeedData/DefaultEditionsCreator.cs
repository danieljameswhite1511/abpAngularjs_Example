using System.Linq;
using Abp.Application.Editions;
using abp_angularjs.Editions;
using abp_angularjs.EntityFramework;

namespace abp_angularjs.Migrations.SeedData
{
    public class DefaultEditionsCreator
    {
        private readonly abp_angularjsDbContext _context;

        public DefaultEditionsCreator(abp_angularjsDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            var defaultEdition = _context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
            if (defaultEdition == null)
            {
                defaultEdition = new Edition { Name = EditionManager.DefaultEditionName, DisplayName = EditionManager.DefaultEditionName };
                _context.Editions.Add(defaultEdition);
                _context.SaveChanges();

                //TODO: Add desired features to the standard edition, if wanted!
            }   
        }
    }
}