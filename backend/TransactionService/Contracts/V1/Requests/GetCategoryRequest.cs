using MediatR;

namespace TransactionService.Contracts.V1.Requests;

public class GetCategoryRequest: IRequest<string>
{
    public string Description {get; set;}
}