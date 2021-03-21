using FundaAssignment.Configuration;
using FundaAssignment.Models;
using FundaAssignment.Services.Http;
using FundaAssignment.Services.SearchQuery;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FundaAssignment.Services
{
    public class OfferProvider : IOfferProvider
    {
        private readonly IFundaOfferHttpClientFactory fundaOfferHttpClientfactory;
        private readonly ISearchQueryBuilder searchQueryBuilder;

        public OfferProvider(IFundaOfferHttpClientFactory fundaOfferHttpClientfactory, ISearchQueryBuilder searchQueryBuilder)
        {
            this.fundaOfferHttpClientfactory = fundaOfferHttpClientfactory;
            this.searchQueryBuilder = searchQueryBuilder;
        }

        public async Task<IEnumerable<OfferItem>> GetOffer(IEnumerable<string> filters)
        {
            string queryString = searchQueryBuilder?.GetSearchQuery(filters);
            IFundaOfferHttpClient httpClient = fundaOfferHttpClientfactory.GetFundaOfferHttpClient();

            ApiResponse<OfferResponse> offerResponse = await httpClient.GetOffer(queryString);

            if (!offerResponse.IsSuccess
                || offerResponse.Body?.Objects == null)
            {
                return null;
            }

            return offerResponse.Body.Objects;
        }
    }
}