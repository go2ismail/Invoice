using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        //dependency injection
        private SendGridOptions _sendGridOptions { get; }
        private INetcoreService _netcoreService { get; }
        private SmtpOptions _smtpOptions { get; }

        public EmailSender(IOptions<SendGridOptions> sendGridOptions,
            INetcoreService netcoreService,
            IOptions<SmtpOptions> smtpOptions)
        {
            _sendGridOptions = sendGridOptions.Value;
            _netcoreService = netcoreService;
            _smtpOptions = smtpOptions.Value;
        }


        public Task SendEmailAsync(string email, string subject, string message)
        {
            //send email using sendgrid via netcoreService
            _netcoreService.SendEmailBySendGridAsync(_sendGridOptions.SendGridKey,
                _sendGridOptions.FromEmail,
                _sendGridOptions.FromFullName,
                subject,
                message,
                email).Wait();

            //send email using smtp via dotnetdesk. uncomment to use it
            /*
            _dotnetdesk.SendEmailByGmailAsync(_smtpOptions.fromEmail,
                _smtpOptions.fromFullName,
                subject,
                message,
                email,
                email,
                _smtpOptions.smtpUserName,
                _smtpOptions.smtpPassword,
                _smtpOptions.smtpHost,
                _smtpOptions.smtpPort,
                _smtpOptions.smtpSSL).Wait();
                */
            return Task.CompletedTask;
        }
    }
}
