using FundaAssignment.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace FundaAssignment.Services.Http
{
    public class FundaOfferHttpClientCachingDecorator : IFundaOfferHttpClient
    {
        private readonly IFundaOfferHttpClient decoratedClient;
        private readonly IMemoryCache memoryCache;

        public FundaOfferHttpClientCachingDecorator(IFundaOfferHttpClient decoratedClient, IMemoryCache memoryCache)
        {
            this.decoratedClient = decoratedClient;
            this.memoryCache = memoryCache;
        }

        public async Task<ApiResponse<OfferResponse>> GetOffer(string searchQuery)
        {
            if (memoryCache.TryGetValue(searchQuery, out ApiResponse<OfferResponse> offerResponse))
            {
                return offerResponse;
            }

            offerResponse = await decoratedClient.GetOffer(searchQuery);
            //TODO: Move cache time to configuration
            memoryCache.Set(searchQuery, offerResponse, TimeSpan.FromSeconds(15));

            return offerResponse;
        }
    }
}