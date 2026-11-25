using System.ComponentModel.DataAnnotations;

namespace RojLuxWeb.Models;

public class MitgliedFormular
{
    [Required(ErrorMessage = "Bitte geben Sie Ihren Vornamen ein.")]
    [StringLength(100)]
    public string Vorname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte geben Sie Ihren Nachnamen ein.")]
    [StringLength(100)]
    public string Nachname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte geben Sie eine gültige E-Mail-Adresse ein.")]
    [EmailAddress(ErrorMessage = "Bitte geben Sie eine gültige E-Mail-Adresse ein.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte geben Sie Ihre Telefonnummer ein.")]
    [Phone(ErrorMessage = "Bitte geben Sie eine gültige Telefonnummer ein.")]
    public string Telefon { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte geben Sie Ihre Adresse ein.")]
    [StringLength(200)]
    public string Strasse { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte geben Sie Ihre PLZ ein.")]
    [StringLength(20)]
    public string Plz { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte geben Sie Ihren Ort ein.")]
    [StringLength(100)]
    public string Ort { get; set; } = string.Empty;

    [StringLength(100)]
    public string Land { get; set; } = "Luxembourg";

    [StringLength(2000)]
    public string? Nachricht { get; set; }

    [Required(ErrorMessage = "Bitte bestätigen Sie Ihre Zustimmung.")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Bitte bestätigen Sie Ihre Zustimmung.")]
    public bool Zustimmung { get; set; }
}
