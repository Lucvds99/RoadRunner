using RoadRunnerApp.AppRoutes;
using RoadRunnerApp.Views;
using RoadRunnerApp.UIControllers;
using Mlocation = Microsoft.Maui.Devices.Sensors.Location;

namespace UnitTests
{
    public class MapPageTest()
    {
        MapPage mapPage = new MapPage(new List<Landmark>(), new List<Landmark>(), new RoadRunnerApp.AppRoutes.RouteManager(), new User());

        [Theory]
        [InlineData(10)]
        public async void CheckClosestLandmark(double closestDistance)
        {
            // Arrange
            List<Tuple<Landmark, double>> distances = new List<Tuple<Landmark, double>>
        {
            new Tuple<Landmark, double>(new Landmark(1, "Landmark1", "Description1", "Theme1", "img", new CustomLocation(1.0, 2.0)), 10),
            new Tuple<Landmark, double>(new Landmark(2, "Landmark2", "Description2", "Theme2", "img2", new CustomLocation(3.0, 4.0)), 20),
        };

            // Act
            var closestTuple = await mapPage.GetClosestLandmark(distances);
            var closestLandmark = closestTuple.Item1;

            // Assert
            Assert.NotNull(closestTuple);
            Assert.Equal(closestDistance, closestTuple.Item2);
        }

        [Theory]
        [InlineData(10.0, true)]
        [InlineData(20.0, false)]
        public void CheckIsInDistance(double distance, bool expectedResult)
        {
            // Arrange
            var landmark = new Landmark(1, "Landmark1", "Description1", "Theme1","img", new CustomLocation(1.0, 2.0));

            // Act
            bool isWithinThreshold = mapPage.isInDistance(Tuple.Create(landmark, distance));

            // Assert
            Assert.Equal(expectedResult, isWithinThreshold);
        }

        [Theory]
        [InlineData(1.0, 1.0)]
        public async Task CheckLandMarksDistance(double userLatitude, double userLongitude)
        {
            // Arrange
            
            var mockUserLocation = new Mlocation(userLatitude, userLongitude);

            // Act
            var distances = await mapPage.GetLandmarksDistance(mockUserLocation);

            // Assert
            Assert.NotNull(distances);
            Assert.True(distances.Count > 0);

            foreach (var distance in distances)
            {
                Assert.NotNull(distance.Item1);  // Ensure each landmark is not null
                Assert.True(distance.Item2 >= 0);  // Ensure distance is non-negative
            }
        }

    }
}
