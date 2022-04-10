using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Reflection;
using System.Web.Mvc;
namespace CallWebAPI_IHttpClientFactory.App_Start
{
    public class AutofacConfig
    {
        /// <summary>
        /// 此段須加在Global.asax底下觸發
        /// </summary>
        public static void Bootstrapper()
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(assembly);
            builder.Register(c =>
            {
                var hostBuilder = new HostBuilder();
                hostBuilder.ConfigureServices(s => s.AddHttpClient());
                return hostBuilder.Build().Services.GetService<IHttpClientFactory>();
            }).SingleInstance();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}