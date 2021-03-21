using FundaAssignment.Configuration;
using FundaAssignment.Models;
using FundaAssignment.Services;
using FundaAssignment.Services.Http;
using FundaAssignment.Services.SearchQuery;
using FundaAssignment.Services.Throttling;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FundaAssignment.Extensiosn
{
    public static class IServiceCollectionExtensions
    {
        public static void AddFundaAssignmentConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FundaApiConfiguration>(configuration.GetSection("FundaApi"));
        }

        public static void AddFundaAssignmentServices(this IServiceCollection services)
        {
            services.AddHttpClient<IFundaOfferHttpClient<OfferItem>, FundaOfferHttpClient<OfferItem>>();
            services.Decorate(typeof(IFundaOfferHttpClient<>), typeof(FundaOfferHttpClientRateLimiterDecorator<>));
            services.Decorate(typeof(IFundaOfferHttpClient<>), typeof(FundaOfferHttpClientCachingDecorator<>));
            services.AddSingleton(typeof(IFundaOfferHttpClientFactory<>), typeof(FundaOfferHttpClientFactory<>));
            services.AddSingleton<IOfferProvider, OfferProvider>();
            services.AddSingleton<IFilterParameterParser, FilterParameterParser>();
            services.AddSingleton<ITopAgentProvider, TopAgentProvider>();
            services.AddSingleton<ISearchQueryBuilder, SearchQueryBuilder>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddSingleton(typeof(IPaginatedDataCollator<>), typeof(PaginatedDataCollator<>));
            services.AddSingleton<IRequestThrottling,RequestThrottling>();

            //TODO: drive the order of the chain fo command from Configuration
            services.AddSingleton<ISearchQueryFilterProcessor, GardenSearchQueryProcessor>(s => new GardenSearchQueryProcessor(null));
            services.Decorate<ISearchQueryFilterProcessor, CitySearchQueryStringProcessor>();
        }
    }
}