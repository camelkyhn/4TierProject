using System;
using System.Web;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Mime;
using _4TierProject.Common.Helpers;

namespace _4TierProject.BLL.MessageServices
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // E-posta göndermek için e-posta hizmetinizi buraya bağlayın.
            //return Task.FromResult(0);
            return Task.Factory.StartNew(() =>
            {
                SendLiveMail(message);
            });
        }

        void SendLiveMail(IdentityMessage message)
        {
            #region formatter
            string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
            string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
            #endregion

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(ConfigHelper.Get<string>("Email"));
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("smtp.live.com", Convert.ToInt32(587)); //smtp.live.com means its a microsoft mail
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(ConfigHelper.Get<string>("Email"), ConfigHelper.Get<string>("Password"));
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}
