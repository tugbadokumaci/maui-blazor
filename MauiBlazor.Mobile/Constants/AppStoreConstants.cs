namespace MauiBlazor.Mobile.Constants;

public static class AppStoreConstants
{
    //const string _appStoreLink = "itms-apps://www.apple.com/tr/developer/argus-teknoloji-bilişim-sanayi-ticaret-anonim-şirketi/id1662508488";
    const string _appStoreLink = "itms-apps://www.apple.com/tr/app/argus-myd/id6463635117";
    const string _googlePlayStoreLink = "market://details?id=com.trifellas.qrgenerator";

    const string _appleAppStoreUrl = "https://apps.apple.com/tr/app/argus-myd/id6463635117";
    const string _googlePlayStoreUrl = "https://play.google.com/store/apps/details?id=com.trifellas.qrgenerator";

    const string _placeHolderUrl = "https://www.argusteknoloji.com";

    //[Obsolete]
    //public static string RatingRequest { get; } = Device.RuntimePlatform switch
    //{
    //    Device.iOS => AppStoreRatingRequestConstants.iOS,
    //    Device.Android => AppStoreRatingRequestConstants.Android,
    //    Device.UWP => AppStoreRatingRequestConstants.Windows,
    //    Device.MacCatalyst => throw new NotImplementedException(),
    //    Device.Tizen => throw new NotImplementedException(),
    //    Device.tvOS => throw new NotImplementedException(),
    //    _ => AppStoreRatingRequestConstants.Other
    //};

    [Obsolete]
    public static string AppLink { get; } = Device.RuntimePlatform switch
    {
        Device.iOS => _appStoreLink,
        Device.Android => _googlePlayStoreLink,
        Device.UWP => throw new NotImplementedException(),
        Device.MacCatalyst => throw new NotImplementedException(),
        Device.Tizen => throw new NotImplementedException(),
        Device.tvOS => throw new NotImplementedException(),
        _ => _placeHolderUrl
    };

    [Obsolete]
    public static string Url { get; } = Device.RuntimePlatform switch
    {
        Device.iOS => _appleAppStoreUrl,
        Device.Android => _googlePlayStoreUrl,
        Device.UWP => throw new NotImplementedException(),
        Device.MacCatalyst => throw new NotImplementedException(),
        Device.Tizen => throw new NotImplementedException(),
        Device.tvOS => throw new NotImplementedException(),
        _ => _placeHolderUrl
    };
}