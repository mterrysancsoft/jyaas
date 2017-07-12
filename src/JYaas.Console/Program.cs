using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
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
            var client = new CallfireClient(ConfigurationManager.AppSettings["CallFireUser"], ConfigurationManager.AppSettings["CallFirePassword"]);

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
                    LiveMessageSoundId = 5743849003,
                    MachineMessageSoundId = 5743849003
                }
            };
            IList<Call> calls = client.CallsApi.Send(recipients);
        }
    }
}
