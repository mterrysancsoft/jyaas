using System.Web;
using System.Web.Mvc;
using JYaas.API.App_Start;

namespace JYaas.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionLoggerFilter());
        }
    }
}
