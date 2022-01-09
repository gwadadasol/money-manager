using AutoMapper;
using MediatR;
using CategoryService.Contracts.V1.Requests;
using CategoryService.Domains.Dtos;
using CategoryService.Domains.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CategoryService.Contracts.V1.Handlers
{
    public class GetCategoryRequestHandler : IRequestHandler<GetCategoryRequest, CategoryDto>
    {

        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public GetCategoryRequestHandler(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public Task<CategoryDto> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
                var categoryEnt = _repository.GetCategoryById(request.Id);
                var categoryDto = _mapper.Map<CategoryDto>(categoryEnt);
                return Task.FromResult(categoryDto);
        }
    }
}
