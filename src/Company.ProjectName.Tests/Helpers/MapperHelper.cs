namespace Company.ProjectName.Tests.Helpers;

public static class MapperHelper
{
    public static IMapper Create()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.AddMaps(typeof(ProductProfile).Assembly));
        return config.CreateMapper();
    }
}
