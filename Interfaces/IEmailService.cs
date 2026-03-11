public interface IEmailService
{
    Task SendMail(string toEmail, string subject, string body);
}