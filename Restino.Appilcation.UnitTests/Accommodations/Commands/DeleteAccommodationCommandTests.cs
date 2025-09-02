using Moq;
using Restino.Appilcation.UnitTests.Mock;
using Restino.Appilcation.UnitTests.Mocks;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Accommodations.Commands.DeleteAccommodation;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Accommodations.Commands
{
    public class DeleteAccommodationCommandTests
    {
        private readonly Mock<IAccommodationRepository> _mockAccommodationRepository;

        public DeleteAccommodationCommandTests()
        {
            _mockAccommodationRepository = AccommodationRepositoryMock.GetAccommodationRepository();
        }

        [Fact]
        public async Task Delete_Accommodation_RemovesAccommodationFromRepo()
        {
            var handler = new DeleteAccommodationCommandHandler(_mockAccommodationRepository.Object);
            await handler.Handle(new DeleteAccommodationCommand() { Id = Guid.Parse("2a4ab6df-66b8-46f7-8198-c94332964002"), UserRole = "Admin", UserId = "123345089345" }, CancellationToken.None);

            var allAccommodations = await _mockAccommodationRepository.Object.ListAllAsync();
            allAccommodations.Count.ShouldBe(6);
        }
    }
}
