using Microsoft.AspNetCore.Mvc;
using Restino.App.Contracts;
using Restino.App.Helpers;
using Restino.App.ViewModels.Category;

namespace Restino.App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryDataService _categoryDataService;
        public CategoryController(ICategoryDataService categoryDataService)
        {
            _categoryDataService = categoryDataService;
        }
        public async Task<IActionResult> Overview()
        {
            var categoryDetails = await _categoryDataService.GetAllCategoriesWithAccommodations(false, Guid.Empty);
            return View(categoryDetails);
        }

        public async Task<IActionResult> CurrentCategoryAccommodations(Guid? categoryId)
        {
            var categoryDetails = await _categoryDataService.GetAllCategoriesWithAccommodations(true, categoryId);
            return View(categoryDetails);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var category = await _categoryDataService.GetCategoryById(id);
            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            var createdCategory = await _categoryDataService.CreateCategory(categoryViewModel);
            TempData["Message"] = HandleErrors.HandleResponse(createdCategory);
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _categoryDataService.DeleteCategory(id);
            TempData["Message"] = "Deleted success";
            return Json(new { redirectUrl = Url.Action("Index", "Home") });
        }
    }
}
