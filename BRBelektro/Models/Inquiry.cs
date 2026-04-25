namespace BRBelektro.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("inquiry")]
public class Inquiry
{
    [Column("id")]
    public int Id { get; set; }
    [Column("navn")]
    public string? Name { get; set; }
    [Column("epost")]
    public string? Epost { get; set; }
    [Column("telefon")]
    public int? Telefon { get; set; }
    [Column("dato")]
    public DateOnly? Dato { get; set; }
     [Column("status")]
    public string? Status { get; set; }
    [Column("betaling")]
    public bool? Betaling { get; set; }
    [Column("beskrivelse")]
    public string? Beskrivelse { get; set; }
}

