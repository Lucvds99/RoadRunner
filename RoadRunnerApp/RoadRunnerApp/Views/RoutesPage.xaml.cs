using Microsoft.Maui.Controls;
using RoadRunnerApp.Views.Models;
using System.Collections.ObjectModel;
using RoadRunnerApp.AppRoutes;

namespace RoadRunnerApp.Views;

public partial class RoutesPage : ContentPage
{
    private ObservableCollection<RouteItem> _routesCollection;
    private List<Landmark> _landmarksToVisit;
    private List<Landmark> _landmarksVisited;
    public RoutesPage(List<Landmark> landmarksToVisit, List<Landmark> landmarksVisited)
	{

		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");
        _landmarksToVisit = landmarksToVisit;
        _landmarksVisited = landmarksVisited;

        //TODO make sure that the Routes that we actually have are eighter from the database or just from other pages. 

        _routesCollection = new ObservableCollection<RouteItem>
            {
                new RouteItem { RouteName = "Route 1", ImageSource = "distance.png"},
                new RouteItem { RouteName = "Route 2", ImageSource = "distance.png"},
                new RouteItem { RouteName = "Route 3", ImageSource = "distance.png" },
            };

        collectionView.ItemsSource = _routesCollection;

    }

    private void refresh(object sender, EventArgs e)
    {

    }

    private void LocationsButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LocationPage(_landmarksToVisit, _landmarksVisited));
    }

    private void MapButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MapPage(_landmarksToVisit, _landmarksVisited));     
    }

    private void RoutesButton(object sender, EventArgs e)
    {
        
    }
}