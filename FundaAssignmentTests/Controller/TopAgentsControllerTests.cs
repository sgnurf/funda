using FundaAssignment.Controllers;
using FundaAssignment.Models;
using FundaAssignment.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FundaAssignmentTests.Controller
{
    public class TopAgentsControllerTests
    {
        [Fact]
        public async Task Get_NegativeOrZeroCountRequested_Returns400HttpError()
        {
            TopAgentsController controller = new TopAgentsController(null, null);

            IActionResult actionResult = await controller.Get(0);

            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task Get_PositiveCountRequested_TopAgentsAreRequestedFromTheProvider()
        {
            int requestedCount = 5;
            Mock<IFilterParameterParser> filterParameterParser = new Mock<IFilterParameterParser>();
            Mock<ITopAgentProvider> topAgentProvider = new Mock<ITopAgentProvider>();
            TopAgentsController controller = new TopAgentsController(topAgentProvider.Object, filterParameterParser.Object);

            IActionResult actionResult = await controller.Get(requestedCount);

            topAgentProvider.Verify(p => p.GetTopAgents(requestedCount, It.IsAny<IEnumerable<string>>()));
        }

        [Fact]
        public async Task Get_AListOfTopAgentsIsObtained_ReturnsListOfAgents()
        {
            TopAgent[] providedTopAgents = new TopAgent[] {
                new TopAgent(1, "agent1", 1),
                new TopAgent(2, "agent2", 2) };
            Mock<IFilterParameterParser> filterParameterParser = new Mock<IFilterParameterParser>();
            Mock<ITopAgentProvider> topAgentProvider = new Mock<ITopAgentProvider>();
            topAgentProvider
                .Setup(p => p.GetTopAgents(It.IsAny<int>(), It.IsAny<IEnumerable<string>>()))
                .Returns(Task.FromResult(providedTopAgents.AsEnumerable()));
            TopAgentsController controller = new TopAgentsController(topAgentProvider.Object, filterParameterParser.Object);

            IActionResult actionResult = await controller.Get(10);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
            IEnumerable<TopAgent> topAgents = Assert.IsAssignableFrom<IEnumerable<TopAgent>>(okResult.Value);
            Assert.Equal(providedTopAgents, topAgents);
        }

        [Fact]
        public async Task Get_AnEmptyListListOfTopAgentsIsObtained_ReturnsEnEmptyList()
        {
            Mock<IFilterParameterParser> filterParameterParser = new Mock<IFilterParameterParser>();
            Mock<ITopAgentProvider> topAgentProvider = new Mock<ITopAgentProvider>();
            topAgentProvider
                .Setup(p => p.GetTopAgents(It.IsAny<int>(), It.IsAny<IEnumerable<string>>()))
                .Returns(Task.FromResult(Enumerable.Empty<TopAgent>()));
            TopAgentsController controller = new TopAgentsController(topAgentProvider.Object, filterParameterParser.Object);

            IActionResult actionResult = await controller.Get(10);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(actionResult);
            IEnumerable<TopAgent> topAgents = Assert.IsAssignableFrom<IEnumerable<TopAgent>>(okResult.Value);
            Assert.Empty(topAgents);
        }
    }
}