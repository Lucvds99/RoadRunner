using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
using RoadRunnerApp.AppRoutes;


namespace RoadRunnerApp.Views;

public partial class MapPage : ContentPage
{
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

            landmarks.Add(new Landmark(1, "Chasse theater", "oulleh", "eets", new AppRoutes.Location(51.58775, 4.782)));
            landmarks.Add(new Landmark(1, "VVV-pand", "oulleh", "eets", new AppRoutes.Location(51.5941117, 4.7794167)));
            landmarks.Add(new Landmark(1, "pauper", "oulleh", "eets", new AppRoutes.Location(35.8948, 46.9200)));

            return landmarks;


        
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