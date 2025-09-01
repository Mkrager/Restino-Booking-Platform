using Microsoft.EntityFrameworkCore;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Persistence.Repositories
{
    public class CategoryRepository : BaseRepositrory<Category>, ICategoryRepository
    {
        public CategoryRepository(RestinoDbContext dbcontext) : base(dbcontext)
        {
            
        }

        public async Task<List<Category>> GetCategoryWithAccommodationAsync(bool onlyOneCategoryResult, Guid? categoryId)
        {
            var allCategories = await _dbContext.Categories.Include(x => x.Accommodations).ToListAsync();
            if (onlyOneCategoryResult)
            {
                allCategories = allCategories.Where(x => x.Accommodations.Any(a => a.CategoryId == categoryId)).ToList();
            }

            return allCategories;
        }

        public async Task<bool> IsCategoryNameUniqueAsync(string name)
        {
            var matches = await _dbContext.Categories.AnyAsync(n => n.Name.Equals(name));
            return matches;
        }
    }
}
