using Microsoft.Maui.Controls;
using Route = RoadRunnerApp.AppDatabase.Route;
using RoadRunnerApp.AppRoutes;
using RoadRunnerApp.Views.Models;
using System.Collections.ObjectModel;
using RoadRunnerApp.AppRoutes;
using RoadRunnerApp.AppDatabase;
using RoadRunnerApp.UIControllers;

namespace RoadRunnerApp.Views;

public partial class RoutesPage : ContentPage
{
    User user;
    private ObservableCollection<RouteItem> _routesCollection;
    private List<Landmark> _landmarksToVisit;
    private List<Landmark> _landmarksVisited;
   
    private List<Route> routes;
    private RouteManager _routeManager;
    public RoutesPage(List<Landmark> landmarksToVisit, List<Landmark> landmarksVisited, RouteManager routeManager)
	{
        this.user = user;

        InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");
        _landmarksToVisit = landmarksToVisit;
        _landmarksVisited = landmarksVisited;

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
        Navigation.PushAsync(new LocationPage(_landmarksToVisit, _landmarksVisited, _routeManager, user));
    }

    private void MapButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MapPage(_landmarksToVisit, _landmarksVisited, _routeManager, user));     
    }

    private void RoutesButton(object sender, EventArgs e)
    {
        
    }
}