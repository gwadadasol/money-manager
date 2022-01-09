using AutoMapper;
using MediatR;
using CategoryService.Contracts.V1.Requests;
using CategoryService.Domains.Dtos;
using CategoryService.Domains.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CategoryService.Contracts.V1.Handlers
{
    public class GetCategoryByDescriptionRequestHandler : IRequestHandler<GetCategoryByDescriptionRequest, CategoryDto>
    {

        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public GetCategoryByDescriptionRequestHandler(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public Task<CategoryDto> Handle(GetCategoryByDescriptionRequest request, CancellationToken cancellationToken)
        {
                var categoryEnt = _repository.GetCategoryByDescription(request.Description);
                var categoryDto = _mapper.Map<CategoryDto>(categoryEnt);
                return Task.FromResult(categoryDto);
        }
    }
}
