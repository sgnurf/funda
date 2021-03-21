using FundaAssignment.Configuration;
using FundaAssignment.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace FundaAssignment.Services.Http
{
    public class FundaOfferHttpClientCachingDecorator<T> : IFundaOfferHttpClient<T>
    {
        private readonly IFundaOfferHttpClient<T> decoratedClient;
        private readonly IMemoryCache memoryCache;
        private readonly int cachingTimeInSeconds;

        public FundaOfferHttpClientCachingDecorator(IFundaOfferHttpClient<T> decoratedClient, IMemoryCache memoryCache, IOptions<FundaApiConfiguration> options)
        {
            this.decoratedClient = decoratedClient;
            this.memoryCache = memoryCache;
            cachingTimeInSeconds = options.Value.RequestCacheDurationInSeconds;
        }

        public async Task<ApiCallResult<FundaApiResponse<T>>> GetOffer(string searchQuery)
        {
            if (memoryCache.TryGetValue(searchQuery, out ApiCallResult<FundaApiResponse<T>> offerResponse))
            {
                return offerResponse;
            }

            offerResponse = await decoratedClient.GetOffer(searchQuery);
            //TODO: Move cache time to configuration
            memoryCache.Set(searchQuery, offerResponse, TimeSpan.FromSeconds(cachingTimeInSeconds));

            return offerResponse;
        }
    }
}