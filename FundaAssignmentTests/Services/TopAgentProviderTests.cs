using FundaAssignment.Models;
using FundaAssignment.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FundaAssignmentTests.Services
{
    public class TopAgentProviderTests
    {
        [Fact]
        public async void Get_OfferIsNull_ReturnsNull()
        {
            Mock<IOfferProvider> offerProvider = GetOfferProviderMock(null);
            TopAgentProvider topAgentProvider = new TopAgentProvider(offerProvider.Object);

            IEnumerable<TopAgent> topAgents = await topAgentProvider.GetTopAgents(10, null);

            Assert.Null(topAgents);
        }


        [Theory]
        [MemberData(nameof(SuccessfulOfferResponses))]
        public async void Get_OfferIsNotNull_ReturnsOrderedTopAgents(IEnumerable<OfferItem> offer, IEnumerable<TopAgent> expectedResult)
        {
            Mock<IOfferProvider> offerProvider = GetOfferProviderMock(offer);
            TopAgentProvider topAgentProvider = new TopAgentProvider(offerProvider.Object);

            IEnumerable<TopAgent> topAgents = await topAgentProvider.GetTopAgents(10, null);

            Assert.Equal(expectedResult, topAgents);
        }

        [Fact]
        public async void Get_OfferSuccesfulyObtainedWithMoreAgentsThanRequested_ReturnsOnlyRequestedCount()
        {
            int requestedAgentCount = 5;
            int agentsInOffer = 10;
            IEnumerable<OfferItem> offer = GenerateOffers(agentsInOffer);
            Mock<IOfferProvider> offerProvider = GetOfferProviderMock(offer);
            TopAgentProvider topAgentProvider = new TopAgentProvider(offerProvider.Object);

            IEnumerable<TopAgent> topAgents = await topAgentProvider.GetTopAgents(requestedAgentCount, null);

            Assert.Equal(requestedAgentCount, topAgents.Count());
        }

        public static IEnumerable<object[]> SuccessfulOfferResponses()
        {
            OfferItem[] offers = GenerateOffers(3).ToArray();
            OfferItem o1 = offers[0];
            OfferItem o2 = offers[1];
            OfferItem o3 = offers[2];

            yield return new object[] { GenerateOffer(o1, o2, o3), GenerateExpectedResult((o1, 1), (o2, 1), (o3, 1)) };
            yield return new object[] { GenerateOffer(o1, o1, o1), GenerateExpectedResult((o1, 3)) };
            yield return new object[] { GenerateOffer(o1, o2, o2, o3, o3, o3), GenerateExpectedResult((o3, 3), (o2, 2), (o1, 1)) };
        }

        private static Mock<IOfferProvider> GetOfferProviderMock(IEnumerable<OfferItem> offer)
        {
            Mock<IOfferProvider> offerProvider = new Mock<IOfferProvider>();
            offerProvider
                .Setup(p => p.GetOffer(It.IsAny<IEnumerable<string>>()))
                .Returns(Task.FromResult(offer));
            return offerProvider;
        }

        private static IEnumerable<OfferItem> GenerateOffers(int count) {
            for(int i=0; i < count; ++i)
            {
                yield return new OfferItem(i, "TestAgent" + i);
            }
        }

        private static IEnumerable<OfferItem> GenerateOffer(params OfferItem[] offers) => offers.AsEnumerable();
        private static IEnumerable<TopAgent> GenerateExpectedResult(params (OfferItem agent, int count)[] offers) => offers.Select(o => new TopAgent(o.agent.MakelaarId, o.agent.MakelaarNaam, o.count));
    }
}