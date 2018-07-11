using System.Security.Cryptography;

namespace Powerumc.AspNetCore.Hmac.Extensions
{
    internal static class EncryptExtension
    {
        public static byte[] EncryptAsHmacHash(this string str, byte[] key, byte[] buffer)
        {
            var hmac = HMAC.Create("HMACSHA256");
            hmac.Key = key;
            var hashedData = hmac.ComputeHash(buffer);

            return hashedData;
        }
    }
}