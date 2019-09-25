using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Services
{
    public class NullMailService : IEmailSender
    {
        private readonly ILogger<NullMailService> _logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation($"To: {email} Subject: {subject} Body: {htmlMessage}");
            return Task.CompletedTask;
        }

        //public void SendMessage(string to, string subject, string body)
        //{
        //    _logger.LogInformation($"To: {to} Subject: {subject} Body: {body}");
        //}
    }
}
