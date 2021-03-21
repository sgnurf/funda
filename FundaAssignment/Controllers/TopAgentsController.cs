using FundaAssignment.Models;
using FundaAssignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundaAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopAgentsController : ControllerBase
    {
        private readonly ITopAgentProvider topAgentProvider;
        private readonly IFilterParameterParser filterParameterParser;

        public TopAgentsController(ITopAgentProvider topAgentProvider, IFilterParameterParser filterParameterParser)
        {
            this.topAgentProvider = topAgentProvider;
            this.filterParameterParser = filterParameterParser;
        }

        [HttpGet("{count}/{*filters}")]
        public async Task<IActionResult> Get(int count, string filters= null)
        {
            if (count < 1)
            {
                return BadRequest("The requested count need to be a positive integer.");
            }

            IEnumerable<string> parsedFilters = filterParameterParser.ParseFilters(filters);
            IEnumerable<TopAgent> topAgents = await topAgentProvider.GetTopAgents(count, parsedFilters);

            if(topAgents == null)
            {
                return StatusCode(500, "The service is currently unavailable, please try again later.");
            }

            return Ok(topAgents);
        }
    }
}