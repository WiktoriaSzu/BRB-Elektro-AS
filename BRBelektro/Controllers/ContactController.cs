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
            Name = request.Name,
            Epost = request.Email,
            Beskrivelse = request.Message,
            Telefon = request.PhoneNumber
        });

        try 
        {
            await _emailService.SendEmails(request.Email, request.Name, request.Message);
        } 
        catch (Exception)
        {
            // Something went wrong sending email. Keep going... 
        }
        
        return Ok(new {message = "Sent successfully"});
    }
}

public class ContactRequest
{
   public string Name {get; set;} = "";
   public string Email {get; set;} = "";
   public string Message {get; set;}  = "";
   public int PhoneNumber {get; set;}
}