using System.Threading.Tasks;

namespace FundaAssignment.Services.Throttling
{
    public interface IRequestThrottling
    {
        Task Throttle();
    }
}