using CommunityToolkit.Maui.Views;

namespace RoadRunnerApp.UIControllers;

public partial class PopupTestPage : ContentPage
{
	public PopupTestPage()
	{
		InitializeComponent();
	}

	private void LetPopupBeShowed(object sender, EventArgs e)
	{
		this.ShowPopup(new SimplePopup());
	}
}