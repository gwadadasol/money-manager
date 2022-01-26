using AnalyzerService.Contracts.V1;
using AnalyzerService.Contracts.V1.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnalyzerService.Controllers
{
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
            await _mediator.Send(new GetAmountsByCategoriesRequest());
            return Ok();
        }
    }
}