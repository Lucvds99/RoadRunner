using RoadRunnerApp.AppRoutes;
using Mlocation = Microsoft.Maui.Devices.Sensors.Location;


namespace UnitTests
{
    public class RouteTests
    {
        RouteManager routeMngr = new RouteManager();

        [Fact]
        public async void CheckDecodedRoute()
        {
            Mlocation userLocation = new Mlocation(51.58775, 4.782); // Test origin
            Landmark testDest = new Landmark(1, "VVV-pand", "test description", "Unit Testing Theme", new CustomLocation(51.5941117, 4.7794167));
            List<Mlocation> coordinatesList = await routeMngr.GetDecodedRoute(new List<Landmark> { testDest }, userLocation);

            int amountOfCoordinates = coordinatesList.Count;
            Assert.Equal(24, amountOfCoordinates);
        }
    }
}
