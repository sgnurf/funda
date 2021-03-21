using System;
using System.Linq;

namespace FundaAssignment.Services.SearchQuery
{
    public class CitySearchQueryStringProcessor : AbstractSearchQueryFilterProcessor
    {
        //TODO: Use external services to identify cities
        private readonly string[] cities = new string[] { "amsterdam", "rotterdam" };

        public CitySearchQueryStringProcessor(ISearchQueryFilterProcessor nextProcessor) : base(nextProcessor)
        {
        }

        public override string ProcessFilter(string filter)
        {
            string lowerCaseFilter = filter.ToLower();
            return cities.Contains(lowerCaseFilter)
                ? lowerCaseFilter
                : CallNextProcessor(filter);
        }
    }
}