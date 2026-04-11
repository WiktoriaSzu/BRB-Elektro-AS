using System.Net;
using System.Net.Mail;

public class EmailService
{
    public async Task SendEmails(string customerEmail, string customerName, string message)
    {
        var smtpClient = new SmtpClient("mailcluster.loopia.se", 587)
        {
            Credentials = new NetworkCredential("arek@brbelektro", "password"), 
            EnableSsl = true
        };

        // Notification of inquiry to company email
        var companyMail = new MailMessage
        {
            From = new MailAddress("arek@brbelektro"), 
            Subject = "Ny førespørsel",
            Body = $"Name: {customerName}\nEmail: {customerEmail}\nMessage: {message}"
        };

        companyMail.To.Add("arek@brbelektro");

        // Auto-reply to customers
        var autoReply = new MailMessage
        {
            From = new MailAddress("arek@brbelektro"), 
            Subject = "Vi har mottatt ditt melding!", 
            Body = $"Hei {customerName}, \n\nTakk for at du kontaktet oss. Vi tar kontant med deg i løpet av 1-2 dager. \n\nMed vennlig hilsen,\nArkadiusz Czajka."
        };

        autoReply.To.Add(customerEmail);
        await smtpClient.SendMailAsync(companyMail);
        await smtpClient.SendMailAsync(autoReply);
        }
    }