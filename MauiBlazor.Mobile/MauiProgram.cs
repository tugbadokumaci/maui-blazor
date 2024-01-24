using Microsoft.Extensions.Logging;
using MauiBlazor.Mobile.Data;
using Microsoft.AspNetCore.Components.WebView.Maui;
using CommunityToolkit.Maui;

namespace MauiBlazor.Mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		
builder.UseMauiApp<App>().UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddScoped<HttpClient>();
        //builder.Services.AddSingleton<IStudentService, StudentService>();
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddSingleton<CardService>();

		return builder.Build();
	}
}

