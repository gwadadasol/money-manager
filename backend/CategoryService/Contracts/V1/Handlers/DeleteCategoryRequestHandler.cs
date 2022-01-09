using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CategoryService.Contracts.V1.Requests;
using CategoryService.Domains.Repository;
using MediatR;

namespace CategoryService.Contracts.V1.Handlers
{
    public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, bool>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public DeleteCategoryRequestHandler(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<bool> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            _repository.DeleteCategoryById(request.CategoryId);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
