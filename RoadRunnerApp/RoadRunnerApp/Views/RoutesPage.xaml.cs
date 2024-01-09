using Microsoft.Maui.Controls;
using Route = RoadRunnerApp.AppDatabase.Route;
using RoadRunnerApp.AppRoutes;
using RoadRunnerApp.Views.Models;
using System.Collections.ObjectModel;
using RoadRunnerApp.AppDatabase;

namespace RoadRunnerApp.Views;

public partial class RoutesPage : ContentPage
{
    private ObservableCollection<RouteItem> _routesCollection;
    private List<Route> routes;
    private RouteManager _routeManager;
    public RoutesPage(RouteManager routeManager)
	{

		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");
    
        //TODO make sure that the Routes that we actually have are eighter from the database or just from other pages. 

        
        _routeManager = routeManager;
        _routesCollection = new ObservableCollection<RouteItem>();

        DatabaseManager databaseManager = routeManager._dbManager;

        routes = databaseManager.GetAllRoutes();

        foreach (var route in routes)
        {
            _routesCollection.Add(new RouteItem { RouteName = route.Name, Description = route.Description, Difficulty = route.Difficulty.ToString(), Id = route.Id.ToString() });
        }
        collectionView.ItemsSource = _routesCollection;

    }

    private void OnFrameTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new detailPage(routes[0], this));
    }

    private void LocationsButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LocationPage(_routeManager));
    }

    private void MapButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MapPage(_routeManager));     
    }

    private void RoutesButton(object sender, EventArgs e)
    {
        
    }
}