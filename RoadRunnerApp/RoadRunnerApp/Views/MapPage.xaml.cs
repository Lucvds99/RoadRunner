using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace RoadRunnerApp;

public partial class MapPage : ContentPage
{
	public MapPage()
	{
		InitializeComponent();


        Location location = new Location(51.659012926034684, 4.95121208613425, 17);
        Location locationPin = new Location(51.659012926034684, 4.95121208613425, 17);
        MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
        MainMap.MoveToRegion(mapSpan);
        MainMap.IsShowingUser = true;
        MainMap.Pins.Add(new Pin() { Location = locationPin, Label = "myHome", Address = "myAddress" });

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

    }
}