using FundaAssignment.Models;
using FundaAssignment.Services.Http;
using FundaAssignment.Services.SearchQuery;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundaAssignment.Services
{
    public class OfferProvider : IOfferProvider
    {
        private readonly ISearchQueryBuilder searchQueryBuilder;
        private readonly IPaginatedDataCollator<OfferItem> paginatedDataCollator;

        public OfferProvider(ISearchQueryBuilder searchQueryBuilder, IPaginatedDataCollator<OfferItem> paginatedDataCollator)
        {
            this.searchQueryBuilder = searchQueryBuilder;
            this.paginatedDataCollator = paginatedDataCollator;
        }

        public async Task<IEnumerable<OfferItem>> GetOffer(IEnumerable<string> filters)
        {

            string searchQuery = searchQueryBuilder?.GetSearchQuery(filters);
            return await paginatedDataCollator.GetCollatedData(searchQuery);
        }
    }
}