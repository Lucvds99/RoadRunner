using RoadRunnerApp.AppRoutes;
using RoadRunnerApp.Views.Models;
using System.Collections.ObjectModel;

namespace RoadRunnerApp.Views;

public partial class LocationPage : ContentPage
{
    private ObservableCollection<locationItem> _locationCollection;
    private RouteManager _routeManager;
    private List<Landmark> _landmarks;
	public LocationPage(RouteManager routeManager)
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");


        _routeManager = routeManager;
        _locationCollection = new ObservableCollection<locationItem>();

        _landmarks = _routeManager.GetLandmarks();

        foreach (var landmark in _landmarks)
        {
            _locationCollection.Add(new locationItem { LocationName = landmark.name, ImageSource = landmark.ImgFilePath });
        }
        collectionView.ItemsSource = _locationCollection;
    }

    private void LocationsButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new detailPage());
    }

    private void MapButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MapPage(_routeManager));
    }

    private void RoutesButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RoutesPage(_routeManager));
    }
}