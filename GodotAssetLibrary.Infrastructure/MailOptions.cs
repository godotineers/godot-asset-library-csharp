

namespace GodotAssetLibrary.Infrastructure
{
    public class MailOptions
    {
        public string From { get; set; }
        public string? ReplyTo { get; set; }

        public SmtpSettings Smtp { get; set; } = new SmtpSettings();

        public class SmtpSettings
        {
            public string Host { get; set; }

            public int Port { get; set; }

            public bool Secure { get; set; } = false;

            public SmtpAuthSettings? Auth { get; set; }
        }

        public class SmtpAuthSettings
        {
            public string User { get; set; }
            public string Pass { get; set; }
        }
    }
}
