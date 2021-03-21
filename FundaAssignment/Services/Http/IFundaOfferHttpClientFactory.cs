using FundaAssignment.Services;

namespace FundaAssignment.Services.Http
{
    public interface IFundaOfferHttpClientFactory
    {
        IFundaOfferHttpClient GetFundaOfferHttpClient();
    }
}