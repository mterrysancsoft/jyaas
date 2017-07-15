using System.IO;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using Serilog;

namespace JYaas.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Log.Logger = new LoggerConfiguration().CreateLogger();
            Serilog.Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("appname", Assembly.GetExecutingAssembly().GetName().Name)
                .WriteTo.RollingFile(Path.Combine(ConfigurationManager.AppSettings["LogPath"], "serilog-{Date}.txt"))
                .CreateLogger();
        }
    }
}
