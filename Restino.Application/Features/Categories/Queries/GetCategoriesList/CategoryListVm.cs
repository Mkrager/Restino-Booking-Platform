namespace Restino.Application.Features.Categories.Queries.GetCategoriesList
{
    public class CategoryListVm
    {
        public Guid Id { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
