using CommunityToolkit.Maui.Views;
using MauiPopup;
using MauiPopup.Views;

namespace MauiBlazor.Mobile.MauiPages;

public partial class DisplayPage : BasePopupPage
{
	public DisplayPage()
	{
		InitializeComponent();
}

    async void OkButton_Clicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}

