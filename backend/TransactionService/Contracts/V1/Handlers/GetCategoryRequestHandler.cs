using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using TransactionService.Contracts.V1.Models;

namespace TransactionService.Contracts.V1.Requests;

public class GetCategoryRequestHandler : IRequestHandler<GetCategoryRequest, string>
{
    private readonly string _categoryService;
    private readonly string _endPoint;
    private readonly ILogger<GetCategoryRequestHandler> _logger;

    public GetCategoryRequestHandler(IConfiguration configuration, ILogger<GetCategoryRequestHandler> logger)
    {
        _categoryService = configuration["CategoryService"];
        _endPoint = configuration["CategoryServiceGetCategory"];
        _logger = logger;

    }

    public Task<string> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogTrace($"Request => {request.Description}");
        _logger.LogTrace($"{_categoryService}{_endPoint}");

        var client = new RestClient(_categoryService);
        var requestApi = new RestRequest($"{_endPoint}")
                            .AddQueryParameter("description", request.Description);

        var response = client.Get(requestApi);


        if (response.StatusCode == HttpStatusCode.OK)
        {
            Category category = JsonSerializer.Deserialize<Category>(response.Content);
            return Task.FromResult(category.Name);
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Task.FromResult(string.Empty);
        }
        else
        {
            _logger.LogTrace($"--> Error Message:{response.ErrorMessage}");
            throw response.ErrorException;

        }
    }
}