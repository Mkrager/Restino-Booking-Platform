namespace Restino.Application.Contracts
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        public string UserRole { get; }
    }
}
