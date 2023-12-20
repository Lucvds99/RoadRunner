using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
using RoadRunnerApp.AppRoutes;
using Mlocation = Microsoft.Maui.Devices.Sensors.Location;


namespace RoadRunnerApp.Views;

public partial class MapPage : ContentPage
{

    private readonly IRouteService _routeService;

	public MapPage()
	{
		InitializeComponent();



        // To do:

        // Get/update user's realtime location on map

        // Get/set route from DB

        // Get all saved points of interest from DB

        // Show route on map

        // Highlight each saved point with a circle on map.

        // Handle Logic for point of interest interaction -> Notification Controller

        // Dummy 'DB' List for testing:

        _routeService = new RouteManager();

        _routeService.CoordinatesReceived += OnCoordinatesReceived;
        _routeService.GetRouteCoordinates();

        Microsoft.Maui.Devices.Sensors.Location location = new Microsoft.Maui.Devices.Sensors.Location(51.659012926034684, 4.95121208613425, 17);
      
        
       MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
        MainMap.MoveToRegion(mapSpan);
        MainMap.IsShowingUser = true;

        Circle circle = new Circle()
        {
            Center = location,
            Radius = Distance.FromMeters(50),
            StrokeColor = Colors.Aqua,
            StrokeWidth = 8,
            FillColor = Color.FromRgba(255, 0, 0, 64)
        };

        //Polyline polyline = new Polyline
        //{
        //    StrokeColor = Colors.Blue,
        //    StrokeWidth = 12,
        //    Geopath =
        //    {
        //        new Mlocation(51.58775f, 4.782f),
        //        new Mlocation(51.5941117f, 4.7794167f)
        //    }
        //};

        // Instantiate a polygon
        Polygon polygon = new Polygon
        {
            StrokeWidth = 8,
            StrokeColor = Color.FromArgb("#1BA1E2"),
            FillColor = Color.FromArgb("#881BA1E2"),
            Geopath =
            {
                new Mlocation(51.58775f, 4.782f),
                new Mlocation(51.5941117f, 4.7794167f),
                new Mlocation(51.5925f, 4.7794167f),
            }
        };

        // Add the polygon to the map's MapElements collection
        //MainMap.MapElements.Add(polygon);


        // Add the Polyline to the map's MapElements collection
        

        MainMap.MapElements.Add(circle);

        Map map = new Map(mapSpan);
        map.MapElements.Add(circle);

        drawpins(getLandmarkList());
        


 



    }

    public void drawpins(List<Landmark> landmarks)  
    {
        foreach (Landmark landmark in landmarks)
        {
            double longitude = landmark.location.longitude;
            double latitude = landmark.location.latitude;

            Microsoft.Maui.Devices.Sensors.Location pinlocation = new Microsoft.Maui.Devices.Sensors.Location(longitude, latitude);
            MainMap.Pins.Add(new Pin { Location = pinlocation, Label = landmark.name, Type = PinType.Place });
        }
        
    }


    public List<Landmark> getLandmarkList()
    {
        
        List<Landmark> landmarks = new List<Landmark>();

        landmarks.Add(new Landmark(1, "Chasse theater", "oulleh", "eets", new AppRoutes.CustomLocation(51.58775f, 4.782f)));
        landmarks.Add(new Landmark(1, "VVV-pand", "oulleh", "eets", new AppRoutes.CustomLocation(51.5941117f, 4.7794167f)));
        landmarks.Add(new Landmark(1, "Nassau Baronie Monument", "oulleh", "eets", new AppRoutes.CustomLocation(51.5925f, 4.779695f)));
        landmarks.Add(new Landmark(1, "Kasteel van Breda", "oulleh", "eets", new AppRoutes.CustomLocation(51.5906117f, 4.7761667f)));
        landmarks.Add(new Landmark(1, "Grote Kerk", "oulleh", "eets", new AppRoutes.CustomLocation(51.5888333f, 4.775278f)));
        landmarks.Add(new Landmark(1, "Bevrijdingsmonument", "oulleh", "eets", new AppRoutes.CustomLocation(51.5880283f, 4.7763333f)));
        landmarks.Add(new Landmark(1, "Stadhuis", "oulleh", "eets", new AppRoutes.CustomLocation(51.58875f, 4.7761111f)));
        return landmarks;
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




    //private CancellationTokenSource _cancelTokenSource;
    //private bool _isCheckingLocation;

    //public async Task GetCurrentLocation()
    //{
    //    try
    //    {
    //        _isCheckingLocation = true;

    //        GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

    //        _cancelTokenSource = new CancellationTokenSource();

    //        Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

    //        if (location != null)
    //            Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
    //    }
    //    // Catch one of the following exceptions:
    //    //   FeatureNotSupportedException
    //    //   FeatureNotEnabledException
    //    //   PermissionException
    //    catch (Exception ex)
    //    {
    //        // Unable to get location
    //    }
    //    finally
    //    {
    //        _isCheckingLocation = false;
    //    }
    //}

    //public void CancelRequest()
    //{
    //    if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
    //        _cancelTokenSource.Cancel();
    //}
}