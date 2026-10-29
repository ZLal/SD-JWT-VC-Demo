using Microsoft.Extensions.Options;
using SD_JWT_VC_Demo.Models;

namespace SD_JWT_VC_Demo.Services
{
    public class HolderService(IOptions<DemoOptions> demoOptions, UtilService utilService, JwtService jwtService)
    {
        public string PublicKey => demoOptions.Value.Holder.PublicKey;

        public string CreateDisclosedToken(string issuerToken, DisclosedTokenRequest tokenRequest)
        {
            string[] requestedClaims = [.. tokenRequest.dcql_query.credentials.First().claims.SelectMany(x => x.path)];
            var tokenSplits = issuerToken.Split(JwtService.DisclosureSep);
            var decodedDisclosures = tokenSplits[1..].Select(x => new KeyValuePair<string, string>(x, utilService.FromBase64String(x)));
            var selectedDisclosures = decodedDisclosures.Where(x => requestedClaims.Any(y => x.Value.Contains($", {y},")));
            string issuerTokenWithDisclosures = string.Join(JwtService.DisclosureSep, [tokenSplits[0], ..selectedDisclosures.Select(x => x.Key).ToArray()]);
            string sd_hash = utilService.CreateHash(issuerTokenWithDisclosures);
            Dictionary<string, string> kbTokenClaims = new() {
                { JwtService.Claim_nonce, tokenRequest.nonce },
                { JwtService.Claim_sd_hash, sd_hash },
                };
            var holderOptions = demoOptions.Value.Holder;
            string kbToken = jwtService.GenerateToken(
                holderOptions.PrivateKey, holderOptions.IssuerName,
                holderOptions.AudienceName, kbTokenClaims,
                holderOptions.ValidityInMinutes);
            return issuerTokenWithDisclosures + JwtService.DisclosureSep + kbToken;
        }
    }
}
