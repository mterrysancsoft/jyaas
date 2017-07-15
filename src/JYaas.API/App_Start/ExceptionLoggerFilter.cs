using System.Web;
using System.Web.Mvc;
using Serilog;

namespace JYaas.API.App_Start
{
    public class ExceptionLoggerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            HttpRequestBase Request = filterContext.RequestContext.HttpContext.Request;
            Log.Fatal(filterContext.Exception, "{Method} {Path:l} ", Request.HttpMethod, Request.RawUrl);
        }
    }
}