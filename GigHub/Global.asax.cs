using AutoMapper;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(Config);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void Config(IMapperConfigurationExpression cfgExpression)
        {
            cfgExpression.CreateMap<ApplicationUser, UserDto>();
            cfgExpression.CreateMap<Genre, GenreDto>();
            cfgExpression.CreateMap<Gig, GigDto>();
            cfgExpression.CreateMap<Notification, NotificationDto>();
        }
    }
}
