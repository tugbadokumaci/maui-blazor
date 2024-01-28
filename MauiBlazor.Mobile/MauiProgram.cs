using Microsoft.Extensions.Logging;
using MauiBlazor.Mobile.Data;
using Microsoft.AspNetCore.Components.WebView.Maui;
using CommunityToolkit.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;
using ZXing.Net.Maui.Controls;

namespace MauiBlazor.Mobile;

public static class MauiProgram
{
    [Obsolete]
    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		
builder.UseMauiApp<App>().UseMauiCommunityToolkit()
                         .UseBarcodeReader()

            .UseSkiaSharp()
            .ConfigureFonts(fonts =>
			{
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemiold");
                fonts.AddFont("Inter-Black.ttf", "InterBlack");
                fonts.AddFont("Inter-Bold.ttf", "InterBold");
                fonts.AddFont("Inter-ExtraBold.ttf", "InterExtraBold");
                fonts.AddFont("Inter-ExtraLight.ttf", "InterExtraLight");
                fonts.AddFont("Inter-Light.ttf", "InterLight");
                fonts.AddFont("Inter-Medium.ttf", "InterMedium");
                fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                fonts.AddFont("Inter-Semibold.ttf", "InterSemibold");
                fonts.AddFont("Inter-Thin.ttf", "InterThin");
            });

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        builder.Services.AddBlazorBootstrap();
        builder.Services.AddScoped<HttpClient>();
        //builder.Services.AddSingleton<IStudentService, StudentService>();
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddSingleton<CardService>();
     

        return builder.Build();
	}
}

