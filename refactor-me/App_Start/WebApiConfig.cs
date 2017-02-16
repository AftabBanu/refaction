using Microsoft.Practices.Unity;
using System.Web.Http;
using Refactor_BusinessServices.Services;
using Unity.WebApi;
using System.Web.Http.ExceptionHandling;

namespace refactor_me
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {  
            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.Indent = true;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

 
        }
    }
}
