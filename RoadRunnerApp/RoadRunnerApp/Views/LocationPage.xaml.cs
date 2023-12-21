using RoadRunnerApp.Views.Models;
using System.Collections.ObjectModel;

namespace RoadRunnerApp.Views;

public partial class LocationPage : ContentPage
{
    private ObservableCollection<locationItem> _locationCollection;
	public LocationPage()
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");

        _locationCollection = new ObservableCollection<locationItem>
            {
                new locationItem { LocationName = "location 1", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 2", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 3", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 4", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 5", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 6", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 7", ImageSource = "museum.png"}

            };

        collectionView.ItemsSource = _locationCollection;
    }

    private void LocationsButton(object sender, EventArgs e)
    {

    }

    private void MapButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MapPage());
    }

    private void RoutesButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RoutesPage());
    }
}