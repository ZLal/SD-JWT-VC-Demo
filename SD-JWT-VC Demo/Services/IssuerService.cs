using Microsoft.Extensions.Options;
using SD_JWT_VC_Demo.Models;

// Ref - https://www.ietf.org/archive/id/draft-ietf-oauth-sd-jwt-vc-04.html

namespace SD_JWT_VC_Demo.Services
{
    public class IssuerService(IOptions<DemoOptions> demoOptions, UtilService utilService, JwtService jwtService)
    {
        public const int SaltLength = 20;
        public string PublicKey => demoOptions.Value.Issuer.PublicKey;

        public static Dictionary<string, string> CreateHolderPrivateData()
        {
            return new Dictionary<string, string>
            {
                { "Name", "Amith" },
                { "AgeAbove18", "true" },
                { "AgeAbove50", "false" },
                { "Illegal Drug user", "true" },
                { "Convicted", "false" },
            };
        }

        private List<ClaimsToProcess> CreateProcessedData(Dictionary<string, string> holderPrivateData)
        {
            var result = holderPrivateData.Select(x => new ClaimsToProcess
            {
                Key = x.Key,
                Value = x.Value,
                Salt = utilService.GetRandomString(SaltLength),
                Hash = string.Empty
            }).ToList();
            result.ForEach(x => x.Hash = utilService.CreateHash(x.GetSaltedString()));
            return result;
        }

        private Dictionary<string, string> GetClaims(List<ClaimsToProcess> processedPrivateData)
        {
            string hashStrings = string.Join(',', processedPrivateData.Select(x => x.Hash));
            hashStrings = $"[{hashStrings}]";
            // TODO Fix vct
            Dictionary<string, string> result = new()
            {
                { "vct", "https://credentials.example.com/identity_credential" },
                { JwtService.Claim_sd, hashStrings },
                { "_sd_alg", "sha-256" },
                { "cnf", """{ "jwk": { "kty": "EC", "crv": "P-256", "x": "TCAER19Zvu3OHF4j4W4vfSVoHIP1ILilDls7vCeGemc", "y": "ZxjiWWbZMQGHVWKVQ4hbSIirsVfuecCE6t4jT9F2HZQ" }""" },
            };
            return result;
        }

        public string CreateTokenWithDisclosedData(Dictionary<string, string> holderPrivateData)
        {
            var processedData = CreateProcessedData(holderPrivateData);
            Dictionary<string, string> claims = GetClaims(processedData);

            string token = jwtService.GenerateToken(demoOptions.Value.Issuer.PrivateKey,
                demoOptions.Value.Issuer.IssuerName, demoOptions.Value.Issuer.AudienceName,
                claims, demoOptions.Value.Issuer.ValidityInMinutes);

            string[] disclosures = [.. processedData.Select(x => x.GetHashedString())];
            disclosures = [.. disclosures.Select(utilService.ToBase64String)];
            string tokenWithDisclosedData = string.Join(JwtService.DisclosureSep, [token, .. disclosures]) + JwtService.DisclosureSep;

            return tokenWithDisclosedData;
        }
    }
}
