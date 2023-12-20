using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
using RoadRunnerApp.AppRoutes;
using Mlocation = Microsoft.Maui.Devices.Sensors.Location;
using System.Diagnostics;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;


namespace RoadRunnerApp.Views;

public partial class MapPage : ContentPage
{
    private readonly IRouteService _routeService;
    private List<Landmark> _landMarksToDraw;


	public MapPage()
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");


        /////////////////hard coded embeding xaml 
        
        

        ////////////////
 
        _routeService = new RouteManager();
        _landMarksToDraw = new List<Landmark>();

        _routeService.LandmarksRecieved += OnLandmarksReceived;
        _routeService.CoordinatesReceived += OnCoordinatesReceived;

        _routeService.GetLandmarks();
        _routeService.GetRouteCoordinates(_landMarksToDraw);

        MainMap.IsShowingUser = true;

        MainMap.IsVisible = true;
        // To do:

        // Get/update user's realtime location on map

        // Get/set route from DB

        // Get all saved points of interest from DB

        // Show route on map

        // Highlight each saved point with a circle on map.

        // Handle Logic for point of interest interaction -> Notification Controller

        // Dummy 'DB' List for testing:

        Thread updateThread = new Thread(UpdateMap);
        updateThread.Start();

    }

    public async void UpdateMap()
    {

        while (true)
        {


            Mlocation location =  await GetUserLocation();

            MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);


            Trace.WriteLine("bozo mapspan:" + mapSpan.Center + "location:" + location);


            Device.BeginInvokeOnMainThread(() =>
            {
                MainMap.MoveToRegion(mapSpan);
            });
            
            await Task.Delay(500);

        }

    }

    public async Task<Mlocation> GetUserLocation()
    {
        try
        {
            Mlocation location = await Geolocation.Default.GetLastKnownLocationAsync();

            if (location != null)
                return location;
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
        }

        return null; // Johan might be dissapointed
    }

    public void Drawpins(List<Landmark> landmarks)  
    {
        foreach (Landmark landmark in landmarks)
        {
            double longitude = landmark.location.longitude;
            double latitude = landmark.location.latitude;

            Microsoft.Maui.Devices.Sensors.Location pinlocation = new Microsoft.Maui.Devices.Sensors.Location(latitude, longitude);
            MainMap.Pins.Add(new Pin { Location = pinlocation, Label = landmark.name, Type = PinType.Place });
        }
        
    }

    private void OnLandmarksReceived(object sender, List<Landmark> landmarks)
    {
        this._landMarksToDraw = landmarks;
        Drawpins(_landMarksToDraw);
    }


    private void OnCoordinatesReceived(object sender, List<Mlocation> coordinates)
    {
        // Handle received coordinates, e.g., draw polylines on the map
        // Use the received coordinates to draw polylines on the frontend
        DrawPolyline(coordinates);
    }


    public void DrawPolyline(List<Mlocation> locations)
    {
        Polyline polyline = new Polyline
        {
            StrokeColor = Colors.Blue,
            StrokeWidth = 12,
            Geopath =
            {
                
            }

        };


        foreach (Mlocation location in locations)
        {
            polyline.Geopath.Add(location);
        }
        MainMap.MapElements.Add(polyline);


    }


    private void LocationsButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LocationPage());
    }

    private void MapButton(object sender, EventArgs e)
    {
        
    }

    private void RoutesButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RoutesPage());
    }
}