using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SD_JWT_VC_Demo.Services
{
    public class JwtService
    {
        public const char DisclosureSep = '~';
        public const string Claim_sd = "_sd";
        public const string Claim_nonce = "nonce";
        public const string Claim_sd_hash = "sd_hash";

        public string GenerateToken(string privateKey, string issuer, string audience, Dictionary<string, string> claims, double validityInMinutes)
        {
            var tokenClaims = claims.Select(x => new Claim(x.Key, x.Value));
            
            RSA rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
            var key = new RsaSecurityKey(rsa);
            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: tokenClaims,
                expires: DateTime.Now.AddMinutes(validityInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Dictionary<string, string> ValidateTokenAndGetClaims(string publicKey, string token, string issuer, string audience)
        {
            RSA rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKey), out _);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(rsa),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                RequireSignedTokens = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;

            var result = jwtToken.Claims.ToDictionary(x => x.Type, x => x.Value);
            return result;
        }
    }
}
