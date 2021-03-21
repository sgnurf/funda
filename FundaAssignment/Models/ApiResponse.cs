namespace FundaAssignment.Models
{
    public record ApiResponse<T>(bool IsSuccess, T Body);
}