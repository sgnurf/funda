using Microsoft.Extensions.DependencyInjection;
using System;

namespace FundaAssignment.Services.Http
{
    public class FundaOfferHttpClientFactory<T> : IFundaOfferHttpClientFactory<T>
    {
        private readonly IServiceProvider serviceProvider;

        public FundaOfferHttpClientFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IFundaOfferHttpClient<T> GetFundaOfferHttpClient()
        {
            return serviceProvider.GetRequiredService<IFundaOfferHttpClient<T>>();
        }
    }
}