using Moq;
using Restino.Application.Contracts.Identity;
using Restino.Application.DTOs.Authentication;

namespace Restino.Appilcation.UnitTests.Mocks
{
    public class UserServiceMock
    {
        public static Mock<IUserService> GetUserService()
        {
            var users = new List<GetUserDetailsResponse>
            {
                new GetUserDetailsResponse { UserId = "35b820e5-1cea-47c8-a6a0-cedccfbda4e6", UserName = "user1", Email = "user1@example.com", FirstName = "John", LastName = "Doe" },
                new GetUserDetailsResponse { UserId = "35b820e5-1cea-47c8-a6a0-cedccfbda4e1", UserName = "user2", Email = "user2@example.com", FirstName = "John2", LastName = "Doe2" }
            };

            var mockUserService = new Mock<IUserService>();

            mockUserService
                .Setup(service => service.GetUserDetailsAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) => users.FirstOrDefault(u => u.UserId == userId));

            return mockUserService;
        }
    }
}
