using Restino.App.ViewModels.Category;

namespace Restino.App.ViewModels.Accommodation
{
    public class AccommodationListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Capacity { get; set; }
        public string ShortDescription { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsHotProposition { get; set; }
        public string? ImgUrl { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public CategoryViewModel Category { get; set; } = default!;
    }
}
