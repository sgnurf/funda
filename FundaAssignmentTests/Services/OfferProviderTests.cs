using FundaAssignment.Models;
using FundaAssignment.Services;
using FundaAssignment.Services.SearchQuery;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FundaAssignmentTests.Services
{
    public class OfferProviderTests
    {
        [Fact]
        public async void Get_ResponseIsnull_ReturnsNull()
        {
            var (collator, queryBuilder) = GetDependencies(null);
            OfferProvider offerProvider = new OfferProvider(queryBuilder, collator.Object);

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

            var (collator, queryBuilder) = GetDependencies(responseOffer);
            OfferProvider offerProvider = new OfferProvider(queryBuilder, collator.Object);

            IEnumerable<OfferItem> offer = await offerProvider.GetOffer(null);

            Assert.Equal(responseOffer, offer);
        }

        //TODO: Add tests checking that filters are passed to the processor and that the resulting value is passed to the Http Client

        private static (Mock<IPaginatedDataCollator<OfferItem>> collator, ISearchQueryBuilder searchQuerybuilder) GetDependencies(IEnumerable<OfferItem> offerItems)
        {
            Mock<IPaginatedDataCollator<OfferItem>> collator = new Mock<IPaginatedDataCollator<OfferItem>>();
            collator.Setup(c => c.GetCollatedData(It.IsAny<string>()))
                .Returns(Task.FromResult(offerItems));

            return (collator, null);
        }
    }
}