using AutoMapper;
using Sample2.Application.Profiles;

namespace Sample2.UnitTests.Utils;

public static class AutoMapperHelper
{
    public static IMapper GetMapperInstance()
    {
        MapperConfiguration config = new (cfg => cfg.AddProfile(new ProductItemProfile()));

        return config.CreateMapper();
    }
}