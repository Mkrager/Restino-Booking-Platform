using AutoMapper;
using Moq;
using Restino.Appilcation.UnitTests.Mock;
using Restino.Application.Contracts.Persistance;
using Restino.Application.Features.Accommodations.Queries.GetAccommodationList;
using Restino.Application.Profiles;
using Shouldly;

namespace Restino.Appilcation.UnitTests.Accommodations.Queries
{
    public class GetAccommodationListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAccommodationRepository> _mockAccommodationRepository;

        public GetAccommodationListQueryHandlerTests()
        {
            _mockAccommodationRepository = RepositoryMocks.GetAccommodationRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetAccommodationListTest_IsAccommodationHotNull()
        {
            bool? isAccommodationHot = null;
            var handler = new GetAccommodationListQueryHandler(_mapper, _mockAccommodationRepository.Object);

            var result = await handler.Handle(new GetAccommodationListQuery() { IsAccommodationHot = isAccommodationHot.HasValue }, CancellationToken.None);

            result.ShouldBeOfType<List<AccommodationListVm>>();

            result.Count.ShouldBe(7);
        }

        [Fact]
        public async Task TaskGetAccommodationListTest_IsAccommodationHoTrue()
        {
            bool? isAccommodationHot = true;
            var handler = new GetAccommodationListQueryHandler(_mapper, _mockAccommodationRepository.Object);

            var result = await handler.Handle(new GetAccommodationListQuery() { IsAccommodationHot = isAccommodationHot.HasValue }, CancellationToken.None);

            result.ShouldBeOfType<List<AccommodationListVm>>();

            result.Count.ShouldBe(4);
        }
    }
}
