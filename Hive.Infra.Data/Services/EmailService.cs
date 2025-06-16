using Hive.Application.Interfaces;
using Hive.Infra.Data.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Hive.Infra.Data.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> options, ILogger<EmailService> logger)
        {
            _logger = logger;
            _smtpSettings = options.Value;
        }

        public async Task SendEmail(string to, string subject, string htmlContent)
        {
            try
            {
                if (string.IsNullOrEmpty(_smtpSettings.FromEmail) || string.IsNullOrEmpty(_smtpSettings.Username))
                {
                    _logger.LogError("Configurações de SMTP não foram carregadas corretamente.");
                    return;
                }

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = htmlContent };
                email.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.SslOnConnect);

                    await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);

                    await client.SendAsync(email);

                    await client.DisconnectAsync(true);
                }

                _logger.LogInformation("E-mail SMTP enviado com sucesso para {To}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado ao enviar o e-mail via SMTP.");
            }
        }
    }
}
