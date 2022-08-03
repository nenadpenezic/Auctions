using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.EmailClient
{
    public class MailkitClient : IEmailClient
    {
        public void SendEmail(string to, string body)
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("lila64@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject="Confirm email Address";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("tremaine.jones59@ethereal.email", "VXSBvdPVKGFPNVTZPJ"); //https://ethereal.email/
            smtp.Send(email);
            smtp.Disconnect(true);





        }
    }
}
