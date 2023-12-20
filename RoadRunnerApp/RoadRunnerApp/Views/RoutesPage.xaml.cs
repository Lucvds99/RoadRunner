namespace RoadRunnerApp.Views;

public partial class RoutesPage : ContentPage
{
	public RoutesPage()
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");
    }

    private void LocationsButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LocationPage());
    }

    private void MapButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MapPage());     
    }

    private void RoutesButton(object sender, EventArgs e)
    {
        
    }
}