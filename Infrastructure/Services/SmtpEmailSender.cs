using System.Net;
using System.Net.Mail;
using Application.Services;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class SmtpEmailSender: IEmailSender
{
    private readonly string _senderEmail;
    private readonly string _appPassword;
    
    private const string SmtpHost = "smtp.gmail.com";
    private const int SmtpPort = 587;
    private const bool EnableSsl = true;

    public SmtpEmailSender(IConfiguration configuration)
    {
        _senderEmail = configuration["EmailSenderOptions:SenderEmailAddress"] 
                       ?? throw new ArgumentNullException("SenderEmailAddress not configured");
        _appPassword = configuration["EmailSenderOptions:AppPassword"] 
                       ?? throw new ArgumentNullException("AppPassword not configured");
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        using var smtp = new SmtpClient(SmtpHost)
        {
            Port = SmtpPort,
            EnableSsl = EnableSsl,
            Credentials = new NetworkCredential(_senderEmail, _appPassword)
        };

        using var mail = new MailMessage
        {
            From = new MailAddress(_senderEmail),
            Subject = subject,
            Body = body,
        };

        mail.To.Add(toEmail);

        await smtp.SendMailAsync(mail);
    }
}