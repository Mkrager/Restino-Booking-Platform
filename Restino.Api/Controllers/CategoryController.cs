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
    public class CategoryController(IMediator mediator) : Controller
    {

        [HttpGet("all", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryListVm>>> GetAllCategories()
        {
            var dtos = await mediator.Send(new GetCategoryListQuery());
            return Ok(dtos);
        }

        [HttpGet("allwithcategories", Name = "GetAllCategoriesWithAccommodations")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<List<CategoryAccommodationListVm>>>
            GetAllCategoriesWithAccommodations([FromQuery] bool onlyOneCategoryResult, [FromQuery]Guid? CategoryId)
        {
            GetCategoryListWithAccommodationQuery getCategoryListWithAccommodationQuery = new GetCategoryListWithAccommodationQuery() { OnlyOneCategoryResult = onlyOneCategoryResult, Id = CategoryId};
            var dtos = await mediator.Send(getCategoryListWithAccommodationQuery);
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<ActionResult<CategoryDetailsVm>> GetCategoryById(Guid id)
        {
            var getCategoryDeatailQuery = new GetCategoryDetailQuery() { Id = id };
            return Ok(await mediator.Send(getCategoryDeatailQuery));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(Name = "AddCategory")]
        public async Task<ActionResult<Guid>> CreateCategory
            ([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var responce = await mediator.Send(createCategoryCommand);
            return Ok(responce);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            var deleteCategoryCommand = new DeleteCategoryCommand() { Id = id };
            await mediator.Send(deleteCategoryCommand);
            return NoContent();
        }
    }
}
