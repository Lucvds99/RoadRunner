using RoadRunnerApp.AppRoutes;
using RoadRunnerApp.Views;

namespace UnitTests
{
    public class MapPageTest()
    {
        MapPage mapPage = new MapPage();

        [Theory]
        [InlineData(10)]
        public async void CheckClosestLandmark(double closestDistance)
        {
            // Arrange
            List<Tuple<Landmark, double>> distances = new List<Tuple<Landmark, double>>
        {
            new Tuple<Landmark, double>(new Landmark(1, "Landmark1", "Description1", "Theme1", new CustomLocation(1.0, 2.0)), 10),
            new Tuple<Landmark, double>(new Landmark(2, "Landmark2", "Description2", "Theme2", new CustomLocation(3.0, 4.0)), 20),
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
            var landmark = new Landmark(1, "Landmark1", "Description1", "Theme1", new CustomLocation(1.0, 2.0));

            // Act
            bool isWithinThreshold = mapPage.isInDistance(Tuple.Create(landmark, distance));

            // Assert
            Assert.Equal(expectedResult, isWithinThreshold);
        }

        [Fact]
        public void CheckLandMarksDistance()
        {
            //mapPage.GetClosestLandmark();

            
        }

    }
}
