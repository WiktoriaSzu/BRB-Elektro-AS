using System;
using System.Collections.Generic;
using System.Linq;
using BRBelektro.Data;
using BRBelektro.Models;
using Microsoft.EntityFrameworkCore;

    
using Microsoft.AspNetCore.Mvc;

[ApiController]
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
        var inquiries = await _repository.GetAll();
        return Ok(inquiries);
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
        var result = await _repository.Update(id, inquiry);

        if (result == 0)
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