using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using RestSharp;
using TransactionService.Contracts.V1.Models;

namespace TransactionService.Contracts.V1.Requests;

public class GetCategoryRequestHandler : IRequestHandler<GetCategoryRequest, string>
{
    private readonly string _categoryService;
    private readonly string _endPoint;

    public GetCategoryRequestHandler(IConfiguration configuration)
    {
        _categoryService = configuration["CategoryService"];
        _endPoint = configuration["CategoryServiceGetCategory"];

    }

    public Task<string> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
    {
        var client = new RestClient(_categoryService);
        var requestApi = new RestRequest($"{_endPoint}?description={request.Description}");

        var response = client.Get(requestApi);


        if (response.StatusCode == HttpStatusCode.OK)
        {
            Category category = JsonSerializer.Deserialize<Category>(response.Content);
            return Task.FromResult(category.Name);
        }
        return Task.FromResult("");
    }
}