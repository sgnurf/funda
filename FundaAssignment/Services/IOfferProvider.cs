using FundaAssignment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundaAssignment.Services
{
    public interface IOfferProvider
    {
        Task<IEnumerable<OfferItem>> GetOffer(IEnumerable<string> filters);
    }
}