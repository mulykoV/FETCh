namespace FETCh.Configurations
{
    public class AppConfiguration
    {
        public string ApplicationName { get; set; } = string.Empty;
        public ConnectionStrings ConnectionStrings { get; set; } = new();
        public string ApiKey { get; set; } = string.Empty;
        public AdminSettings AdminSettings { get; set; } = new();
        public EmailSettings EmailSettings { get; set; } = new();
        public ProjectSettings ProjectSettings { get; set; } = new();
        public int MaxUsers { get; set; } = 100;
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; } = string.Empty;
        public string FETChDbContextConnection { get; set; } = string.Empty;
    }

    public class AdminSettings
    {
        public string SecretKey { get; set; } = string.Empty;
    }

    public class EmailSettings
    {
        public string SmtpHost { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPass { get; set; } = string.Empty;
    }

    public class ProjectSettings
    {
        public string Mode { get; set; } = string.Empty;
        public bool EnableNotifications { get; set; }
    }
}
