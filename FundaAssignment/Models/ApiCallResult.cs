namespace FundaAssignment.Models
{
    public record ApiCallResult<T>(bool IsSuccess, T Body);
}