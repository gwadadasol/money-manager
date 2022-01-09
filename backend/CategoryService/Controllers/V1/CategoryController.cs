using System.Threading.Tasks;
using CategoryService.Contracts.V1;
using CategoryService.Contracts.V1.Requests;
using CategoryService.Domains.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CategoryService.Controllers.V1
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IMediator mediator, ILogger<CategoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet(ApiRoutes.Category.GetAll)]
        public async Task<IActionResult> GetCategories()
        {
            _logger.LogTrace("Get all categories");
            var result = await _mediator.Send(new GetCategoriesRequest());
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Category.Get, Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            _logger.LogTrace("GetCategoryById");
            var result = await _mediator.Send(new GetCategoryRequest() { Id = categoryId });

            _logger.LogTrace($"Result:[Id:{result.Id}, Name:{result.Name}]");
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet(ApiRoutes.Category.GetByDescription, Name = "GetCategoryByDescription")]
        public async Task<IActionResult> GetCategoryByDescription(string description)
        {
            _logger.LogTrace("GetCategoryByDescription");
            var result = await _mediator.Send(new GetCategoryByDescriptionRequest() { Description = description });

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Category.Create)]
        public async Task<IActionResult> Create(string categoryName)
        {
            _logger.LogTrace("Create");

            var result = await _mediator.Send(new CreateCategoryRequest() { Name = categoryName });

            if (result != null)
            {
                // var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                // var locationUri = $"{baseUrl}/{ApiRoutes.Category.Get.Replace("{categoryId}", result.Id.ToString())}";
                // return Created(locationUri, result);

                _logger.LogTrace($"Result:[Id:{result.Id}, Name:{result.Name}]");
                return CreatedAtRoute(nameof(GetCategoryById), new { categoryId = result.Id }, result);
            }
            else
                return NotFound();
        }

        [HttpDelete(ApiRoutes.Category.Delete)]
        public async Task<IActionResult> Delete(int categoryId)
        {
            _logger.LogTrace("Delete");
            _logger.LogTrace($"Category to delete:[Id:{categoryId}]");
            var result = await _mediator.Send(new DeleteCategoryRequest() { CategoryId = categoryId });

            return result ? Ok() : NotFound();
        }

        [HttpPost(ApiRoutes.Rule.Create)]
        public async Task<IActionResult> CreateRule(RuleDto rule)
        {
            _logger.LogTrace("Create");

            var result = await _mediator.Send(new CreateRuleRequest() { Rule = rule });

            if (result != null)
            {
                // var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                // var locationUri = $"{baseUrl}/{ApiRoutes.Category.Get.Replace("{categoryId}", result.Id.ToString())}";
                // return Created(locationUri, result);

                _logger.LogTrace($"Result:[Category:{result.Category}, Pattern:{result.Pattern}]");
                // return CreatedAtRoute(nameof(GetRuleByCategoryName), new { categoryName = result.Category }, result);
                return Ok(result);
            }
            else
                return NotFound();
        }

        [HttpGet(ApiRoutes.Rule.Get, Name = "GetRuleByCategoryName")]
        public async Task<IActionResult> GetRuleByCategoryName(string categoryName)
        {
            _logger.LogTrace("GetRuleByCategoryName");
            var result = await _mediator.Send(new GetRuleByCategoryNameRequest() { Category = categoryName });

            if (result != null)
            {
                _logger.LogTrace($"Result:[Id:{result.Category}, Name:{result.Pattern}]");
            }
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet(ApiRoutes.Rule.GetAll)]
        public async Task<IActionResult> GetRules()
        {
            _logger.LogTrace("Get all rules");
            var result = await _mediator.Send(new GetRulesRequest());
            return Ok(result);
        }

        [HttpDelete(ApiRoutes.Rule.Delete)]
        public async Task<IActionResult> DeleteRule(int ruleId)
        {
            _logger.LogTrace("Delete");
            _logger.LogTrace($"Rule to delete:[Id:{ruleId}]");
            var result = await _mediator.Send(new DeleteRuleRequest() { RuleId = ruleId });

            return result ? Ok() : NotFound();

        }
    }
}
