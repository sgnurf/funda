using FundaAssignment.Models;
using FundaAssignment.Services;
using FundaAssignment.Services.Http;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FundaAssignmentTests.Services
{
    public class OfferProviderTests
    {
        [Fact]
        public async void Get_RequestFailed_ReturnsNull()
        {
            ApiResponse<OfferResponse> failedResponse = new ApiResponse<OfferResponse>(false, null);
            var (_, offerServiceFactory) = GetDependencies(failedResponse);

            OfferProvider offerProvider = new OfferProvider(offerServiceFactory.Object, null);

            IEnumerable<OfferItem> offer = await offerProvider.GetOffer(null);

            Assert.Null(offer);
        }

        [Fact]
        public async void Get_ObjectPropertyIsNullOnResponse_ReturnsNull()
        {
            OfferResponse offerResponse = new OfferResponse(null);
            ApiResponse<OfferResponse> failedResponse = new ApiResponse<OfferResponse>(true, offerResponse);
            var (_, offerServiceFactory) = GetDependencies(failedResponse);

            OfferProvider offerProvider = new OfferProvider(offerServiceFactory.Object, null);

            IEnumerable<OfferItem> offer = await offerProvider.GetOffer(null);

            Assert.Null(offer);
        }

        [Fact]
        public async void Get_ResponseContainsOffer_ReturnsOffer()
        {
            OfferItem[] responseOffer =
            {
                new OfferItem(1,"TestAgent"),
                new OfferItem(1,"TestAgent2")
            };

            OfferResponse offerResponse = new OfferResponse(responseOffer);
            ApiResponse<OfferResponse> failedResponse = new ApiResponse<OfferResponse>(true, offerResponse);
            var (_, offerServiceFactory) = GetDependencies(failedResponse);

            OfferProvider offerProvider = new OfferProvider(offerServiceFactory.Object, null);

            IEnumerable<OfferItem> offer = await offerProvider.GetOffer(null);

            Assert.Equal(responseOffer, offer);
        }

        //TODO: Add tests checking that filters are passed to the processor and that the resulting value is passed to the Http Client

        private static (Mock<IFundaOfferHttpClient> OfferService, Mock<IFundaOfferHttpClientFactory> offerServiceFactory) GetDependencies(ApiResponse<OfferResponse> offerResponse)
        {
            Mock<IFundaOfferHttpClient> httpClient = new Mock<IFundaOfferHttpClient>();
            httpClient
                .Setup(s => s.GetOffer(It.IsAny<string>()))
                .Returns(Task.FromResult(offerResponse));

            Mock<IFundaOfferHttpClientFactory> httpClientFactory = new Mock<IFundaOfferHttpClientFactory>();
            httpClientFactory
                .Setup(f => f.GetFundaOfferHttpClient())
                .Returns(httpClient.Object);

            return (httpClient, httpClientFactory);
        }
    }
}