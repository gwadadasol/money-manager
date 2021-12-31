using CategoryService.Domains.Dtos;

namespace CategoryService.AsyncDataServices
{
    public interface IMessageBusClientAzure
    {
        void PublisheNewCategoryRule(RulePublishedDto rulePublishedDto);
    }
}