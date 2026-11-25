using System.Net;
using System.Net.Mail;

namespace RojLuxWeb.Services;

public class EmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration config, ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;
    }

    /// <summary>
    /// Sendet eine Kontakt-E-Mail an den Verein.
    /// </summary>
    public async Task SendKontaktMailAsync(string name, string email, string betreff, string nachricht)
    {
        var subject = $"[Kontaktformular] {betreff}";
        var body = $@"
<h2>Neue Kontaktanfrage über die Website</h2>
<hr>
<p><strong>Name:</strong> {name}</p>
<p><strong>E-Mail:</strong> {email}</p>
<p><strong>Betreff:</strong> {betreff}</p>
<hr>
<h3>Nachricht:</h3>
<p>{nachricht.Replace("\n", "<br>")}</p>
<hr>
<p style='color: #666; font-size: 12px;'>Diese Nachricht wurde über das Kontaktformular auf rojlux.lu gesendet.</p>";

        await SendMailAsync(subject, body, email);
    }

    /// <summary>
    /// Sendet eine Mitgliedsantrag-E-Mail an den Vorstand.
    /// </summary>
    public async Task SendMitgliedMailAsync(string vorname, string nachname, string email, 
        string telefon, string strasse, string plz, string ort, string land, string? nachricht)
    {
        var subject = $"[Mitgliedsantrag] {vorname} {nachname}";
        var body = $@"
<h2>Neuer Mitgliedsantrag über die Website</h2>
<hr>
<h3>Persönliche Daten</h3>
<p><strong>Vorname:</strong> {vorname}</p>
<p><strong>Nachname:</strong> {nachname}</p>
<p><strong>E-Mail:</strong> {email}</p>
<p><strong>Telefon:</strong> {telefon}</p>
<hr>
<h3>Adresse</h3>
<p><strong>Straße:</strong> {strasse}</p>
<p><strong>PLZ:</strong> {plz}</p>
<p><strong>Ort:</strong> {ort}</p>
<p><strong>Land:</strong> {land}</p>
<hr>
{(string.IsNullOrWhiteSpace(nachricht) ? "" : $"<h3>Nachricht:</h3><p>{nachricht.Replace("\n", "<br>")}</p><hr>")}
<p style='color: #666; font-size: 12px;'>Dieser Antrag wurde über das Mitgliedsformular auf rojlux.lu gesendet.</p>";

        await SendMailAsync(subject, body, email);
    }

    private async Task SendMailAsync(string subject, string htmlBody, string replyToEmail)
    {
        var settings = _config.GetSection("EmailSettings");
        var smtpHost = settings["SmtpHost"] ?? "smtp.gmail.com";
        var smtpPort = int.Parse(settings["SmtpPort"] ?? "587");
        var senderEmail = settings["SenderEmail"] ?? "";
        var senderName = settings["SenderName"] ?? "Roj Lux ASBL";
        var senderPassword = settings["SenderPassword"] ?? "";
        var recipientEmail = settings["RecipientEmail"] ?? senderEmail;

        try
        {
            using var message = new MailMessage();
            message.From = new MailAddress(senderEmail, senderName);
            message.To.Add(recipientEmail);
            message.ReplyToList.Add(new MailAddress(replyToEmail));
            message.Subject = subject;
            message.Body = htmlBody;
            message.IsBodyHtml = true;

            using var client = new SmtpClient(smtpHost, smtpPort);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(senderEmail, senderPassword);

            await client.SendMailAsync(message);
            _logger.LogInformation("E-Mail erfolgreich gesendet: {Subject}", subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fehler beim Senden der E-Mail: {Subject}", subject);
            throw;
        }
    }
}
