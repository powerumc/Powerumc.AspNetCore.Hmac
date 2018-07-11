using System;

namespace Powerumc.AspNetCore.Hmac.Extensions
{
    internal static class Guard
    {
        public static void ThrowIfNullOrWhitespace(string str, string message)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(message);
        }

        public static void ThrowIfNull(object obj, string message)
        {
            if (obj == null)
                throw new ArgumentNullException(message);
        }

        public static void ThrowIfZeroOrLess(long num, string message)
        {
            if (num <= 0)
                throw new ArgumentException(message);
        }

        public static void ThrowIf<T>(this T obj, Predicate<T> predicate, string message)
        {
            if (predicate(obj))
                throw new ArgumentException(message);
        }
    }
}