using FundaAssignment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundaAssignment.Services
{
    public interface ITopAgentProvider
    {
        Task<IEnumerable<TopAgent>> GetTopAgents(int numberOfAgents, IEnumerable<string> filters);
    }
}