using System;
using System.Text;

namespace Powerumc.AspNetCore.Hmac.Extensions
{
    internal static class StringExtension
    {
        public static string DecodeBase64(this string str)
        {
            Guard.ThrowIfNull(str, nameof(str));
            
            var data = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(data);
        }

        public static string EncodeBase64(this string str)
        {
            Guard.ThrowIfNull(str, nameof(str));
            
            var data = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(data);
        }

        public static byte[] ToBytes(this string str, Encoding encoding = null)
        {
            Guard.ThrowIfNull(str, nameof(str));
            
            if (encoding == null)
                encoding = Encoding.UTF8;
            
            return encoding.GetBytes(str);
        }
    }
}