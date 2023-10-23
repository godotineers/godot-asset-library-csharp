

using GodotAssetLibrary.Contracts;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GodotAssetLibrary.Infrastructure.Mail
{
    public class MailUtility : IMailUtility
    {
        public MailUtility(
                    IOptions<MailOptions> options)
        {
            Options = options;
        }

        public IOptions<MailOptions> Options { get; }

        public async Task<bool> Send(string username, string email, string subject, string body, string altBody, CancellationToken cancellationToken = default)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Godot Asset Library", this.Options.Value.From));
            if (!string.IsNullOrEmpty(this.Options.Value.ReplyTo))
                message.ReplyTo.Add(new MailboxAddress(this.Options.Value.ReplyTo, this.Options.Value.ReplyTo));

            message.To.Add(new MailboxAddress(username, email));
            message.Subject = subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            builder.TextBody = altBody;

            message.Body = builder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(this.Options.Value.Smtp.Host, 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    if (this.Options.Value.Smtp.Secure)
                        await client.AuthenticateAsync(this.Options.Value.Smtp.Auth.User, this.Options.Value.Smtp.Auth.Pass);


                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
