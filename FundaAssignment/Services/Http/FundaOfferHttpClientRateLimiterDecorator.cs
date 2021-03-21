using FundaAssignment.Models;
using FundaAssignment.Services.Throttling;
using System.Threading.Tasks;

namespace FundaAssignment.Services.Http
{
    public class FundaOfferHttpClientRateLimiterDecorator<T> : IFundaOfferHttpClient<T>
    {
        private readonly IFundaOfferHttpClient<T> decoratedClient;
        private readonly IRequestThrottling requestThrottling;

        public FundaOfferHttpClientRateLimiterDecorator(IFundaOfferHttpClient<T> decoratedClient, IRequestThrottling requestThrottling)
        {
            this.decoratedClient = decoratedClient;
            this.requestThrottling = requestThrottling;
        }

        public async Task<ApiCallResult<FundaApiResponse<T>>> GetOffer(string searchQuery)
        {
            await requestThrottling.Throttle();
            return await decoratedClient.GetOffer(searchQuery);
        }
    }
}