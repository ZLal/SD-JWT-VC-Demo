using Microsoft.Extensions.Options;
using SD_JWT_VC_Demo.Models;
using System.Text.Json;

namespace SD_JWT_VC_Demo.Services
{
    public class VerifierService(IOptions<DemoOptions> demoOptions, UtilService utilService, JwtService jwtService)
    {
        public DisclosedTokenRequest CreateDisclosedTokenRequest()
        {
            string nonce = utilService.GetRandomString(12);
            string jsonTokenRequest = """
                {
                  "client_id": "https://verifier.example.com",
                  "nonce": "{nonce}",
                  "dcql_query": {
                    "credentials": [{
                      "id": "id_cred",
                      "format": "dc+sd-jwt",
                      "meta": { "vct_values": ["https://credentials.example.gov/identity"] },
                      "claims": [ { "path": ["{claim1}"] }, { "path": ["{claim2}"] } ]
                    }]
                  }
                }
                """;
            jsonTokenRequest = jsonTokenRequest
                .Replace("{nonce}", nonce)
                .Replace("{claim1}", "Name")
                .Replace("{claim2}", "AgeAbove18");
            DisclosedTokenRequest tokenRequest = JsonSerializer.Deserialize<DisclosedTokenRequest>(jsonTokenRequest)
                ?? throw new Exception("Failed to deserialize token request");
            return tokenRequest;
        }

        public ClaimsToProcess[] GetDisclosedClaims(string[] claimValues)
        {
            ClaimsToProcess[] result;
            result = claimValues.Select(utilService.FromBase64String)
                .Select(x =>
                {
                    string[] splits = x.Replace("[", string.Empty)
                        .Replace("]", string.Empty)
                        .Split(',')
                        .ToArray();
                    if (splits.Length != 3)
                        throw new Exception("Invalid disclosure format");
                    return new ClaimsToProcess()
                    {
                        Hash = splits[0].Trim(),
                        Key = splits[1].Trim(),
                        Value = splits[2].Trim()
                    };
                }).ToArray();
            return result;
        }

        public Dictionary<string, string> ValidateTokenAndGetClaims(string holderToken, string nonce)
        {
            var tokenSplits = holderToken.Split(JwtService.DisclosureSep);
            if (tokenSplits.Length < 3)
                throw new Exception("Holder token must contain atleaset one disclosure");
            
            // Validate issuer token
            var issuerOptions = demoOptions.Value.Issuer;
            var issuerClaims = jwtService.ValidateTokenAndGetClaims(issuerOptions.PublicKey,
                tokenSplits.First(), issuerOptions.IssuerName, issuerOptions.AudienceName);

            // Validate holder token / kb token
            var holderOptions = demoOptions.Value.Holder;
            var holderClaims = jwtService.ValidateTokenAndGetClaims(holderOptions.PublicKey,
                tokenSplits.Last(), holderOptions.IssuerName, holderOptions.AudienceName);

            // Validate nonce
            if (!holderClaims.TryGetValue(JwtService.Claim_nonce, out string? claim_nonce) || claim_nonce != nonce)
                throw new Exception($"{JwtService.Claim_nonce} mismatch");

            // Validate sd_hash
            string issuerTokenWithDisclosures = string.Join(JwtService.DisclosureSep, tokenSplits[0..^1]);
            string sd_hash = utilService.CreateHash(issuerTokenWithDisclosures);
            if (!holderClaims.TryGetValue(JwtService.Claim_sd_hash, out string? claim_sd_hash) || claim_sd_hash != sd_hash)
                throw new Exception($"{JwtService.Claim_sd_hash} mismatch");
            
            // Validate sd claim hash
            ClaimsToProcess[] holderDisclosedClaims = GetDisclosedClaims(tokenSplits[1..^1]);
            if (!issuerClaims.TryGetValue(JwtService.Claim_sd, out string? issuer_sd_hash) || string.IsNullOrWhiteSpace(issuer_sd_hash))
                throw new Exception($"{JwtService.Claim_sd} mismatch");
            string[] issuer_sd_hashs = issuer_sd_hash.Replace("[", string.Empty).Replace("]", string.Empty).Split(',');
            if (holderDisclosedClaims.Length > issuer_sd_hashs.Length)
                throw new Exception($"{JwtService.Claim_sd} count mismatch");
            if (holderDisclosedClaims.Any(x => !issuer_sd_hashs.Any(y => y == x.Hash)))
                throw new Exception($"{JwtService.Claim_sd} hash mismatch");
            
            return holderDisclosedClaims.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
