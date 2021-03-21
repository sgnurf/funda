namespace FundaAssignment.Services.SearchQuery
{
    public abstract class AbstractSearchQueryFilterProcessor : ISearchQueryFilterProcessor
    {
        protected ISearchQueryFilterProcessor NextProcessor { get; }

        public AbstractSearchQueryFilterProcessor(ISearchQueryFilterProcessor nextProcessor)
        {
            NextProcessor = nextProcessor;
        }

        public abstract string ProcessFilter(string filter);

        protected string CallNextProcessor(string filter)
        {
            return NextProcessor == null 
                ? null 
                : NextProcessor.ProcessFilter(filter);
        }
    }
}