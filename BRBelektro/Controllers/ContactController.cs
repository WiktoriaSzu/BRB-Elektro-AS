using BRBelektro.Models;
using BRBelektro.Repository;
using BRBelektro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BRBelektro.Controllers;

[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
    private readonly InquiryRepository _repository;
    private readonly EmailService _emailService;
    private readonly ILogger<ContactController> _logger;

    public ContactController(InquiryRepository repository, EmailService emailService, ILogger<ContactController> logger)
    {
        _repository = repository;
        _emailService = emailService;
        _logger = logger;
    }

    [HttpPost]
    [EnableRateLimiting("contact-form")]
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
        catch (Exception ex)
        {
            // The inquiry is already saved, so the customer still gets a success
            // response. Log the failure so it can be followed up.
            _logger.LogError(ex, "Could not send contact form emails");
        }

        return Ok(new { message = "Sent successfully" });
    }
}
