using BRBelektro.Models;
using BRBelektro.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BRBelektro.Controllers;

// Admin API for customer inquiries. Only signed-in Google accounts listed in
// Admin:AllowedEmails can use it.
[ApiController]
[Authorize(Policy = "AllowedEmails")]
[Route("api/[controller]")]
public class InquiryController : ControllerBase
{
    private readonly InquiryRepository _repository;

    public InquiryController(InquiryRepository repository)
    {
        _repository = repository;
    }

    // GET: api/inquiry
    [HttpGet]
    public async Task<ActionResult<List<Inquiry>>> GetAll()
    {
        return Ok(await _repository.GetAll());
    }

    // GET: api/inquiry/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Inquiry>> GetById(int id)
    {
        var inquiry = await _repository.GetById(id);

        if (inquiry == null)
            return NotFound();

        return Ok(inquiry);
    }

    // POST: api/inquiry
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Inquiry inquiry)
    {
        await _repository.Create(inquiry);
        return CreatedAtAction(nameof(GetById), new { id = inquiry.Id }, inquiry);
    }

    // PUT: api/inquiry/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Inquiry inquiry)
    {
        var found = await _repository.Update(id, inquiry);

        if (!found)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/inquiry/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.Delete(id);
        return NoContent();
    }
}
