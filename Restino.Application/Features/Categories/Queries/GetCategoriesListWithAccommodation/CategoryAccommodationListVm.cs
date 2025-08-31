namespace Restino.Application.Features.Categories.Queries.GetCategoriesListWithAccommodation
{
    public class CategoryAccommodationListVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
        public ICollection<CategoryAccommodationDto>? Accommodations { get; set; }
    }
}
