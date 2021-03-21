using FundaAssignment.Models;
using System.Threading.Tasks;

namespace FundaAssignment.Services.Http
{
    public interface IFundaOfferHttpClient
    {
        Task<ApiResponse<OfferResponse>> GetOffer(string searchQuery);
    }
}