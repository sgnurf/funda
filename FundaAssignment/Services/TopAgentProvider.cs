using FundaAssignment.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaAssignment.Services
{
    public class TopAgentProvider : ITopAgentProvider
    {
        private readonly IOfferProvider offerProvider;

        public TopAgentProvider(IOfferProvider offerProvider)
        {
            this.offerProvider = offerProvider;
        }

        public async Task<IEnumerable<TopAgent>> GetTopAgents(int numberOfAgents, IEnumerable<string> filters)
        {
            IEnumerable<OfferItem> offers = await offerProvider.GetOffer(filters);

            return offers == null
                ? null
                : offers
                    .GroupBy(o => o.MakelaarId)
                    .Select(g =>
                    {
                        return new TopAgent(g.Key, g.First().MakelaarNaam, g.Count());
                    })
                    .OrderByDescending(o => o.OfferCount)
                    .Take(numberOfAgents);
        }
    }
}