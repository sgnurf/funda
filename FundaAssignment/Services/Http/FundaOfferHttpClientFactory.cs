using FundaAssignment.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FundaAssignment.Services.Http
{
    public class FundaOfferHttpClientFactory : IFundaOfferHttpClientFactory
    {
        private readonly IServiceProvider serviceProvider;

        public FundaOfferHttpClientFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IFundaOfferHttpClient GetFundaOfferHttpClient()
        {
            return serviceProvider.GetRequiredService<IFundaOfferHttpClient>();
        }
    }
}