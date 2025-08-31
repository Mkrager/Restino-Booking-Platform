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

        public Task<bool> IsAccommodationNameAndCategoryUnique(string name, Guid categoryId)
        {
            var matches = _dbContext.Accommodations.Any(e => e.Name.Equals(name) && e.CategoryId.Equals(categoryId));
            return Task.FromResult(matches);
        }

        public Task<bool> IsAccommodationNameAndCategoryUniqueUpdate(string name, Guid categoryId, Guid? accommodationsId)
        {
            var matches = _dbContext.Accommodations.Any(e =>
                e.Name.Equals(name) &&
                e.CategoryId.Equals(categoryId) &&
                (accommodationsId == null || e.Id != accommodationsId)
        );
            return Task.FromResult(matches);
        }

        public async Task<List<Accommodation>> ListAllAccommodations(bool isAccommodationHot)
        {
            var allAccommodations = await _dbContext.Accommodations.ToListAsync();
            if (isAccommodationHot)
            {
                allAccommodations = allAccommodations.Where(p => p.IsHotProposition).ToList();
            }
            return allAccommodations;
        }

        public Task<List<Accommodation>> ListUserAccommodations(string userId)
        {
            var userAccommodation = _dbContext.Accommodations.Where(e => e.CreatedBy == userId);
            return userAccommodation.ToListAsync();
        }

        public Task<List<Accommodation>> SearchAccommodation(string? searchString)
        {
            var searchResults = _dbContext.Accommodations.
                Where(n => n.Name.Contains(searchString)).ToListAsync();
            return searchResults;
        }
    }
}

