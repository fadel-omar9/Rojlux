using System.ComponentModel.DataAnnotations;

namespace RojLuxWeb.Models;

public class KontaktFormular
{
    [Required(ErrorMessage = "Bitte geben Sie Ihren Namen ein.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte geben Sie eine gültige E-Mail ein.")]
    [EmailAddress(ErrorMessage = "Bitte geben Sie eine gültige E-Mail ein.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte geben Sie einen Betreff ein.")]
    [StringLength(200)]
    public string Betreff { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte geben Sie eine Nachricht ein.")]
    [StringLength(5000)]
    public string Nachricht { get; set; } = string.Empty;
}
