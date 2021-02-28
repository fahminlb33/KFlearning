using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using System.Net;

namespace KFlearning.Core.Extensions
{
    public static class ApiHelpers
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

        public static readonly JsonSerializerSettings SerializeSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
            },
        };

        public static void EnableTls()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12;
        }

        public static double ToUnixTime(this DateTime dateTime)
        {
            return dateTime.Subtract(UnixEpoch).TotalSeconds;
        }
    }
}
