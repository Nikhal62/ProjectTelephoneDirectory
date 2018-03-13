using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Business_Logic_Layer.Service;
using Data_Access_Layer.Repository;
using Data_Access_Layer.DBContext;

namespace Presentation_Layer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterType<TelephoneDirectoryService>().As<ITelephoneDirectoryService>();
            builder.RegisterType<TelephoneDirectoryRepository>().As<ITelephoneDirectoryRepository>();
            builder.RegisterType<TelephoneDirectoryDbContext>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
