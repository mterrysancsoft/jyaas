using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
using CallfireApiClient;
using CallfireApiClient.Api.CallsTexts.Model;

namespace JYaas
{
    class Program
    {
        static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^1([0-9]{10})$").Success;
        }

        static void Main(string[] args)
        {
            // CallFire requires TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new CallfireClient(ConfigurationManager.AppSettings["CallFireUser"], ConfigurationManager.AppSettings["CallFirePassword"]);
            var account = client.MeApi.GetBillingPlanUsage();
            int credits = (int)(account?.TotalRemainingCredits ?? 0);
            Console.WriteLine("# of credits remaining {0}", credits);

            if ((args.Length == 0) || (!IsPhoneNumber(args[0])))
            {
                Console.WriteLine("Usage: jyaas.console.exe [phonenumber:1234567890]");
                return;
            }


            var recipients = new List<CallRecipient>
            {
                new CallRecipient
                {
                    PhoneNumber = args[0],
                    LiveMessageSoundId = 12329299003,
                    MachineMessageSoundId = 12329299003
                }
            };
            IList<Call> calls = client.CallsApi.Send(recipients);
        }
    }
}
