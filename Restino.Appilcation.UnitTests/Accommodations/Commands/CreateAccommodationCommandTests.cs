using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mocks;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Accommodations.Commands.CreateAccommodation;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Accommodations.Commands
{
    public class CreateAccommodationCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAccommodationRepository> _mockAccommodationRepository;

        public CreateAccommodationCommandTests()
        {
            _mockAccommodationRepository = AccommodationRepositoryMock.GetAccommodationRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Create_Accommodation_Successfully()
        {
            var handler = new CreateAccommodationCommandHandler(_mapper, _mockAccommodationRepository.Object);
            var command = new CreateAccommodationCommand
            {
                Name = "Test",
                Address = "Test",
                Capacity = 42,
                CategoryId = Guid.Parse("cf0976f2-83c1-4708-afea-3e4785db6d66"),
                ShortDescription = "Testtesttesttesttesttesttest",
                LongDescription = "Testtesttesttesttesttesttest",
                ImgUrl = "test",
                Price = 42
            };

            await handler.Handle(command, CancellationToken.None);

            var allAccommodation = await _mockAccommodationRepository.Object.GetAccommodationsWithCategoriesAsync(false);
            allAccommodation.Count.ShouldBe(8);

            var createdAccommodation = allAccommodation.FirstOrDefault(a => a.Name == command.Name && a.Address == command.Address);
            createdAccommodation.ShouldNotBeNull();
            createdAccommodation.Name.ShouldBe(command.Name);
            createdAccommodation.Address.ShouldBe(command.Address);
            createdAccommodation.Capacity.ShouldBe(command.Capacity);
            createdAccommodation.CategoryId.ShouldBe(command.CategoryId);
            createdAccommodation.ShortDescription.ShouldBe(command.ShortDescription);
            createdAccommodation.LongDescription.ShouldBe(command.LongDescription);
            createdAccommodation.ImgUrl.ShouldBe(command.ImgUrl);
            createdAccommodation.Price.ShouldBe(command.Price);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenAccommodationWithSameNameAndCAtegoryIdAlreadyExsit()
        {
            var validator = new CreateAccommodationCommandValidator(_mockAccommodationRepository.Object);
            var query = new CreateAccommodationCommand
            {
                Name = "City View Apartment",
                Address = "Test",
                Capacity = 2,
                CategoryId = Guid.Parse("c119661c-1d5a-42c1-8819-6b0885af4d4a"),
                ShortDescription = "Testtesttesttesttesttesttest",
                LongDescription = "Testtesttesttesttesttesttest",
                ImgUrl = "test",
                Price = 3000
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.ErrorMessage == "An accommodation with the same name and category already exists");
        }
    }
}
