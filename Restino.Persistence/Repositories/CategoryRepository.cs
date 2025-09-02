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
            var query = _dbContext.Categories
                .Include(x => x.Accommodations)
                .AsQueryable();

            if (onlyOneCategoryResult && categoryId.HasValue)
            {
                query = query.Where(c => c.Accommodations.Any(a => a.CategoryId == categoryId.Value));
            }

            return await query.ToListAsync();
        }

        public async Task<bool> IsCategoryNameUniqueAsync(string name)
        {
            var matches = await _dbContext.Categories.AnyAsync(n => n.Name.Equals(name));
            return matches;
        }
    }
}
