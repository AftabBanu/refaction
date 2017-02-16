using Microsoft.Practices.Unity;
using Refactor_BusinessServices.Services;
using System.Web.Http;
using Unity.WebApi;

namespace refactor_me
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {

            //Unity Container
            var container = new UnityContainer();
            container.RegisterType<IProductService, ProductService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductOptionService, ProductOptionService>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

         }
    }
}