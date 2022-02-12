using AutoMapper;
using MediatR;
using CategoryService.Contracts.V1.Requests;
using CategoryService.Domains.Dtos;
using CategoryService.Domains.Model;
using CategoryService.Domains.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CategoryService.Contracts.V1.Handlers
{
    public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CreateCategoryRequestHandler> _logger;

        public CreateCategoryRequestHandler(IMapper mapper, ICategoryRepository repository, ILogger<CreateCategoryRequestHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<CategoryDto> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogTrace($"Handler CreateCategoryRequest: {request.Name}");

            var category = new CategoryEntity { Name = request.Name };
            _repository.CreateCategory(category);
            bool inserted = await _repository.SaveChangesAsync();

            _logger.LogTrace($"SaveChangesAsync status: {inserted}");
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
