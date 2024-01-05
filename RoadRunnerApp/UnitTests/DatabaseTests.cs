using RoadRunnerApp.AppDatabase;
using RoadRunnerApp.AppRoutes;
using System.Data.Common;
using Mlocation = Microsoft.Maui.Devices.Sensors.Location;
using DBRoute = RoadRunnerApp.AppDatabase.Route;

namespace UnitTests
{
    public class DatabaseTests
    {


        DatabaseManager dbManager = new DatabaseManager();

        // Test DB functionality, getting sights from db
        [Fact]
        public void GetAllSights()
        {
            List<Sight> sightsToCheck = dbManager.GetAllSights();

            int amountOfSights = sightsToCheck.Count;
            // Check if amount of retrieved sights out DB are the correct amount
            Assert.Equal(26, amountOfSights);

        }


        // Test DB functionality, getting routes from db
        [Fact]
        public void GetAllRoutes()
        {

            List<DBRoute> routesToCheck = dbManager.GetAllRoutes();

            int amountOfRoutes = routesToCheck.Count;
            

            // Check if amount of retrieved sights out DB are the correct amount
            Assert.Equal(1, amountOfRoutes);
        }


        // Test DB functionality, getting sights in route from db
        [Fact]
        public void GetSightsInRoute()
        {

            List<Sight> sightsToCheck = dbManager.GetSightsInRoute(1);

            int amountOfSights = sightsToCheck.Count;


            // Check if amount of retrieved sights out DB are the correct amount for a certain route
            Assert.Equal(3, amountOfSights);
        }

    }


    public class RouteTests()
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