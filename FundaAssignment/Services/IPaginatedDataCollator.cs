using FundaAssignment.Services.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundaAssignment.Services
{
    public interface IPaginatedDataCollator<T>
    {
        Task<IEnumerable<T>> GetCollatedData(string baseQuery);
    }
}