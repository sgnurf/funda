namespace FundaAssignment.Configuration
{
    //TODO: Split or abstract  behind small interfaces
    public class FundaApiConfiguration
    {
        public string OfferApiBaseUrl { get; set; }

        public string Key { get; set; }

        public int MaxRequestsPerMinutes { get; set; }

        public int PageSize { get; set; }

        public int RequestCacheDurationInSeconds { get; set; }
    }
}