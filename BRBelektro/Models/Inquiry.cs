namespace BRBelektro.Models;

public class Inquiry
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Epost { get; set; }
    public string Telefon { get; set; }
    public DateOnly Dato { get; set; }
    public string Status { get; set; }
    public bool Betaling { get; set; }
}

