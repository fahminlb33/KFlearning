using System;

namespace KFlearning.Core.Helpers
{
    public static class DateTimeHelpers
    {
        private static readonly DateTime UnixEpoch = new(1970, 1, 1);

        public static double ToUnixTime(this DateTime dateTime)
        {
            return dateTime.Subtract(UnixEpoch).TotalSeconds;
        }
    }
}
