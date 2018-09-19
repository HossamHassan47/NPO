using Microsoft.Exchange.WebServices.Data;
using NPO.Code;
using NPO.Code.Entity;
using NPO.Code.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace NPO.Service
{
    public class MailboxHelper
    {
        public List<Controller> listOfControllers = getAllControllers();
        public List<Site> listOfSites = getAllSites();

        public void StartRead()
        {
            ExchangeService _service = MailHelper.Exchange_Service();

            FindItemsResults<Item> findResults = _service.FindItems(WellKnownFolderName.Inbox, new ItemView(100));

            EmailRepository emailRepository = new EmailRepository();
            foreach (EmailMessage item in findResults)
            {
                var emailEntity = this.GetEmailEntity(item);
                emailEntity.Path = GetMailBackUpPath();

                int emailParentId = CheckPerantEmail(item.Subject);
                if (emailParentId == -1)
                {
                    int id = emailRepository.InsertNewEmail(emailEntity);
                    if (id > 0)
                    {
                        BackUpMail(item, _service, emailEntity.Path);
                        DeleteEmail(item);
                        AssignAuto(item, id);
                    }
                }
                else
                {
                    int id = emailRepository.InsertNewEmailHavePerant(emailEntity, emailParentId);
                    if (id > 0)
                    {
                        BackUpMail(item, _service, emailEntity.Path);
                        DeleteEmail(item);  
                    }
                    if (CheckEmailClosed(item.TextBody.ToString()))
                    {
                        emailRepository.UpdateStutesClosed(emailParentId);
                    }

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
            //  item.Load(new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.Body));

            email.Subject = item.Subject;
            email.Body = item.TextBody.ToString();
            email.BodyHtml = item.Body.Text;
            email.From = item.From.Address.ToString();
            email.To = item.DisplayTo.ToString();
            email.CC = item.DisplayCc;
            email.DateTimeReceived = item.DateTimeReceived;
            return email;
        }




        private bool CheckEmailClosed(string body)
        {
            if (body.Contains("[Resolved]"))
            {
                return true;
            }
            return false;
        }

        #region CheckPerantEmail

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }


        private int CheckPerantEmail(string subject)
        {
           string emailRef = getBetween(subject, "{", "}");
            int emailParentId;
            EmailRepository parentEmails = new EmailRepository();

            if (!(emailRef ==""))
            {
                emailParentId = parentEmails.GetParentEmailId(emailRef);

                if (!(emailParentId == -1))
                {
                    return emailParentId;
                }
                //else
                //{
                //    emailParentId = SearchSameSubject(subject);
                //    if (!(emailParentId == -1))
                //    {
                //        return emailParentId;
                //    }
                //}
            }
            
            //else
            //{
            //    emailParentId = SearchSameSubject(subject);
            //    if (!(emailParentId == -1))
            //    {
            //        return emailParentId;
            //    }
            //}

            return -1;
        }

        private int SearchSameSubject(string emailSubject)
        {

            List<string> subjects = new List<string>();
            EmailRepository parentEmails = new EmailRepository();
            subjects = parentEmails.GetAllSubjects();
            string emailSubjectRe = emailSubject.Substring(5);
            string subject = subjects.Find(sub => emailSubjectRe == sub);
            if (subject != null)
            {
                int emailParentId =  parentEmails.GetParentEmailIdsub(subject);
                if (!(emailParentId == -1))
                {
                    return emailParentId;
                }
            }


            return -1;
        }


        #endregion

        #region  AssignAutomatic

        private static List<Controller> getAllControllers()
        {
            ControllerRepository getAllCon = new ControllerRepository();
            List<Controller> listOfControllers = getAllCon.GetAllControllers();
            return listOfControllers;
        }

        private static List<Site> getAllSites()
        {
            SiteRepository getAllSites = new SiteRepository();
            List<Site> listOfSites = getAllSites.GetAllSites();
            return listOfSites;
        }




        /*
        * First will search in subject about controllers name or site code or site name 
        * if exist go to and insert in database and assign users 
        * else go to search in body about same if exist make same 
        * if not exist in subject or body Exit from function and don't assign any user in this case Admin will assign manually 
        * 
        */
        private void AssignAuto(EmailMessage email, int id)
        {
            List<int> controllerId;


            List<int> conIdSub = SubContainsCon(email);
            if (conIdSub[0] == -1)
            {
                List<int> conIdBody = BodyContainsCon(email);
                if (conIdBody[0] == -1)
                {
                    return;
                }
                else
                {
                    controllerId = conIdBody.Distinct().ToList();

                }
            }
            else
            {
                controllerId = conIdSub.Distinct().ToList();
            }


            EmailRepository updateEmail = new EmailRepository();
            string emailRef = "NPO#" + id;
            updateEmail.UpdateStutes(emailRef, id);
            for (int i = 0; i < controllerId.Count; i++)
            {
                if (controllerId[i] != 0)
                {
                    updateEmail.AddControllerId(controllerId[i], id);
                    DataTable dataTable = updateEmail.GetControllerAssignUsers(controllerId[i]);

                    string emails = "";

                    for (int rowNum = 0; rowNum < dataTable.Rows.Count; rowNum++)
                    {
                        emails += dataTable.Rows[rowNum][2].ToString() + ",";

                    }
                    EntityEmail sendEmail = new EntityEmail();

                    sendEmail.To = emails;
                    sendEmail.Body = "you have a new Assign search By Email Reference : " + emailRef;
                    sendEmail.Subject = "NPO Tool";

                    MailHelper.SendMail(sendEmail);
                }
            }



        }

        private List<int> SubContainsCon(EmailMessage email)
        {

            ControllerRepository getConIdBySiteId = new ControllerRepository();

            List<int> controllerId = new List<int>();

            List<Controller> listControllers = listOfControllers.Where(controller => email.Subject.Contains(controller.ControllerName)).ToList();

            foreach (var item in listControllers)
            {
                controllerId.Add(item.ControllerId);
            }
            List<Site> listSites = listOfSites.Where(site => email.Subject.Contains(site.SiteName) | email.Subject.Contains(site.SiteCode)).ToList();

            foreach (var item in listSites)
            {
                List<int> GetControllerIdsBySiteId = getConIdBySiteId.GetControllerIdsBySiteId(item.SiteId);
                controllerId.AddRange(GetControllerIdsBySiteId);

            }


            if (controllerId.Count != 0)
            {
                return controllerId;
            }

            return new List<int> { -1 };
        }

        private List<int> BodyContainsCon(EmailMessage email)
        {


            ControllerRepository getConIdBySiteId = new ControllerRepository();

            List<int> controllerId = new List<int>();

            List<Controller> listControllers = listOfControllers.Where(controller => email.TextBody.ToString().Contains(controller.ControllerName)).ToList();

            foreach (var item in listControllers)
            {
                controllerId.Add(item.ControllerId);
            }
            List<Site> listSites = listOfSites.Where(site => email.TextBody.ToString().Contains(site.SiteName) | email.TextBody.ToString().Contains(site.SiteCode)).ToList();

            foreach (var item in listSites)
            {
                List<int> GetControllerIdsBySiteId = getConIdBySiteId.GetControllerIdsBySiteId(item.SiteId);
                controllerId.AddRange(GetControllerIdsBySiteId);

            }


            if (controllerId.Count != 0)
            {
                return controllerId;
            }

            return new List<int> { -1 };
        }


        #endregion


    }
}
