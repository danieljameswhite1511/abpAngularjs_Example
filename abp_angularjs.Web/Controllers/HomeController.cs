using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace abp_angularjs.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : abp_angularjsControllerBase
    {
        public ActionResult Index()
        {
             return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}