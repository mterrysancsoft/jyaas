using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Http.Cors;
using CallfireApiClient;
using CallfireApiClient.Api.CallsTexts.Model;

namespace JYaas.API.Controllers
{
    [EnableCors(origins: "http://www.jyaas.com", headers: "*", methods: "get,post")]
    public class CallController : ApiController
    {
        // not mom, fridge, alone
        protected long[] messages = { 5754254003, 5754252003, 5754245003 };

        /// <summary>
        /// Send a Jian Yang call to the supplied phone number
        /// </summary>
        /// <param name="phoneNumber">Receiving phone number in format 123456789</param>
        /// <param name="messageId">Receiving phone number in format 123456789</param>
        public void Post([FromUri]string phoneNumber, [FromUri]int messageId)
        {
            if (!Regex.Match(phoneNumber, @"^1([0-9]{10})$").Success)
            {
                throw new Exception("Invalid phone number supplied (1234567890)");
            }
            if (messageId > messages.Length)
            {
                throw new Exception("Invalid message identifier, out of range");
            }
            // if the requested messageId is 0, select a message randomly
            if (messageId == 0)
            {
                Random rnd = new Random();
                messageId = rnd.Next(0, messages.Length);
            }
            else
            {
                messageId = messageId - 1;
            }

            var client = new CallfireClient(ConfigurationManager.AppSettings["CallFireUser"], ConfigurationManager.AppSettings["CallFirePassword"]);
            var recipients = new List<CallRecipient>
            {
                new CallRecipient
                {
                    PhoneNumber = phoneNumber,
                    LiveMessageSoundId = messages[messageId],
                    MachineMessageSoundId = messages[messageId]
                }
            };
            IList<Call> calls = client.CallsApi.Send(recipients);
        }
    }
}
