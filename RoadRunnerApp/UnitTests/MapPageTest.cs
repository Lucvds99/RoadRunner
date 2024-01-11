using RoadRunnerApp.Views;
using RoadRunnerApp.AppRoutes;

namespace UnitTests
{
    public class MapPageTest()
    {
        MapPage mapPage = new MapPage(new List<Landmark>(), new List<Landmark>(), new RoadRunnerApp.AppRoutes.RouteManager());
        [Fact]
        public void CheckClosestLandmark()
        {
            //mapPage.GetClosestLandmark();

            Assert.Equal(true, false);
        }

        [Fact]
        public void CheckIsInDistance()
        {
            //mapPage.isInDistance();

            Assert.Equal(true, false);
        }

        [Fact]
        public void CheckLandMarksDistance()
        {
            //mapPage.GetClosestLandmark();

            Assert.Equal(true, false);
        }

    }
}
