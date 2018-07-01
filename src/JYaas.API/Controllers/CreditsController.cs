using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Http.Cors;
using Serilog;
using CallfireApiClient;
using CallfireApiClient.Api.CallsTexts.Model;

namespace JYaas.API.Controllers
{
    [EnableCors(origins: "http://www.jyaas.com,https://www.jyaas.com", headers: "*", methods: "get,post")]
    public class CreditsController : ApiController
    {
        /// <summary>
        /// Get the available number of credits on the account. Returns 0 if it can't get the number of 
        /// credits.
        /// </summary>
        /// <returns>Number of Credits</returns>
        public int Get()
        {
            Log.Information("Get credits");
            // CallFire requires TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new CallfireClient(ConfigurationManager.AppSettings["CallFireUser"], ConfigurationManager.AppSettings["CallFirePassword"]);
            var account = client.MeApi.GetBillingPlanUsage();
            int credits = (int)(account?.TotalRemainingCredits ?? 0);
            Log.Information("# of credits remaining {0}", credits);
            return credits;
        }
    }
}
