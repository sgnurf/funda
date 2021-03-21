using System;
using System.Collections.Generic;
using System.Linq;

namespace FundaAssignment.Services
{
    public class FilterParameterParser : IFilterParameterParser
    {
        public IEnumerable<string> ParseFilters(string filters)
        {
            if (String.IsNullOrWhiteSpace(filters))
            {
                return Enumerable.Empty<string>();
            }

            return filters
                .Split("/")
                .Select(f => f.Trim());
        }
    }
}