namespace SD_JWT_VC_Demo.Models
{
    public class DemoOptions
    {
        public Issuer Issuer { get; set; }
        public Holder Holder { get; set; }
    }

    public class Issuer
    {
        public string IssuerName { get; init; }
        public string AudienceName { get; init; }
        public int ValidityInMinutes { get; init; }
        public string PublicKey { get; init; }
        public string PrivateKey { get; init; }
    }

    public class Holder
    {
        public string IssuerName { get; init; }
        public string AudienceName { get; init; }
        public int ValidityInMinutes { get; init; }
        public string PublicKey { get; init; }
        public string PrivateKey { get; init; }
    }
}
