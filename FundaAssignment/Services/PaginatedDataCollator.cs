using FundaAssignment.Configuration;
using FundaAssignment.Models;
using FundaAssignment.Services.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundaAssignment.Services
{
    public class PaginatedDataCollator<T> : IPaginatedDataCollator<T>
    {
        private readonly IFundaOfferHttpClientFactory<T> fundaOfferHttpClientfactory;
        private readonly int pageSize;

        public PaginatedDataCollator(IFundaOfferHttpClientFactory<T> fundaOfferHttpClientfactory, IOptions<FundaApiConfiguration> options)
        {
            this.fundaOfferHttpClientfactory = fundaOfferHttpClientfactory;
            pageSize = options.Value.PageSize;
        }

        public async Task<IEnumerable<T>> GetCollatedData(string baseQuery)
        {
            IFundaOfferHttpClient<T> client = fundaOfferHttpClientfactory.GetFundaOfferHttpClient();

            ApiCallResult<FundaApiResponse<T>> response;
            List<T> results = new List<T>();
            int currentPage = 0;

            do
            {
                currentPage++;

                response = await client.GetOffer(GetPaginatedQuery(baseQuery, currentPage));

                if (!response.IsSuccess)
                {
                    return null;
                }

                results.AddRange(response.Body.Objects);
            }
            while (response.Body.Paging.AantalPaginas > response.Body.Paging.HuidigePagina);

            return results;
        }

        private string GetPaginatedQuery(string baseQuery, int currentPage)
        {
            return baseQuery + $"&page={currentPage}&pagesize={pageSize}";
        }
    }
}