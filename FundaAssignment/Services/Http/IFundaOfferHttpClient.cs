using FundaAssignment.Models;
using System.Threading.Tasks;

namespace FundaAssignment.Services.Http
{
    public interface IFundaOfferHttpClient<T>
    {
        Task<ApiCallResult<FundaApiResponse<T>>> GetOffer(string searchQuery);
    }
}