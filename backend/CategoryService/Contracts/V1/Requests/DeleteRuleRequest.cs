using MediatR;

namespace CategoryService.Contracts.V1.Requests
{
    public class DeleteRuleRequest : IRequest<bool>
    {
        public int RuleId { get; set; }
    }
}
