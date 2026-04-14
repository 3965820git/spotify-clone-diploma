using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using SpotifyClone.Shared.BuildingBlocks.Application.Email;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Shared.BuildingBlocks.Infrastructure.Messaging;

public sealed class SmtpEmailSender(IOptions<SmtpOptions> options) : IEmailSender
{
    private readonly SmtpOptions _options = options.Value ?? throw new ArgumentNullException(nameof(options));

    public async Task<Result> SendAsync(
        EmailMessage message,
        IEnumerable<EmailAttachment>? attachments = null,
        EmailPriority priority = EmailPriority.Normal,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        ArgumentNullException.ThrowIfNull(message);

        if (!message.To.Any())
        {
            throw new ArgumentException("At least one recipient is required.");
        }

        using var smtp = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.EnableSsl
        };

        if (!string.IsNullOrWhiteSpace(_options.UserName))
        {
            smtp.Credentials = new NetworkCredential(_options.UserName, _options.Password);
        }

        using var mail = new MailMessage
        {
            From = new MailAddress(
                message.From?.Email?? _options.DefaultFromEmail ?? throw new InvalidOperationException(
                    "No sender configured"),
                message.From?.Name ?? _options.DefaultFromName ?? string.Empty),
            Subject = message.Subject,
            Body = message.HtmlBody ?? message.PlainTextBody ?? string.Empty,
            IsBodyHtml = !string.IsNullOrWhiteSpace(message.HtmlBody),
            Priority = MapPriority(priority)
        };

        foreach (string recipient in message.To)
        {
            mail.To.Add(recipient);
        }

        if (message.Cc != null)
        {
            foreach (string cc in message.Cc)
            {
                mail.CC.Add(cc);
            }
        }

        if (message.Bcc != null)
        {
            foreach (string bcc in message.Bcc)
            {
                mail.Bcc.Add(bcc);
            }
        }

        if (!string.IsNullOrWhiteSpace(message.ReplyTo))
        {
            mail.ReplyToList.Add(message.ReplyTo);
        }

        if (attachments != null)
        {
            foreach (EmailAttachment attachment in attachments)
            {
                mail.Attachments.Add(new Attachment(attachment.Content, attachment.FileName, attachment.ContentType));
            }
        }

        try
        {
            await smtp.SendMailAsync(mail, cancellationToken);
        }
        catch (Exception)
        {
            return Result.Failure(EmailErrors.SendFailed);
        }

        return Result.Success();
    }

    private static MailPriority MapPriority(EmailPriority priority) => priority switch
    {
        EmailPriority.Low => MailPriority.Low,
        EmailPriority.Normal => MailPriority.Normal,
        EmailPriority.High => MailPriority.High,
        _ => MailPriority.Normal
    };
}
