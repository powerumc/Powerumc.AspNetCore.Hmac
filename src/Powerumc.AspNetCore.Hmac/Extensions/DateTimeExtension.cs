using System;

namespace Powerumc.AspNetCore.Hmac.Extensions
{
    internal static class DateTimeExtension
    {
        public static long ToTimeStamp(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        }
    }
}