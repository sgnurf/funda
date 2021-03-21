using FundaAssignment.Services;

namespace FundaAssignment.Services.Http
{
    public interface IFundaOfferHttpClientFactory<T>
    {
        IFundaOfferHttpClient<T> GetFundaOfferHttpClient();
    }
}