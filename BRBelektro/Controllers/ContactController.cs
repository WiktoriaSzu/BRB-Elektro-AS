using BRBelektro.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
    private readonly EmailService _emailService = new EmailService();

    private readonly InquiryRepository _repository;

    public ContactController(InquiryRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Send([FromBody] ContactRequest request)
    {
        await _repository.Create(new Inquiry
        {
            Name = request.Name.Trim(),
            Epost = request.Email.Trim(),
            Beskrivelse = request.Message.Trim(),
            Telefon = string.IsNullOrWhiteSpace(request.PhoneNumber) ? null : request.PhoneNumber.Trim(),
            Dato = DateOnly.FromDateTime(DateTime.Now),
            Status = "Ny",
            Betaling = false
        });

        try
        {
            await _emailService.SendEmails(request.Email, request.Name, request.Message);
        }
        catch (Exception)
        {
            // Something went wrong sending email. Keep going...
        }

        return Ok(new { message = "Sent successfully" });
    }
}
