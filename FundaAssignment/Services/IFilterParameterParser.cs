using System.Collections.Generic;

namespace FundaAssignment.Services
{
    public interface IFilterParameterParser
    {
        IEnumerable<string> ParseFilters(string filters);
    }
}