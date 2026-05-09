using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public class EmailService
{
    public async Task SendEmails(string customerEmail, string customerName, string message)
    {
        const string smptHost = "mailcluster.loopia.se";
        const int smtpPort = 587;
    
       string smtpUser = GetEnv("SMTP_USER");
       string smtpPassword = GetEnv("SMTP_PASSWORD");
       string fromEmail = GetEnv("SMTP_FROM_EMAIL");
       string companyEmail = GetEnv("COMPANY_EMAIL");

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
           $"Hei {customerName}, \n\n" +
           "Takk for at du kontaktet oss. Vi tar kontakt med deg i løpet av 1–2 dager.\n\n" +
           "Med vennlig hilsen,\n" +
            "Arkadiusz Czajka"
       };

        using var smtp = new SmtpClient { Timeout = 10000 };
        try
        {
            await smtp.ConnectAsync(smptHost, smtpPort, SecureSocketOptions.Auto);
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

    private static string GetEnv(string key)
    {
        var value = Environment.GetEnvironmentVariable(key);
        if (string.IsNullOrWhiteSpace(value))
            throw new Exception($"{key} environment variable has not been set yet.");
        return value;
    }
}