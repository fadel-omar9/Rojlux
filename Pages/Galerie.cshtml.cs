using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RojLuxWeb.Models;

namespace RojLuxWeb.Pages;

public class GalerieModel : PageModel
{
    private readonly IWebHostEnvironment _env;

    public GalerieModel(IWebHostEnvironment env)
    {
        _env = env;
    }

    public List<GalerieBild> Bilder { get; set; } = new();

    public void OnGet()
    {
        var imageDir = Path.Combine(_env.WebRootPath, "images");

        if (!Directory.Exists(imageDir))
            return;

        // Alle "bild (X).jpg" Dateien finden und sortiert laden
        var bildDateien = Directory.GetFiles(imageDir, "bild*.*")
            .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".webp", StringComparison.OrdinalIgnoreCase))
            .ToList();

        foreach (var datei in bildDateien)
        {
            var dateiname = Path.GetFileName(datei);

            // Nummer aus dem Dateinamen extrahieren, z.B. "bild (5).jpg" → 5
            var match = Regex.Match(dateiname, @"\((\d+)\)");
            var nummer = match.Success ? int.Parse(match.Groups[1].Value) : 0;

            Bilder.Add(new GalerieBild
            {
                Nummer = nummer,
                Dateiname = dateiname,
                Pfad = $"/images/{dateiname}",
                AltText = $"Roj Lux Impression {nummer}",
                Kategorie = GalerieBild.GetKategorie(nummer)
            });
        }

        Bilder = Bilder.OrderBy(b => b.Nummer).ToList();
    }
}
