using Microsoft.Maui.Controls.PlatformConfiguration;
using SQLite;

namespace RoadRunnerApp.AppDatabase;

class DatabaseManager
{
    private QueryBuilder _queryBuilder;
    private SQLiteConnection sqlite_conn;

    public DatabaseManager()
    {
        _queryBuilder = new QueryBuilder();

        try
        {
            Console.WriteLine(Constants.DatabasePath);
            this.sqlite_conn = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during database initialization: {ex.Message}");
            throw; // Rethrow the exception to ensure it's not silently ignored
        }

        if (!CheckForData()) InitializeDatabase(); //TODO: ! voor CheckForData()

    }

    public List<T> PerformTask<T>(string sqlCommand) where T : new()
    {
        try
        {
            var cmd = sqlite_conn.CreateCommand(sqlCommand);

            // Using the Query method for SELECT statements
            List<T> result = new List<T>();
            result.AddRange(sqlite_conn.Query<T>(sqlCommand));

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Sight> GetAllSights()
    {
        var selectQuery = _queryBuilder.GetAllSights();
        return PerformTask<Sight>(selectQuery);
    }

    public List<Route> GetAllRoutes()
    {
        var selectQuery = _queryBuilder.GetAllRoutes();
        return PerformTask<Route>(selectQuery);
    }

    public List<Sight> GetSightsInRoute(int routeId)
    {
        var selectQuery = _queryBuilder.GetSightsInRoute(routeId);
        return PerformTask<Sight>(selectQuery);
    }

    private bool CheckForData()
    {
        try
        {
            // Execute a query to check if the table exists
            var tableCount = sqlite_conn.ExecuteScalar<int>(
                $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Sight';");

            // If the count is greater than zero, the table exists
            return tableCount > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking table existence: {ex.Message}");
            return false;
        }
    }

    public void InitializeDatabase()
    {
        this.sqlite_conn.CreateTable<Sight>();
        this.sqlite_conn.CreateTable<Route>();
        this.sqlite_conn.CreateTable<SightRouteLink>();

        List<Sight> sights = new List<Sight>();
        sights.Add(new Sight
        {
            Id = 1,
            Longitude = 4.77942,
            Latitude = 51.59411,
            Name = "Oude VVV-pand",
            Description = "Het Oude pand van de VVV",
            Category = Categories.Museum,
            ImgFilePath = "/path/to/image1.jpg"
        });
        sights.Add(new Sight
        {
            Id = 2,
            Longitude = 4.77939,
            Latitude = 51.59328,
            Name = "Liefdeszuster",
            Description = "Leifdeszusters pant",
            Category = Categories.Park,
            ImgFilePath = "/path/to/image2.jpg"
        });
        sights.Add(new Sight
        {
            Id = 3,
            Longitude = 4.77970,
            Latitude = 51.57583,
            Name = "Nassau Baronie Monument",
            Description = "",
            Category = Categories.Monument,
            ImgFilePath = "/path/to/image3.jpg"
        });

        this.sqlite_conn.InsertAll(sights);

        List<Route> routes = new List<Route>();
        routes.Add(new Route
        {
            Id = 1,
            Name = "Test route",
            Description = "",
            Difficulty = Difficulties.Easy
        });

        this.sqlite_conn.InsertAll(routes);

        List<SightRouteLink> sightRouteLinks = new List<SightRouteLink>();

        sightRouteLinks.Add(new SightRouteLink { Id = 1, RouteId = 1, SightId = 1 });
        sightRouteLinks.Add(new SightRouteLink { Id = 2, RouteId = 1, SightId = 2 });
        sightRouteLinks.Add(new SightRouteLink { Id = 3, RouteId = 1, SightId = 3 });
        sightRouteLinks.Add(new SightRouteLink { Id = 4, RouteId = 2, SightId = 2 });
        sightRouteLinks.Add(new SightRouteLink { Id = 5, RouteId = 2, SightId = 3 });

        this.sqlite_conn.InsertAll(sightRouteLinks);

    }

    // Other database operations can be added here

    public void CloseConnection()
    {
        // Close the connection when done
        this.sqlite_conn.Close();
    }
}