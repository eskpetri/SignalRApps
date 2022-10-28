namespace MauiAppClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddTransient(provider => new HttpClient
        {
            BaseAddress = new Uri($@"https://{(DeviceInfo.DeviceType == DeviceType.Virtual
            ? "10.0.2.2" : "localhost")}:7181/"),
            Timeout = TimeSpan.FromSeconds(10)
        });

        return builder.Build();
    }
}
