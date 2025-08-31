﻿namespace Restino.App.ViewModels
{
    public class AccommodationSearchListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Capacity { get; set; }
        public string ShortDescription { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? ImgUrl { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid CategoryId { get; set; }
        public string Message { get; set; } = string.Empty;
        public CategoryDtoAccommodationSearchViewModel Category { get; set; } = default!;
    }
}
