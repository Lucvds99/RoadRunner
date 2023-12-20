using Microsoft.Maui.Controls;
using RoadRunnerApp.Views.Models;
using System.Collections.ObjectModel;

namespace RoadRunnerApp.Views;

public partial class RoutesPage : ContentPage
{
    private ObservableCollection<RouteItem> _routesCollection;
    public RoutesPage()
	{

		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");
    
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