using CustomerManagement.Infrastrucure.Interface.Email;
using EASendMail;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Infrastrucure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string receipintEmail, string emailSubject, string emailBody)
        {
            try
            {
                EASendMail.SmtpMail oMail = new EASendMail.SmtpMail("TryIt");
                oMail.From = _configuration["EmailConfiguration:From"];
                oMail.To = receipintEmail;
                oMail.Subject = emailSubject;
                oMail.TextBody = emailBody;

                SmtpServer oServer = new SmtpServer(_configuration["EmailConfiguration:SmtpServer"]);
                oServer.User = _configuration["EmailConfiguration:UserName"];
                oServer.Password = _configuration["EmailConfiguration:Password"];
                oServer.Port = Convert.ToInt32(_configuration["EmailConfiguration:Port"]);
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                oSmtp.SendMail(oServer, oMail);

            }
            catch (Exception ep)
            {
                throw;
            }
        }

    }
}
