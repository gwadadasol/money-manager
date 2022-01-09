using MediatR;
using CategoryService.Domains.Dtos;

namespace CategoryService.Contracts.V1.Requests
{
    public class GetCategoryRequest : IRequest<CategoryDto> 
    {
        public int Id { get; set; }
    }
}

