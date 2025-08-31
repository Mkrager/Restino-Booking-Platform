using Restino.Domain.Common;

namespace Restino.Domain.Entities
{
    public class Categories : AuditableEntity
    {
        public Guid CategoriesId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;

        public ICollection<Accommodation>? Accommodations { get; set; }
    }
}
