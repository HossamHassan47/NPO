using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NPO.Code
{
    public static class MailHelper
    {
        public static string userName = "NPOtest@outlook.com";

        public static string password = "npo@123456";
        public static ExchangeService Exchange_Service()
        {
            ExchangeService _service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);

            try
            {
                _service.Credentials = new NetworkCredential(userName, password);
            }
            catch
            {
                return null;
            }

            _service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");

            return _service;
        }



        public static bool SendMail(EntityEmail email)
        {
            try
            {
                EmailMessage msg = new EmailMessage(Exchange_Service());
                var ToRecipients = email.To.Split(',');

                foreach (var item in ToRecipients)
                {
                    if (item != "")
                    {
                        msg.ToRecipients.Add(item.Trim());
                    }
                }
                msg.Subject = email.Subject;
                msg.Body = email.Body;
                msg.Send();
            }
            catch
            {

                return false;
            }
            return true;

        }
    }
}
