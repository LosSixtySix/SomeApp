using ClientLibrary;
using ClientLibrary.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace SomeApp;

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
		builder.AddServiceDefaults();
        builder.Services.AddHttpClient("someapi",client =>
        {
            client.BaseAddress = new Uri("https+http://someapi");
        });
		builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("someapi"));
        builder.Services.AddSingleton<WeatherView>();
		builder.Services.AddScoped<IWeatherService, WeatherService>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
