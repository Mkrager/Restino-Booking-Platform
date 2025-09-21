using Restino.App.ViewModels.Accommodation;

namespace Restino.App.ViewModels.Category
{
    public class CategoryAccommodationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<AccommodationListViewModel>? Accommodations { get; set; }
    }
}
