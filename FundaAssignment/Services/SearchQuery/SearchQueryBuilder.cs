using System.Collections.Generic;
using System.Text;

namespace FundaAssignment.Services.SearchQuery
{
    public class SearchQueryBuilder : ISearchQueryBuilder
    {
        private readonly ISearchQueryFilterProcessor searchQueryFilterProcessor;

        public SearchQueryBuilder(ISearchQueryFilterProcessor searchQueryFilterProcessor)
        {
            this.searchQueryFilterProcessor = searchQueryFilterProcessor;
        }

        public string GetSearchQuery(IEnumerable<string> filters)
        {
            StringBuilder searchQueryBuilder = new StringBuilder();

            foreach (string filter in filters)
            {
                string result = searchQueryFilterProcessor.ProcessFilter(filter);

                if (result != null)
                {
                    searchQueryBuilder.Append($"/{result}");
                }
            }

            return searchQueryBuilder.ToString();
        }
    }
}