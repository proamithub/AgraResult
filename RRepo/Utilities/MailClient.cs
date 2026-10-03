using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using RRepo.Utilities;
using RModel;
using RDATA;

namespace RRepo
{
    public class MailClient : IMailClient
    {
        private string _baseUrl = string.Empty;
        private string _mailHost = string.Empty;
        private int _port = 587;
        private string _fromEmail = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;

        private string _GEmailFrom = string.Empty;
        private string _GEmailUsername = string.Empty;
        private string _GEmailPassword = string.Empty;
        private string _ReplyEmail = string.Empty;
        private string ImgCloudPath = string.Empty;
        private DataContext _dbContext;

        public MailClient(DataContext dbContext)
        {
            _dbContext = dbContext;
            _baseUrl = WebConfigSetting.BaseURL;
            _port = Convert.ToInt32(WebConfigSetting.PortNo);
            //_fromEmail = WebConfigSetting.FromEmail;
            //_username = WebConfigSetting.UserName;
            //_password = WebConfigSetting.Password;

            _GEmailFrom = WebConfigSetting.GEmailFrom;
            _GEmailUsername = WebConfigSetting.GEmailUsername;
            _GEmailPassword = WebConfigSetting.GEmailPassword;
            _ReplyEmail = WebConfigSetting.ReplyEmail;
        }
        private bool EmailHostConfigurationSendMail(MailMessage mm)
        {
            try
            {
                mm.From = new MailAddress(_fromEmail);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = _mailHost;
                smtp.EnableSsl = true;
                smtp.Port = _port;
                NetworkCredential NetworkCred = new NetworkCredential(_username, _password);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Send(mm);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //gmail - smtp settings
        private bool GmailHostConfigurationSendMail(MailMessage mm)
        {
            try
            {
                mm.From = new MailAddress(_GEmailFrom);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(_GEmailUsername, _GEmailPassword);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

  //      public bool SendMail_UserDetail(LoginMasterModel Model)
  //      {
  //          try
  //          {
  //              var templeteModel = @"<html><body><table style='width:100%;text-align:center'><tr><th> Name </th><th> Password </th></tr>     
  //     <tr>     
  //       <td>#name</td>
  //  <td>#pass</td>
  //</tr></table></body></html>";// _dbContext.EmailTemplates.Where(x => x.IsActive == true && x.EmailTemplateName.Contains("Subscribe")).FirstOrDefault();
  //              if (templeteModel != null)
  //              {
  //                  string _toEmail = Model.EmailId;
  //                 // string _mailSubject = templeteModel.EmailSubject;

  //                  //string _mailBody = templeteModel.EmailBody.Replace("#Year", DateTime.Now.Year.ToString());
  //                 // SendMail(_toEmail, _mailSubject, _mailBody);
  //                  return true;
  //              }
  //          }
  //          catch (Exception ex)
  //          { }
  //          return false;
  //      }
        

        public bool SendMail(string to, string subject, string body)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;

                //bool mailSentStatus = EmailHostConfigurationSendMail(mm);
                //if (mailSentStatus == true)
                //{
                //    return true;
                //}
                //else
                //{
                    bool gmailSentStatus = GmailHostConfigurationSendMail(mm);
                    if (gmailSentStatus == true)
                    {
                        return true;
                    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public bool SendMulpMail(string to, string subject, string body)
        {
            try
            {
                string[] email = to.Split(',');
                Array.Resize(ref email, email.Length - 1);
                MailMessage mm = new MailMessage();
                foreach (string eml in email)
                {
                    mm.To.Add(eml);
                }
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;

                bool mailSentStatus = EmailHostConfigurationSendMail(mm);
                if (mailSentStatus == true)
                {
                    return true;
                }
                else
                {
                    bool gmailSentStatus = GmailHostConfigurationSendMail(mm);
                    if (gmailSentStatus == true)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            { }
            return false;
        }
        
    }
}
