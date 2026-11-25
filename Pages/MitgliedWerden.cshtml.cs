using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RojLuxWeb.Models;
using RojLuxWeb.Services;

namespace RojLuxWeb.Pages;

public class MitgliedWerdenModel : PageModel
{
    private readonly EmailService _emailService;
    private readonly ILogger<MitgliedWerdenModel> _logger;

    public MitgliedWerdenModel(EmailService emailService, ILogger<MitgliedWerdenModel> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    [BindProperty]
    public MitgliedFormular Formular { get; set; } = new();

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
            await _emailService.SendMitgliedMailAsync(
                Formular.Vorname,
                Formular.Nachname,
                Formular.Email,
                Formular.Telefon,
                Formular.Strasse,
                Formular.Plz,
                Formular.Ort,
                Formular.Land,
                Formular.Nachricht
            );

            ErfolgreichGesendet = true;
            _logger.LogInformation("Mitgliedsantrag gesendet von {Email}", Formular.Email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fehler beim Senden des Mitgliedsantrags");
            ErfolgreichGesendet = true;
        }

        return Page();
    }
}
