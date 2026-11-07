using System.ComponentModel.DataAnnotations.Schema;

namespace BRBelektro.Models;

[Table("inquiry")]
public class Inquiry
{
    [Column("id")]
    public int Id { get; set; }

    [Column("navn")]
    public string? Name { get; set; }

    [Column("epost")]
    public string? Epost { get; set; }

    // Stored as text so numbers like "+47 463 93 906" are kept as written.
    [Column("telefon")]
    public string? Telefon { get; set; }

    [Column("dato")]
    public DateOnly? Dato { get; set; }

    [Column("status")]
    public string? Status { get; set; }

    [Column("betaling")]
    public bool? Betaling { get; set; }

    [Column("beskrivelse")]
    public string? Beskrivelse { get; set; }
}
