namespace Restino.Application.Features.Accommodations.Queries.SearchAccommodationList
{
    public class CategoryDtoAccommodationSearch
    {
        public Guid CategoriesId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
