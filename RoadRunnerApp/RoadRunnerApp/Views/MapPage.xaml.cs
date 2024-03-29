using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
using RoadRunnerApp.AppRoutes;
using Mlocation = Microsoft.Maui.Devices.Sensors.Location;
using System.Diagnostics;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;
using RoadRunnerApp.Views.Models;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using RoadRunnerApp.UIControllers;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Alerts;
using System.Net.NetworkInformation;
using System.Globalization;
using System.Resources;


namespace RoadRunnerApp.Views;


public partial class MapPage : ContentPage
{
    private readonly RouteManager _routeService;
    User user;
    private List<Landmark> _landmarksToDraw;
    private List<Landmark> _landMarksToVisit;
    private List<Landmark> _landMarksVisited;
    private Polyline _originalPolyline = null;
    private bool permissionGranted = false;
    private bool backupMove = false;
    private ResourceManager resourceManager;
   
	public MapPage(List<Landmark> landmarksToVisit, List<Landmark> landmarksVisited, RouteManager? routeManager, User user)
	{
        this.user = user;
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");

        if (user.language != "Nederlands")
        {
            CultureInfo selectedCulture = new CultureInfo("nl-NL");
            Thread.CurrentThread.CurrentCulture = selectedCulture;
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
        }
        else
        {
            CultureInfo selectedCulture = new CultureInfo("");
            Thread.CurrentThread.CurrentCulture = selectedCulture;
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
        }
        resourceManager = new ResourceManager("RoadRunnerApp.Resources.Strings.AppResources", typeof(TutorialPage).Assembly);

        
        if (routeManager != null)
        {
            _routeService = routeManager;
        }
        else
        {
            _routeService = new RouteManager();
        }
 
        _landmarksToDraw = new List<Landmark>();
 
   

        _routeService.LandmarksRecieved += OnLandmarksReceived;
        _routeService.CoordinatesReceived += OnCoordinatesReceived;
        _routeService.ReverseCoordinatesReceived += OnReverseCoordinatesReceived;

     
        _routeService.GetLandmarksFromRoute(1);

        if(landmarksToVisit.Count == 0 && landmarksVisited.Count == 0)
        {
            _landMarksToVisit = new List<Landmark>(_landmarksToDraw);
            _landMarksVisited = new List<Landmark>();

        }
        else
        {
            _landMarksToVisit = landmarksToVisit;
            _landMarksVisited = landmarksVisited;
        }


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

        //Thread updateMapThread = new Thread(UpdateMap);
        //updateMapThread.Start();

    }


    public async void UpdateMap()
    {
        while (true)
        {



            Mlocation location = await GetUserLocation();
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    //MainMap.MapElements.Clear();
                    _routeService.GetRouteCoordinates(_landMarksToVisit, location);
                    _routeService.GetReverseRouteCoordinates(_landMarksVisited, location);
                    RouteInformation RouteInfo = new RouteInformation { HeadingTo = resourceManager.GetString("Map-Head") + " " + _landMarksToVisit[0].name, DistanceLeft = resourceManager.GetString("Map-Dist") + " " + _routeService.distance + " km", TimeLeft = resourceManager.GetString("Map-Time") + " " + _routeService.timeLeft + " min" };

                    BindingContext = RouteInfo;

                    if (backupMove)
                    {
                        MapSpan mapSpan = new MapSpan(location, 0.005, 0.005);
                        MainMap.MoveToRegion(mapSpan);
                        backupMove = false;
                    }

                });
            }catch(NullReferenceException e)
            {
                Trace.WriteLine(" ");
            }



            List<Tuple<Landmark, double>> distances = await GetLandmarksDistance(location);
            Tuple<Landmark, double> closestTuple = await GetClosestLandmark(distances);
         

            if (isInDistance(closestTuple))
            {
                
                Landmark closestLandmark = closestTuple.Item1;

                //TODO Deze landmark gebruiken voor het aanroepen van de popup.
                Device.BeginInvokeOnMainThread(() =>
                {
                    Vibration.Vibrate(500);
                    this.ShowPopup(new SimplePopup(NotificationVariant.REACHED_LOCATION, closestLandmark.name, "you have reached this location", closestLandmark.ImgFilePath));
                });

                _landMarksToVisit.Remove(closestLandmark);
                _landMarksVisited.Add(closestLandmark);
               


            }


            await Task.Delay(4000);
        }
    }


    public async void MoveMap()
    {

            while (true)
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
            {
                Thread.Sleep(4000);
                break;
            }
           
        }


        Mlocation location = await GetUserLocation();

        MapSpan mapSpan = new MapSpan(location, 0.005, 0.005);

     
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    MainMap.MoveToRegion(mapSpan);

                } catch (NullReferenceException e)
                {
                    Trace.WriteLine("hij doet het verkeerd");
                    backupMove = true;
                }

            });


        UpdateMap();

    }



    public async Task<List<Tuple<Landmark, double>>> GetLandmarksDistance(Mlocation mlocation)
    {
        List<Tuple<Landmark, double>> distances = new List<Tuple<Landmark, double>>();

        Mlocation user;
        Mlocation poi = new Mlocation();

        foreach (var landmark in _landMarksToVisit)
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

 
    public async Task<Tuple<Landmark, double>> GetClosestLandmark(List<Tuple<Landmark, double>> landmarksWithDistance)
    {
        
        Tuple<Landmark,double> closestTuple = landmarksWithDistance.First();
        double closestDistance = closestTuple.Item2;


        foreach (var tuple in landmarksWithDistance)
        {
            
            double currentDistance = tuple.Item2;

            if (currentDistance < closestDistance)
            {

                closestTuple = tuple;
                closestDistance = closestTuple.Item2;
        
            }
            

        }
        return closestTuple;
    }

    public bool isInDistance(Tuple<Landmark, double> closestTuple)
    {
        if(closestTuple.Item2 <= 15)
        {
            return true;
        }
        return false;


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
            Pin pin = new Pin { Location = pinlocation, Label = landmark.name, Type = PinType.Place };
            MainMap.Pins.Add(pin);

            pin.MarkerClicked += async (s, args) =>
            {
                args.HideInfoWindow = true;
                SimplePopup popup = new SimplePopup(NotificationVariant.STANDARD, landmark.name,  landmark.description, landmark.ImgFilePath);
                this.ShowPopup(popup);
            };

        }
        
    }

    private void OnLandmarksReceived(object sender, List<Landmark> landmarks)
    {
        _landmarksToDraw = landmarks;

        Drawpins(_landmarksToDraw);

    }


    private void OnCoordinatesReceived(object sender, List<Mlocation> coordinates)
    {
        // Handle received coordinates, e.g., draw polylines on the map
        // Use the received coordinates to draw polylines on the frontend
   
        DrawPolyline(coordinates);
    }

    private void OnReverseCoordinatesReceived(object sender, List<Mlocation> coordinates)
    {
        // Handle received coordinates, e.g., draw polylines on the map
        // Use the received coordinates to draw polylines on the frontend


        // To Do: Draw reverse polyline
        DrawReversePolyline(coordinates);
    }


    public void DrawReversePolyline(List<Mlocation> locations)
    {
        Polyline reversePolyline = new Polyline
        {
            StrokeColor = Colors.Purple,
            StrokeWidth = 12,
            Geopath = { }
        };

        foreach (Mlocation location in locations)
        {
            reversePolyline.Geopath.Add(location);
        }
        MainMap.MapElements.Add(reversePolyline);


    }


    public void DrawPolyline(List<Mlocation> locations)
    {
        Polyline polyline = new Polyline
        {
            StrokeColor = Colors.Blue,
            StrokeWidth = 12,
            Geopath = {}
        };

        //if (_originalPolyline == null)
        //{
        //    Polyline originalPolyline = new Polyline
        //    {
        //        StrokeColor = Colors.Purple,
        //        StrokeWidth = 12,
        //        Geopath = { }
        //    };

        //    foreach (Mlocation location in locations)
        //    {
        //        originalPolyline.Geopath.Add(location);
        //    }

        //    _originalPolyline = originalPolyline;
          
        //}


        foreach (Mlocation location in locations)
        {
            polyline.Geopath.Add(location);

        }
        MainMap.MapElements.Clear();
        //MainMap.MapElements.Add(_originalPolyline);
        MainMap.MapElements.Add(polyline);


        // Save initial route so the walked route can be visualized in comparison with the "to-be-walked" route



    }


    private void LocationsButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LocationPage(_landMarksToVisit, _landMarksVisited, _routeService, user));
    }

    private void MapButton(object sender, EventArgs e)
    {
        
    }

    private void RoutesButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RoutesPage(_landMarksToVisit, _landMarksVisited, _routeService, user));
    }
}