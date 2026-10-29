using System.Security.Cryptography;

namespace SD_JWT_VC_Demo.Services
{
    public class RSAService(UtilService utilService)
    {
        private const string Const_BeginPublicKey = "-----BEGIN PUBLIC KEY-----";
        private const string Const_EndPublicKey = "-----END PUBLIC KEY-----";
        private const string Const_BeginPrivateKey = "-----BEGIN PRIVATE KEY-----";
        private const string Const_EndPrivateKey = "-----END PRIVATE KEY-----";

        // Key size: 2048 bits is secure for most purposes
        private const int keyLength = 2048;
        private static RSACryptoServiceProvider CreateCryptoService()
        {
            RSACryptoServiceProvider rsa = new(keyLength)
            {
                PersistKeyInCsp = false
            };
            return rsa;
        }

        /// <summary>
        /// Create XML public and private keys
        /// </summary>
        /// <returns>string PublicKey, string PrivateKey</returns>
        public (string, string) CreateNewKey_XML()
        {
            using var cryptoService = CreateCryptoService();

            string publicKey = cryptoService.ToXmlString(false); // false = public key only
            string privateKey = cryptoService.ToXmlString(true); // true = includes private key

            return (publicKey, privateKey);
        }

        /// <summary>
        /// Create PEM public and private keys
        /// </summary>
        /// <returns>string PublicKey, string PrivateKey</returns>
        public (string, string) CreateNewKey_PEM(bool formatForFile = false)
        {
            using var cryptoService = CreateCryptoService();

            string publicKey = ExportPublicKeyPem(cryptoService, formatForFile);
            string privateKey = ExportPrivateKeyPem(cryptoService, formatForFile);

            if (formatForFile)
            {
                publicKey = Const_BeginPublicKey + "\n" + publicKey + "\n" + Const_EndPublicKey;
                privateKey = Const_BeginPrivateKey + "\n" + privateKey + "\n" + Const_EndPrivateKey;
            }

            return (publicKey, privateKey);
        }

        // Export public key in PEM format
        private string ExportPublicKeyPem(RSACryptoServiceProvider rsa, bool includeLineBreaks)
        {
            var publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();
            string publicKey = utilService.ToBase64String(publicKeyBytes, includeLineBreaks);
            return publicKey;
        }

        // Export private key in PEM format
        private string ExportPrivateKeyPem(RSACryptoServiceProvider rsa, bool includeLineBreaks)
        {
            var privateKeyBytes = rsa.ExportPkcs8PrivateKey();
            string privateKey = utilService.ToBase64String(privateKeyBytes, includeLineBreaks);
            return privateKey;
        }
    }
}
