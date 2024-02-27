namespace MauiBlazor.Mobile;

public partial class App : Application
{
    public static string BaseUrl { get; private set; }

    public App()
    {
        BaseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5059/api" : "http://localhost:5059/api";

        InitializeComponent();

        MainPage = new MainPage();
        //MainPage = new Microsoft.Maui.Controls.NavigationPage(new MainPage());


    }
}

