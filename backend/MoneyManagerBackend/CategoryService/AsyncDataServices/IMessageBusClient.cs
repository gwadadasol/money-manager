using CategoryService.Domains.Dtos;

namespace CategoryService.AsyncDataServices{
    public interface IMessageBusClient
    {
        void PublisheNewCategoryRule(RulePublishedDto rulePublishedDto);
    }
}