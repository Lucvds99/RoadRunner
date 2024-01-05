using Microsoft.Maui.Controls.PlatformConfiguration;
using SQLite;

namespace RoadRunnerApp.AppDatabase;

public class DatabaseManager
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
            Longitude = 4.7794166667,
            Latitude = 51.5941116667,
            Name = "Oude VVV-pand",
            Description = "Het Oude pand van de VVV",
            ImgFilePath = "1"
        });
        sights.Add(new Sight
        {
            Id = 2,
            Longitude = 4.7793883333,
            Latitude = 51.5932783333,
            Name = "Liefdeszuster",
            Description = "",
            ImgFilePath = "3"
        });
        sights.Add(new Sight
        {
            Id = 3,
            Longitude = 4.779695,
            Latitude = 51.5925,
            Name = "Nassau Baronie Monument",
            Description = "",
            Category = Categories.Monument,
            ImgFilePath = "4"
        });

        sights.Add(new Sight
        {
            Id = 4,
            Longitude = 4.7784716667,
            Latitude = 51.5928333333,
            Name = "The Light House",
            Description = "",
            ImgFilePath = "5"
        });

        sights.Add(new Sight
        {
            Id = 5,
            Longitude = 4.7761666667,
            Latitude = 51.5906116667,
            Name = "Kasteel van Breda",
            Description = "",
            ImgFilePath = "9"
        });
        sights.Add(new Sight
        {
            Id =6,
            Longitude = 4.7761383333,
            Latitude = 51.589695,
            Name = "Stadhouderspoort",
            Description = "",
            ImgFilePath = "11"
        });
        sights.Add(new Sight
        {
            Id = 7,
            Longitude = 4.7743616667,
            Latitude = 51.5900283333,
            Name = "Huis van Brecht",
            Description = "",
            ImgFilePath = "6"
        });
        sights.Add(new Sight
        {
            Id = 8,
            Longitude = 4.773445,
            Latitude = 51.590195,
            Name = "Spanjaardsgat",
            Description = "",
            ImgFilePath = "20"
        });
        sights.Add(new Sight
        {
            Id = 9,
            Longitude = 4.7733333333,
            Latitude = 51.5898333333,
            Name = "Begin Vismarkt",
            Description = "",
            ImgFilePath = "19"
        });
        sights.Add(new Sight
        {
            Id = 10,
            Longitude = 4.774445,
            Latitude = 51.5893616667,
            Name = "Begin Havermarkt",
            Description = "",
            ImgFilePath = ""
        });
        sights.Add(new Sight
        {
            Id = 11,
            Longitude = 4.7802783333,
            Latitude = 51.5888333333,
            Name = "Grote Kerk",
            Description = "",
            ImgFilePath = "17"
        });
        sights.Add(new Sight
        {
            Id = 12,
            Longitude = 4.7751383333,
            Latitude = 51.588195,
            Name = "Het Poortje",
            Description = "",
            ImgFilePath = ""
        });
        sights.Add(new Sight
        {
            Id = 13,
            Longitude = 4.77575,
            Latitude = 51.5870833333,
            Name = "Ridderstraat",
            Description = "",
            ImgFilePath = "17"
        });
        sights.Add(new Sight
        {
            Id = 14,
            Longitude = 4.776555,
            Latitude = 51.5874166667,
            Name = "Grote Markt",
            Description = "",
            ImgFilePath = "24"
        });
        sights.Add(new Sight
        {
            Id = 15,
            Longitude = 4.7763333333,
            Latitude = 51.5880283333,
            Name = "Bevrijdingsmonument",
            Description = "",
            Category = Categories.Monument,
            ImgFilePath = ""
        });
        sights.Add(new Sight
        {
            Id = 16,
            Longitude = 4.7761116667,
            Latitude = 51.58875,
            Name = "Stadhuis",
            Description = "",
            ImgFilePath = ""
        });

        sights.Add(new Sight
        {
            Id = 17,
            Longitude = 4.77725,
            Latitude = 51.5876383333,
            Name = "Antonius van Paduakerk",
            Description = "",
            ImgFilePath = ""
        });

        sights.Add(new Sight
        {
            Id = 18,
            Longitude = 4.778945,
            Latitude = 51.588,
            Name = "Bibliotheek",
            Description = "",
            ImgFilePath = ""
        });
        sights.Add(new Sight
        {
            Id = 19,
            Longitude = 4.7810283333,
            Latitude = 51.5877216667,
            Name = "Kloosterkazerne",
            Description = "",
            ImgFilePath = ""
        });
        sights.Add(new Sight
        {
            Id = 20,
            Longitude = 4.782,
            Latitude = 51.58775,
            Name = "Chasse theater",
            Description = "",
            ImgFilePath = ""
        });

        sights.Add(new Sight
        {
            Id = 21,
            Longitude = 4.7808883333,
            Latitude = 51.5886116667,
            Name = "Binding van Isaac",
            Description = "",
            ImgFilePath = ""
        });
        sights.Add(new Sight
        {
            Id = 22,
            Longitude = 4.781,
            Latitude = 51.5896666667,
            Name = "Beyerd",
            Description = "",
            ImgFilePath = ""
        });
        sights.Add(new Sight
        {
            Id = 23,
            Longitude = 4.78,
            Latitude = 51.589555,
            Name = "Gasthuispoort",
            Description = "",
            ImgFilePath = ""
        });

        sights.Add(new Sight
        {
            Id = 24,
            Longitude = 4.777945,
            Latitude = 51.5891116667,
            Name = "Willem Merkxtuin",
            Description = "",
            ImgFilePath = ""
        });

        sights.Add(new Sight
        {
            Id = 25,
            Longitude = 4.7783616667,
            Latitude = 51.589695,
            Name = "Begijnenhof",
            Description = "",
            ImgFilePath = ""
        });
        sights.Add(new Sight
        {
            Id = 26,
            Longitude = 4.77625,
            Latitude = 51.5895,
            Name = "Einde stadswandeling",
            Description = "",
            ImgFilePath = ""
        });

        this.sqlite_conn.InsertAll(sights);

        // TO DO: Remove old records from DB.


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