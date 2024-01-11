using RoadRunnerApp.AppDatabase;
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
            Assert.Equal(26, amountOfSights);
        }

    }

}