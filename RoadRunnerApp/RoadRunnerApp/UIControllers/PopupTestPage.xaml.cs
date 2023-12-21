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
        int secondsToVibrate = 1;
        TimeSpan vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);

        Vibration.Default.Vibrate(vibrationLength);
    }
}