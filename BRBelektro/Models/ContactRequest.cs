using System.ComponentModel.DataAnnotations;

namespace BRBelektro.Models;

public class ContactRequest
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = "";

    [Required, EmailAddress, MaxLength(254)]
    public string Email { get; set; } = "";

    [Required, MaxLength(2000)]
    public string Message { get; set; } = "";

    // Optional. Digits and spaces, with an optional leading +.
    [RegularExpression(@"^\+?[0-9 ]{8,15}$")]
    public string? PhoneNumber { get; set; }
}
