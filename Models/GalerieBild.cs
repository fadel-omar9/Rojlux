namespace RojLuxWeb.Models;

public class GalerieBild
{
    public int Nummer { get; set; }
    public string Dateiname { get; set; } = string.Empty;
    public string Pfad { get; set; } = string.Empty;
    public string AltText { get; set; } = string.Empty;
    public string Kategorie { get; set; } = "kultur";

    /// <summary>
    /// Weist dem Bild basierend auf der Nummer eine Kategorie zu.
    /// Die Kategorien werden zyklisch verteilt.
    /// </summary>
    public static string GetKategorie(int nummer)
    {
        var kategorien = new[] { "kultur", "gemeinschaft", "kinder", "newroz" };
        return kategorien[(nummer - 1) % kategorien.Length];
    }
}
