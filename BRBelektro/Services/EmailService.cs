using System.Net;
using System.Net.Mail;

public class EmailService
{
    public async Task SendEmails(string customerEmail, string customerName, string message)
    {
        var smtpClient = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            Credentials = new NetworkCredential("test@example.com", "***REMOVED***"), 
            EnableSsl = true
        };

        // Notification of inquiry to company email
        var companyMail = new MailMessage
        {
            From = new MailAddress("test@example.com"), 
            Subject = "Ny førespørsel",
            Body = $"Name: {customerName}\nEmail: {customerEmail}\nMessage: {message}"
        };

        companyMail.To.Add("test@example.com");

        // Auto-reply to customers
        var autoReply = new MailMessage
        {
            From = new MailAddress("test@example.com"), 
            Subject = "Vi har mottatt ditt melding!", 
            Body = $"Hei {customerName}, \n\nTakk for at du kontaktet oss. Vi tar kontant med deg i løpet av 1-2 dager. \n\nMed vennlig hilsen,\nArkadiusz Czajka."
        };

        autoReply.To.Add(customerEmail);
        await smtpClient.SendMailAsync(companyMail);
        await smtpClient.SendMailAsync(autoReply);
        }
    }