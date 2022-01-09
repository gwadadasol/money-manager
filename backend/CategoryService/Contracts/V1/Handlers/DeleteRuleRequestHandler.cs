using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CategoryService.Contracts.V1.Requests;
using CategoryService.Domains.Repository;
using MediatR;

namespace CategoryService.Contracts.V1.Handlers
{
    public class DeleteRuleRequestHandler : IRequestHandler<DeleteRuleRequest, bool>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public DeleteRuleRequestHandler(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<bool> Handle(DeleteRuleRequest request, CancellationToken cancellationToken)
        {
            _repository.DeleteRuleById(request.RuleId);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
