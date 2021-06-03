using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace abp_angularjs.EntityFramework.Repositories
{
    public abstract class abp_angularjsRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<abp_angularjsDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected abp_angularjsRepositoryBase(IDbContextProvider<abp_angularjsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class abp_angularjsRepositoryBase<TEntity> : abp_angularjsRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected abp_angularjsRepositoryBase(IDbContextProvider<abp_angularjsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
