using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _settings;

    public EmailService (IOptions<SmtpSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendMail(string toEmail, string subject, string body)
    {
        using SmtpClient client = ConfigureSmtpClient();
        MailMessage emailMessage = CreateEmailMessage(subject, body);

        emailMessage.To.Add(toEmail);
        await client.SendMailAsync(emailMessage);
    }

    private MailMessage CreateEmailMessage(string subject, string body)
    {
        return new MailMessage
        {
            From = new MailAddress(_settings.FromEmail, _settings.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
    }

    private SmtpClient ConfigureSmtpClient()
    {
        return new SmtpClient(_settings.Server, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.FromEmail, _settings.Password)
        };
    }
}