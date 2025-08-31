using Restino.Domain.Common;

namespace Restino.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;

        public ICollection<Accommodation>? Accommodations { get; set; }
    }
}
