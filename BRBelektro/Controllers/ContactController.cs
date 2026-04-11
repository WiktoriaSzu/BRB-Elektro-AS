using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
    private readonly EmailService _emailService = new EmailService();

    [HttpPost]
    public async Task<IActionResult> Send([FromBody] ContactRequest request)
    {
        await _emailService.SendEmails(request.Email, request.Name, request.Message);
        return Ok(new {message = "Sent successfully"});  
    }
}

public class ContactRequest
{
   public string Name {get; set;} = "";
   public string Email {get; set;} = "";
   public string Message {get; set;}  = "";
}