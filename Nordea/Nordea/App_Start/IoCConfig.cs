using Autofac;
using Autofac.Integration.Mvc;
using Nordea.Service;
using Nordea.Service.Interfaces;
using Nordea.Utils;
using Nordea.Utils.Interfaces;
using System.Web.Mvc;

namespace Nordea
{
    public class IoCConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TextService>().As<ITextService>().InstancePerRequest();

            builder.RegisterType<TextUtil>().As<ITextUtil>().InstancePerRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly)
               .InstancePerRequest();

            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}