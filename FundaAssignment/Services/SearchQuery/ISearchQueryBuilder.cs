using System.Collections.Generic;

namespace FundaAssignment.Services.SearchQuery
{
    public interface ISearchQueryBuilder
    {
        string GetSearchQuery(IEnumerable<string> filters);
    }
}