namespace FundaAssignment.Models
{
    public record OfferResponse(OfferItem[] Objects);

    public record OfferItem(int MakelaarId, string MakelaarNaam);
}