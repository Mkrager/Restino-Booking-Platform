namespace Restino.App.ViewModels
{
    public class CategoryAccommodationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<AccommodationListViewModel>? Accommodations { get; set; }
    }
}
