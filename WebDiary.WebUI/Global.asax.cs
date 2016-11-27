using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject.Web.Mvc;
using WebDiary.DAL.Context;

namespace WebDiary
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var kernel = DependencyConfigurations.RegisterDependency();
            Database.SetInitializer<ApplicationDbContext>(null);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
