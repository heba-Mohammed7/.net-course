using System.Text.RegularExpressions;
using authentication.Dtos;
using authentication.Services.Interfaces;
using authentication.Settings;
using HandlebarsDotNet;
using MailKit.Security;
using MimeKit;
using IFileService = authentication.Services.Interfaces.IFileService;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace authentication.Services.Implementations;
public class EmailService : IEmailService
    {
        private readonly MailSetting _mailSetting;
        private readonly DataProtectionTokenProviderSetting _dataProtectionTokenProviderSetting;
        private readonly IFileService _fileService;
        private readonly ServerSetting _serverSetting;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            IOptions<MailSetting> mailSetting,
            IOptions<DataProtectionTokenProviderSetting> dataProtectionTokenProviderSetting,
            IFileService fileService,
            IOptions<ServerSetting> serverSetting,
            ILogger<EmailService> logger)
        {
            _mailSetting = mailSetting.Value;
            _dataProtectionTokenProviderSetting = dataProtectionTokenProviderSetting.Value;
            _fileService = fileService;
            _serverSetting = serverSetting.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(EmailModel emailModel, EmailSubject subject, HtmlTemplate htmlTemplate)
        {
            try
            {
                MimeMessage message = await BuildMimeMessage(emailModel, subject, htmlTemplate);

                using var client = new SmtpClient();
                await client.ConnectAsync(_mailSetting.SmtpServer, _mailSetting.Port, SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_mailSetting.UserName, _mailSetting.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                _logger.LogInformation("Email sent successfully to {ToMail}", emailModel.ToMail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {ToMail}", emailModel.ToMail);
                throw;
            }
        }

        private async Task<MimeMessage> BuildMimeMessage(EmailModel emailModel, EmailSubject subject, HtmlTemplate htmlTemplate)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailSetting.Name, _mailSetting.From));
            message.To.Add(new MailboxAddress(emailModel.ToName, emailModel.ToMail));
            message.Subject = SplitPascalCase(subject.ToString());

            string templateContent = await BuildTemplateAsync(htmlTemplate.ToString(), emailModel);

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = templateContent
            };
            message.Body = bodyBuilder.ToMessageBody();

            return message;
        }

        private async Task<string> BuildTemplateAsync(string templateName, EmailModel model)
        {
            if (model is ConfirmEmailModel emailModel)
            {
                return await BuildConfirmEmailTemplateAsync(templateName, emailModel);
            }

            if (model is ResetPasswordModel resetPasswordModel)
            {
                return await BuildResetPasswordTemplateAsync(templateName, resetPasswordModel);
            }

            return string.Empty;
        }

        private async Task<string> BuildConfirmEmailTemplateAsync(string templateName, ConfirmEmailModel model)
        {
            model.RedirectUrl = _serverSetting.FrontendBaseUrlForConfirmEmail;
            string templateContent = await GetTemplate(templateName);
            var template = Handlebars.Compile(templateContent);
            return template(model);
        }

        private async Task<string> BuildResetPasswordTemplateAsync(string templateName, ResetPasswordModel model)
        {
            string templateContent = await GetTemplate(templateName);
            var template = Handlebars.Compile(templateContent);
            return template(model);
        }
        private async Task<string> GetTemplate(string templateName)
        {
            string templatePath = _fileService.GetFilePath(Path.Combine("HtmlTemplates", templateName + ".html"));
            _logger.LogInformation("Attempting to load template from: {TemplatePath}", templatePath);
            if (!File.Exists(templatePath))
            {
                _logger.LogError("Template {TemplateName} not found at {TemplatePath}", templateName, templatePath);
                throw new FileNotFoundException($"Template {templateName} not found.");
            }

            await using FileStream fileStream = new(templatePath, FileMode.Open);
            using StreamReader reader = new(fileStream);
            return await reader.ReadToEndAsync();
        }

        private static string SplitPascalCase(string input)
        {
            return Regex.Replace(input, "(?<!^)([A-Z])", " $1");
        }
    }