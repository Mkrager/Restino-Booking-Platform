using Microsoft.EntityFrameworkCore;
using Restino.Application.Contracts.Persistance;
using Restino.Domain.Entities;

namespace Restino.Persistence.Repositories
{
    public class AccommodationRepository : BaseRepositrory<Accommodation>, IAccommodationRepository
    {
        public AccommodationRepository(RestinoDbContext dbContext) : base(dbContext)
        {
        }

        public async  Task<List<Accommodation>> GetAccommodationsWithCategoriesAsync(bool isAccommodationHot)
        {
            var query = _dbContext.Accommodations
                .Include(r => r.Category)
                .AsQueryable();

            if (isAccommodationHot)
            {
                query = query.Where(r => r.IsHotProposition);
            }

            return await query.ToListAsync();
        }
        public Task<List<Accommodation>> SearchAccommodationAsync(string searchString)
        {
            var searchResults = _dbContext.Accommodations
                .Include(r => r.Category)
                .Where(n => n.Name.Contains(searchString)).ToListAsync();
            return searchResults;
        }
        public async Task<List<Accommodation>> GetAccommodationsWithCategoriesByUserIdAsync(string userId)
        {
            return await _dbContext.Accommodations
                .Include(r => r.Category)
                .Where(e => e.CreatedBy == userId).ToListAsync();
        }

        public async Task<bool> IsAccommodationNameAndCategoryUniqueAsync(string name, Guid categoryId)
        {
            var matches = await _dbContext.Accommodations.AnyAsync(e => e.Name.Equals(name) && e.CategoryId.Equals(categoryId));
            return matches;
        }

        public async Task<bool> IsAccommodationNameAndCategoryUniqueForUpdateAsync(string name, Guid categoryId, Guid? accommodationsId)
        {
            var matches = await _dbContext.Accommodations.AnyAsync(e =>
                e.Name.Equals(name) &&
                e.CategoryId.Equals(categoryId) &&
                (accommodationsId == null || e.Id != accommodationsId)
        );
            return matches;
        }

        public async Task<Accommodation?> GetAccommodationWithCategoryById(Guid id)
        {
            return await _dbContext.Accommodations
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}

