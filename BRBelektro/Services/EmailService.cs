using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BRBelektro.Services;

// Sends a notification to the company and an automatic reply to the customer
// when the contact form is submitted. SMTP settings come from configuration
// (environment variables or user secrets), see README.
public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmails(string customerEmail, string customerName, string message)
    {
        string smtpHost = _config["SMTP_HOST"] ?? "mailcluster.loopia.se";
        int smtpPort = int.TryParse(_config["SMTP_PORT"], out var port) ? port : 587;
        string smtpUser = GetRequired("SMTP_USER");
        string smtpPassword = GetRequired("SMTP_PASSWORD");
        string fromEmail = GetRequired("SMTP_FROM_EMAIL");
        string companyEmail = GetRequired("COMPANY_EMAIL");

        var companyMail = new MimeMessage();
        companyMail.From.Add(MailboxAddress.Parse(fromEmail));
        companyMail.To.Add(MailboxAddress.Parse(companyEmail));
        companyMail.Subject = "Ny forespørsel";
        companyMail.Body = new TextPart("plain")
        {
            Text = $"Navn: {customerName}\nE-post: {customerEmail}\nMelding: {message}"
        };
        if (MailboxAddress.TryParse(customerEmail, out var replyTo))
            companyMail.ReplyTo.Add(replyTo);

        var autoReply = new MimeMessage();
        autoReply.From.Add(MailboxAddress.Parse(fromEmail));
        autoReply.To.Add(MailboxAddress.Parse(customerEmail));
        autoReply.Subject = "Vi har mottatt meldingen din";
        autoReply.Body = new TextPart("plain")
        {
            Text =
                $"Hei {customerName},\n\n" +
                "Takk for at du kontaktet oss. Vi tar kontakt med deg i løpet av 1–2 dager.\n\n" +
                "Med vennlig hilsen,\n" +
                "Arkadiusz Czajka"
        };

        using var smtp = new SmtpClient { Timeout = 10000 };
        try
        {
            await smtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.Auto);
            await smtp.AuthenticateAsync(smtpUser, smtpPassword);
            await smtp.SendAsync(companyMail);
            await smtp.SendAsync(autoReply);
        }
        finally
        {
            if (smtp.IsConnected)
                await smtp.DisconnectAsync(true);
        }
    }

    private string GetRequired(string key)
    {
        var value = _config[key];
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException($"{key} has not been configured.");
        return value;
    }
}
