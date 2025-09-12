namespace Restino.Domain.Common
{
    public interface IHasCode
    {
        public string? Code { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string? Email { get; set; }
    }
}
