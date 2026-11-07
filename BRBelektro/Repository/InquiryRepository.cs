using BRBelektro.Data;
using BRBelektro.Models;
using Microsoft.EntityFrameworkCore;

namespace BRBelektro.Repository;

public class InquiryRepository
{
    private readonly AppDbContext _context;

    public InquiryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Inquiry>> GetAll()
    {
        return await _context.Inquiries.OrderByDescending(i => i.Id).ToListAsync();
    }

    public async Task<Inquiry?> GetById(int id)
    {
        return await _context.Inquiries.FindAsync(id);
    }

    public async Task Create(Inquiry inquiry)
    {
        _context.Inquiries.Add(inquiry);
        await _context.SaveChangesAsync();
    }

    // Returns false when no inquiry has the given id.
    public async Task<bool> Update(int id, Inquiry updated)
    {
        var existing = await _context.Inquiries.FindAsync(id);
        if (existing == null) return false;

        existing.Name = updated.Name;
        existing.Epost = updated.Epost;
        existing.Telefon = updated.Telefon;
        existing.Dato = updated.Dato;
        existing.Status = updated.Status;
        existing.Betaling = updated.Betaling;
        existing.Beskrivelse = updated.Beskrivelse;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task Delete(int id)
    {
        var inquiry = await _context.Inquiries.FindAsync(id);
        if (inquiry != null)
        {
            _context.Inquiries.Remove(inquiry);
            await _context.SaveChangesAsync();
        }
    }
}
