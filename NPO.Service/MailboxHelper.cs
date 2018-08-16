using Microsoft.Exchange.WebServices.Data;
using NPO.Code.Entity;
using NPO.Code.Repository;
using System;
using System.IO;
using System.Net;


namespace NPO.Service
{
    public class MailboxHelper
    {
        EmailRepository emailRepository = new EmailRepository();

        public void StartRead()
        {
            ExchangeService _service = new ExchangeService(ExchangeVersion.Exchange2013_SP1); ;

            try
            {
                _service.Credentials = new NetworkCredential("Your Email", "Password");
            }
            catch
            {
                return;
            }

            // This is the office365 webservice URL
            _service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");

            FindItemsResults<Item> findResults = _service.FindItems(WellKnownFolderName.Inbox, new ItemView(100));



            foreach (EmailMessage item in findResults)
            {
                var emailEntity = this.GetEmailEntity(item);
                emailEntity.Path = GetMailBackUpPath();

                int id = emailRepository.InsertNewEmail(emailEntity);
                if (id > 0)
                {
                    BackUpMail(item, _service, emailEntity.Path);
                    DeleteEmail(item);
                }

            }
        }

        private void DeleteEmail(EmailMessage item)
        {
            item.Delete(DeleteMode.HardDelete);
        }

        private string GetMailBackUpPath()
        {
            string path = Program.mailBackUpPath + @"\" + DateTime.Now.Year + @"\" + DateTime.Now.Month + @"\" + DateTime.Now.Day;

            string fileName = @"\" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".eml";

            if (Directory.Exists(path))
            {
                return path + fileName;
            }
            else
            {
                Directory.CreateDirectory(path);
                return path + fileName;
            }
        }

        private void BackUpMail(EmailMessage item, ExchangeService _service, string path)
        {
            PropertySet psPropSet = new PropertySet(BasePropertySet.FirstClassProperties);
            psPropSet.Add(ItemSchema.MimeContent);
            var completeEmailMessage = EmailMessage.Bind(_service, item.Id, psPropSet);
            var mimeContent = completeEmailMessage.MimeContent;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                fileStream.Write(mimeContent.Content, 0, mimeContent.Content.Length);
            }

        }

        private Email GetEmailEntity(EmailMessage item)
        {
            Email email = new Email();
            item.Load(new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.TextBody));
            //   item.Load(new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.Body));

            email.Subject = item.Subject;
            email.Body = item.TextBody.ToString();
            //  email.BodyHtml = item.Body.Text;
            email.From = item.From.ToString();
            email.To = item.ToRecipients.ToString();
            email.CC = item.DisplayCc;
            email.DateTimeReceived = item.DateTimeReceived.ToString();


            return email;
        }
    }
}
