namespace authentication.Dtos;

public record ConfirmEmailModel(
    string ToName,
    string ToMail,
    string Code,
    string Token,
    int ExpiredInMinutes)
    : EmailModel(ToName, ToMail, HtmlTemplate.ConfirmEmail)
{
    public string? RedirectUrl { get; set; }
}