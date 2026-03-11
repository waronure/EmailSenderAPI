using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Email")]
public class EmailController : ControllerBase
{
    
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailController> _logger;

    public EmailController(IEmailService emailService, ILogger<EmailController> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    [HttpPost("SendEmailMessage")]
    public async Task<ActionResult> SendEmailMessage([FromBody] EmailMessage emailMessage)
    {
        try{
        await _emailService.SendMail(emailMessage.toEmail, emailMessage.subject, emailMessage.body);
        return StatusCode(200, new{message = "Email sent successfully"});
        }
        catch(SmtpException e)
        {
            return StatusCode(500, new{message = $"SMTP Error: {e.Message} (Status:{e.StatusCode}"});
        }

        catch(Exception e)
        {
            return StatusCode(500, new {message = $"General Error: {e.Message}"});
        }
    }
}