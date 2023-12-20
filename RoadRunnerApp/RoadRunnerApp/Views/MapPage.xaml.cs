using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
using RoadRunnerApp.AppRoutes;
using Mlocation = Microsoft.Maui.Devices.Sensors.Location;
using System.Diagnostics;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.ComponentModel;


namespace RoadRunnerApp.Views;


public partial class MapPage : ContentPage
{

    private readonly IRouteService _routeService;
    private List<Landmark> _landMarksToDraw;
    private Polyline _originalPolyline = null;

   
	public MapPage()
	{
		InitializeComponent();

        _routeService = new RouteManager();
        _landMarksToDraw = new List<Landmark>();

        _routeService.LandmarksRecieved += OnLandmarksReceived;
        _routeService.CoordinatesReceived += OnCoordinatesReceived;
     
        _routeService.GetLandmarks();

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

        Thread moveMapThread = new Thread(MoveMap);
        moveMapThread.Start();

        Thread updateMapThread = new Thread(UpdateMap);
        updateMapThread.Start();

    }

    public async Task<List<Tuple<Landmark, double>>> GetLandmarkDistance(Mlocation mlocation)
    {
        List<Tuple<Landmark, double>> distances = new List<Tuple<Landmark, double>>();

        Mlocation user;
        Mlocation poi = new Mlocation();

        foreach (var landmark in _landMarksToDraw)
        {
            user = mlocation;
            poi.Latitude = landmark.location.latitude;
            poi.Longitude = landmark.location.longitude;

            // British word for "meter", some weird people spell it like this, but we accept them anyways
            double metres = Mlocation.CalculateDistance(user, poi, DistanceUnits.Kilometers) * 1000;
            var currentDistance = new Tuple<Landmark, double> (landmark, metres);
            distances.Add(currentDistance);
        }

        return distances;
    }

 
    public async Task<Landmark> GetClosestLandmark(List<Tuple<Landmark, double>> landmarksWithDistance)
    {
        Landmark closestLandmark = landmarksWithDistance[0].Item1;
        double closestDistance = landmarksWithDistance[0].Item2;


        foreach (var tuple in landmarksWithDistance)
        {
            
            double currentDistance = tuple.Item2;

            if (currentDistance < closestDistance)
            {
                closestLandmark = tuple.Item1;
                closestDistance = currentDistance;
            }
            

        }
        return closestLandmark;
    }



    public async void UpdateMap()
    {
        while(true)
        {


            Mlocation location = await GetUserLocation();
            Device.BeginInvokeOnMainThread(() =>
            {

                MainMap.MapElements.Clear();
                _routeService.GetRouteCoordinates(_landMarksToDraw, location);

            });


            List<Tuple<Landmark, double>> distances = await GetLandmarkDistance(location);
            Landmark closestLandmark = await GetClosestLandmark(distances);


            foreach (Tuple<Landmark, double> distance in distances)
            {
                Trace.WriteLine(closestLandmark);
            }

            await Task.Delay(2000);
        }
    }

   
    public async void MoveMap()
    {

        while (true)
        {

            Mlocation location = await GetUserLocation();

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
            Geopath = {}
        };

        if (_originalPolyline == null)
        {
            Polyline originalPolyline = new Polyline
            {
                StrokeColor = Colors.Purple,
                StrokeWidth = 12,
                Geopath = { }
            };

            foreach (Mlocation location in locations)
            {
                originalPolyline.Geopath.Add(location);
            }

            _originalPolyline = originalPolyline;
          
        }


        foreach (Mlocation location in locations)
        {
            polyline.Geopath.Add(location);

        }
        MainMap.MapElements.Add(_originalPolyline);
        MainMap.MapElements.Add(polyline);


        // Save initial route so the walked route can be visualized in comparison with the "to-be-walked" route



    }





}