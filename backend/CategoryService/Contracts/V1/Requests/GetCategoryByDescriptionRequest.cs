using MediatR;
using CategoryService.Domains.Dtos;

namespace CategoryService.Contracts.V1.Requests
{
    public class GetCategoryByDescriptionRequest : IRequest<CategoryDto> 
    {
        public string Description{ get; set; }
    }
}

