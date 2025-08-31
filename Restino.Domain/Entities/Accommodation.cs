using Restino.Domain.Common;

namespace Restino.Domain.Entities
{
    public class Accommodation : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string ShortDescription {  get; set; } = string.Empty;
        public string LongDescription {  get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string? ImgUrl { get; set; }
        public bool IsHotProposition { get; set; }
        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = default!;
    }
}
