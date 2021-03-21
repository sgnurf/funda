using FundaAssignment.Configuration;
using FundaAssignment.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FundaAssignment.Services.Http
{
    public class FundaOfferHttpClient : IFundaOfferHttpClient
    {
        private readonly HttpClient client;
        private readonly ILogger<OfferProvider> logger;

        public FundaOfferHttpClient(HttpClient client, IOptions<FundaApiConfiguration> fundaConfiguration, ILogger<OfferProvider> logger)
        {
            client.BaseAddress = new Uri($"{fundaConfiguration.Value.OfferApiBaseUrl}/{fundaConfiguration.Value.Key}/");

            this.client = client;
            this.logger = logger;
        }

        public async Task<ApiResponse<OfferResponse>> GetOffer(string searchQuery)
        {
            try
            {
                //TODO: Extract constructing the query string in its own service, make "type" configurable
                string queryString = "?type=koop" 
                    + (String.IsNullOrEmpty(searchQuery)
                        ? string.Empty
                        : $"&zo={searchQuery}");

                HttpResponseMessage response = await client.GetAsync(queryString);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return CreateFailedResponse();
                }

                //TODO: Consider Splitting off deserialisation in it's own service to easily switch between XML and json if needed
                using (var body = await response.Content.ReadAsStreamAsync())
                {
                    OfferResponse offer = await JsonSerializer.DeserializeAsync<OfferResponse>(body);
                    return new ApiResponse<OfferResponse>(true, offer);
                }
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "An exception occured while fetchihng the data");
                return CreateFailedResponse();
            }
        }

        private static ApiResponse<OfferResponse> CreateFailedResponse()
        {
            return new ApiResponse<OfferResponse>(false, null);
        }
    }
}