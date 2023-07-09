namespace Payments.Protocol.Http.Client;

public static class Uris
{
    public static class Payments_v1
    {
        private const string Version = "v1";
        
        public const string Create = $"/api/{Version}/Payments";
        public const string Read = $"/api/{Version}/Payments";
        public const string Update = $"/api/{Version}/Payments";
        public const string Delete = $"/api/{Version}/Payments";
    }
}