namespace CarRentingSystem.Tests.Mocks
{
    using AutoMapper;

    using CarRentingSystem.Infrastructure;

    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(
                    cfg => cfg.AddProfile<MappingProfile>());

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
