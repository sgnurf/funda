namespace FundaAssignment.Services.SearchQuery
{
    public class GardenSearchQueryProcessor : AbstractSearchQueryFilterProcessor
    {
        private const string GARDEN = "garden";
        private const string TUIN = "tuin";

        public GardenSearchQueryProcessor(ISearchQueryFilterProcessor nextProcessor) : base(nextProcessor)
        {
        }

        public override string ProcessFilter(string filter)
        {
            return filter == GARDEN
                ? TUIN
                : CallNextProcessor(filter);
        }
    }
}