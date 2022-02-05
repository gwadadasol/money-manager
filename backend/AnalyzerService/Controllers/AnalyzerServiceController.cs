using AnalyzerService.Contracts.V1;
using AnalyzerService.Contracts.V1.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerService.Controllers
{
    [ApiController]
    public class AnalyzerServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnalyzerServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Analyzer.GetAmountsCategories)]
        public async Task<IActionResult> GetAmountsByCategories()
        {
            var result = await _mediator.Send(new GetAmountsByCategoriesRequest());
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Analyzer.GetBalanceMonthly)]
        public async Task<IActionResult> GetBalancesMonthly()
        {
            var result = await _mediator.Send(new GetBalancesMonthlyRequest());
            return Ok(result);
        }


    }
}