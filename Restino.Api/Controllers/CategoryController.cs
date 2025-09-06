using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restino.Application.Features.Categories.Commands.CreateCategoryCommand;
using Restino.Application.Features.Categories.Commands.DeleteCategoryCommand;
using Restino.Application.Features.Categories.Queries.GetCategoriesList;
using Restino.Application.Features.Categories.Queries.GetCategoriesListWithAccommodation;
using Restino.Application.Features.Categories.Queries.GetCategoryDetails;

namespace Restino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryListVm>>> GetAllCategories()
        {
            var dtos = await mediator.Send(new GetCategoryListQuery());
            return Ok(dtos);
        }

        [HttpGet("accommodations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryAccommodationListVm>>> GetAllCategoriesWithAccommodations(
            [FromQuery] bool onlyOneCategoryResult,
            [FromQuery] Guid? CategoryId)
        {
            var query = new GetCategoryListWithAccommodationQuery
            {
                OnlyOneCategoryResult = onlyOneCategoryResult,
                Id = CategoryId
            };

            var dtos = await mediator.Send(query);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryDetailsVm>> GetCategoryById(Guid id)
        {
            var query = new GetCategoryDetailQuery { Id = id };
            var dto = await mediator.Send(query);
            return Ok(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> CreateCategory([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var response = await mediator.Send(createCategoryCommand);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            await mediator.Send(command);
            return NoContent();
        }
    }
}