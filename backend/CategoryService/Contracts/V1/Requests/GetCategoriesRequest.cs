using MediatR;
using CategoryService.Domains.Dtos;
using System.Collections.Generic;

namespace CategoryService.Contracts.V1.Requests
{
    public class GetCategoriesRequest : IRequest<List<CategoryDto>> { }
}
