namespace Repleet.Contracts
{
    public class AppConfiguration
    {
        public AppSettings AppSettings { get; set; }
        public CorsSettings Cors { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class AppSettings
    {
        public string Token { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

    public class CorsSettings
    {
        public string[] AllowedOrigins { get; set; }
    }

    public class ConnectionStrings
    {
        public string PostgreSQLConnection { get; set; }
    }

}
