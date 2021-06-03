using Abp.Web.Mvc.Views;

namespace abp_angularjs.Web.Views
{
    public abstract class abp_angularjsWebViewPageBase : abp_angularjsWebViewPageBase<dynamic>
    {

    }

    public abstract class abp_angularjsWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected abp_angularjsWebViewPageBase()
        {
            LocalizationSourceName = abp_angularjsConsts.LocalizationSourceName;
        }
    }
}