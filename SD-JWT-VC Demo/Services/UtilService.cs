using System.Security.Cryptography;
using System.Text;

namespace SD_JWT_VC_Demo.Services
{
    public class UtilService
    {
        public string GetRandomString(int length)
        {
            char[] choices = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_-".ToCharArray();
            string randomString = Random.Shared.GetString(choices, length);
            return randomString;
        }

        public string CreateHash(string value)
        {
            byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
            string hashString = Convert.ToHexStringLower(hashBytes);
            return hashString;
        }

        public string ToBase64String(byte[] bytes, bool includeLineBreaks)
        {
            Base64FormattingOptions base64FormattingOptions = includeLineBreaks
                ? Base64FormattingOptions.InsertLineBreaks
                : Base64FormattingOptions.None;
            string base64String = Convert.ToBase64String(bytes, base64FormattingOptions);
            return base64String;
        }

        public string ToBase64String(string value)
        {
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(value);
            string base64String = Convert.ToBase64String(utf8Bytes);
            return base64String;
        }

        public string FromBase64String(string base64Value)
        {
            byte[] utf8Bytes = Convert.FromBase64String(base64Value);
            string decodedString = Encoding.UTF8.GetString(utf8Bytes);
            return decodedString;
        }
    }
}
