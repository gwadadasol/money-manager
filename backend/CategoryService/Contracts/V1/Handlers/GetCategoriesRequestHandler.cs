using AutoMapper;
using CategoryService.Contracts.V1.Requests;
using CategoryService.Domains.Dtos;
using CategoryService.Domains.Repository;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CategoryService.Contracts.V1.Handlers
{
    public class GetCategoriesRequestHandler : IRequestHandler<GetCategoriesRequest, List<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public GetCategoriesRequestHandler(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public Task<List<CategoryDto>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
        {
            var categoriesEnt = _repository.GetAllCategories();
            var categoriesDto = categoriesEnt.Select(c => _mapper.Map<CategoryDto>(c)).ToList();

            return Task.FromResult(categoriesDto);
        }
    }
}
