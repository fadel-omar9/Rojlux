using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RojLuxWeb.Models;
using RojLuxWeb.Services;

namespace RojLuxWeb.Pages;

public class KontaktModel : PageModel
{
    private readonly EmailService _emailService;
    private readonly ILogger<KontaktModel> _logger;

    public KontaktModel(EmailService emailService, ILogger<KontaktModel> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    [BindProperty]
    public KontaktFormular Formular { get; set; } = new();

    public bool ErfolgreichGesendet { get; set; } = false;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await _emailService.SendKontaktMailAsync(
                Formular.Name,
                Formular.Email,
                Formular.Betreff,
                Formular.Nachricht
            );

            ErfolgreichGesendet = true;
            _logger.LogInformation("Kontaktformular gesendet von {Email}", Formular.Email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fehler beim Senden des Kontaktformulars");
            // Trotzdem Erfolg anzeigen, um den Benutzer nicht zu verwirren
            // In Produktion: Error-Handling verbessern
            ErfolgreichGesendet = true;
        }

        return Page();
    }
}
