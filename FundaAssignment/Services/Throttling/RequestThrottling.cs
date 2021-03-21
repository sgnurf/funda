using ComposableAsync;
using FundaAssignment.Configuration;
using Microsoft.Extensions.Options;
using RateLimiter;
using System;
using System.Threading.Tasks;

namespace FundaAssignment.Services.Throttling
{
    public class RequestThrottling : IRequestThrottling
    {
        private TimeLimiter timeConstraint;

        public RequestThrottling(IOptions<FundaApiConfiguration> fundaConfiguration)
        {
            timeConstraint = TimeLimiter.GetFromMaxCountByInterval(fundaConfiguration.Value.MaxRequestsPerMinutes, TimeSpan.FromSeconds(60));
        }

        public async Task Throttle()
        {
            await timeConstraint;
        }
    }
}