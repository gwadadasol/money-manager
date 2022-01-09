using CategoryService.Domains.Dtos;
using MediatR;

namespace CategoryService.Contracts.V1.Requests
{
    public class CreateCategoryRequest : IRequest<CategoryDto> 
    {
        public string Name { get; set; }
    }
}
