namespace FundaAssignment.Models
{
    public record FundaApiResponse<T>(T[] Objects, Paging Paging);

    public record OfferResponse(OfferItem[] Objects, Paging Paging) : FundaApiResponse<OfferItem>(Objects, Paging);

    public record OfferItem(int MakelaarId, string MakelaarNaam);

    public record Paging(int AantalPaginas, int HuidigePagina);
}