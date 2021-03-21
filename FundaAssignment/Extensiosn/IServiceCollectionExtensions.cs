using FundaAssignment.Configuration;
using FundaAssignment.Services;
using FundaAssignment.Services.Http;
using FundaAssignment.Services.SearchQuery;
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
            services.AddHttpClient<IFundaOfferHttpClient, FundaOfferHttpClient>();
            services.Decorate<IFundaOfferHttpClient, FundaOfferHttpClientCachingDecorator>();

            services.AddSingleton<IFundaOfferHttpClientFactory, FundaOfferHttpClientFactory>();
            services.AddSingleton<IOfferProvider, OfferProvider>();
            services.AddSingleton<IFilterParameterParser, FilterParameterParser>();
            services.AddSingleton<ITopAgentProvider, TopAgentProvider>();
            services.AddSingleton<ISearchQueryBuilder, SearchQueryBuilder>();
            services.AddSingleton<IMemoryCache, MemoryCache>();

            //TODO: drive the order of the chain fo command from Configuration
            services.AddSingleton<ISearchQueryFilterProcessor, GardenSearchQueryProcessor>(s => new GardenSearchQueryProcessor(null));
            services.Decorate<ISearchQueryFilterProcessor, CitySearchQueryStringProcessor>();
        }
    }
}