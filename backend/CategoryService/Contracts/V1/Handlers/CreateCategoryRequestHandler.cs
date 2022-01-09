using AutoMapper;
using MediatR;
using CategoryService.Contracts.V1.Requests;
using CategoryService.Domains.Dtos;
using CategoryService.Domains.Model;
using CategoryService.Domains.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CategoryService.Contracts.V1.Handlers
{
    public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly  ICategoryRepository _repository;

        public CreateCategoryRequestHandler(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CategoryDto> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = new CategoryEntity { Name = request.Name };
        
            _repository.CreateCategory(category);
            await _repository.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
