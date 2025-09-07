using authentication.Dtos;

namespace authentication.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(EmailModel emailModel, EmailSubject subject, HtmlTemplate htmlTemplate);
}