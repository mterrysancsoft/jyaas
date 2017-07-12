using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Configuration;
using System.Text.RegularExpressions;
using CallfireApiClient;
using CallfireApiClient.Api.CallsTexts.Model;

namespace JYaas.API.Controllers
{
    public class CallController : ApiController
    {
        /// <summary>
        /// Send a Jian Yang call with a simple get to the supplied phone number
        /// </summary>
        /// <param name="phoneNumber"></param>
        public void Get([FromUri]string phoneNumber)
        {
            if (!Regex.Match(phoneNumber, @"^1([0-9]{10})$").Success)
            {
                throw new Exception("Invalid phone number supplied (1234567890)");
            }
            var client = new CallfireClient(ConfigurationManager.AppSettings["CallFireUser"], ConfigurationManager.AppSettings["CallFirePassword"]);
            var recipients = new List<CallRecipient>
            {
                new CallRecipient
                {
                    PhoneNumber = phoneNumber,
                    LiveMessageSoundId = 5743849003,
                    MachineMessageSoundId = 5743849003
                }
            };
            IList<Call> calls = client.CallsApi.Send(recipients);
        }
        /// <summary>
        /// Send a Jian Yang call to the supplied phone number
        /// </summary>
        /// <param name="phoneNumber">Receiving phone number in format 123456789</param>
        public void Post([FromUri]string phoneNumber)
        {
            if (!Regex.Match(phoneNumber, @"^1([0-9]{10})$").Success)
            {
                throw new Exception("Invalid phone number supplied (1234567890)");
            }
            var client = new CallfireClient("7e6b76d5907b", "ec28a86f969bb6b0");
            var recipients = new List<CallRecipient>
            {
                new CallRecipient
                {
                    PhoneNumber = phoneNumber,
                    LiveMessageSoundId = 5743849003,
                    MachineMessageSoundId = 5743849003
                }
            };
            IList<Call> calls = client.CallsApi.Send(recipients);
        }
    }
}
