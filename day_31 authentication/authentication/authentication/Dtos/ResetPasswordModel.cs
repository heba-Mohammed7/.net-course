namespace authentication.Dtos;

public record ResetPasswordModel(
    string ToName,
    string ToMail,
    string Token,
    string Code,
    int ExpiredInMinutes
) : EmailModel(ToName, ToMail, HtmlTemplate.ResetPassword)
{
    public string? ResetUrl { get; set; }
    public string? SessionId { get; set; }
}