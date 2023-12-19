using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace RoadRunnerApp.Views;

public partial class detailPage : ContentPage
{
	public detailPage()
	{
		// needs to remember where user came from. idk how lol
		InitializeComponent();
	}

	//TODO: multiple constructors. one For route and one for location. 
	//		Both use the same detail page but diffrent init of the 

	public void goBackClicked(object sender, EventArgs e)
	{
		// sends user back to page.
	}

	//only visible and usable if component actually contains route
	public void selectRouteClicked(object sender, EventArgs e)
	{
		// select route to follow. reverts back to map page with route selected.  
	}
}