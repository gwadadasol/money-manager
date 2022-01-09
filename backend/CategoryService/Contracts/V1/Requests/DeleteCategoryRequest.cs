using MediatR;

namespace CategoryService.Contracts.V1.Requests
{
    public class DeleteCategoryRequest : IRequest<bool>
    {
        public int CategoryId { get; set; }
    }
}
