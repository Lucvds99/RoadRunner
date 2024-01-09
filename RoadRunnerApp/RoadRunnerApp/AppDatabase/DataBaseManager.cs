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
            Description = "Het Oude pand van de VVV, meer kunnen we er niet over zeggen. Weinig geschiedenis hiero.",
            ImgFilePath = "vvv.png"
        });
        sights.Add(new Sight
        {
            Id = 2,
            Longitude = 4.7793883333,
            Latitude = 51.5932783333,
            Name = "Liefdeszuster",
            Description = "Mijn zuster houd van liefde, ieder mens heeft het weleens nodig, dit monument heeft het!",
            ImgFilePath = "liefdeszuster2.png"
        });
        sights.Add(new Sight
        {
            Id = 3,
            Longitude = 4.779695,
            Latitude = 51.5925,
            Name = "Nassau Baronie Monument",
            Description = "De baron van naussau kreeg ooit een monument, nu staat het er en doen we deze speurtocht.",
            Category = Categories.Monument,
            ImgFilePath = "nassaubaroniemonument.png"
        });

        sights.Add(new Sight
        {
            Id = 4,
            Longitude = 4.7784716667,
            Latitude = 51.5928333333,
            Name = "The Light House",
            Description = "Een huis van licht. Om ervoor te zorgen dat er licht is, is er een huis gebouwd voor licht. (Let op, in het donker!)",
            ImgFilePath = "lighthouse3.png"
        });

        sights.Add(new Sight
        {
            Id = 5,
            Longitude = 4.7761666667,
            Latitude = 51.5906116667,
            Name = "Kasteel van Breda",
            Description = "Om de oude romeinen vanuit rome niet naar breda te laten trekken is er een kasteel gebouwd. Een graaf heeft dit bedacht ofzo.",
            ImgFilePath = "kasteelbreda5.png"
        });
        sights.Add(new Sight
        {
            Id =6,
            Longitude = 4.7761383333,
            Latitude = 51.589695,
            Name = "Stadhouderspoort",
            Description = "De stadhouder had een poort nodig, dus die kreeg hij voor kerstmis.",
            ImgFilePath = "stadhouderspoort6.png"
        });
        sights.Add(new Sight
        {
            Id = 7,
            Longitude = 4.7743616667,
            Latitude = 51.5900283333,
            Name = "Huis van Brecht",
            Description = "We weten niet wie Brecht is, maar ze heeft wel een mooi huis.",
            ImgFilePath = "huisvanbrecht7.png"
        });
        sights.Add(new Sight
        {
            Id = 8,
            Longitude = 4.773445,
            Latitude = 51.590195,
            Name = "Spanjaardsgat",
            Description = "Het kasteel van breda was gebouwd om ook spanjaarden weg te houden uit de stad,\n er kwamen teveel dus werd er een gat gegraven.",
            ImgFilePath = "spanjaardsgat8.png"
        });
        sights.Add(new Sight
        {
            Id = 9,
            Longitude = 4.7733333333,
            Latitude = 51.5898333333,
            Name = "Begin Vismarkt",
            Description = "Garnaal en pizza kun je hier ook vinden. Vis is optioneel. Het uitzicht is wel mooi.",
            ImgFilePath = "vismarkt9.png"
        });
        sights.Add(new Sight
        {
            Id = 10,
            Longitude = 4.774445,
            Latitude = 51.5893616667,
            Name = "Begin Havermarkt",
            Description = "Deze markt heeft geen vis, maar wel havermout.",
            ImgFilePath = "havermarkt10.png"
        });
        sights.Add(new Sight
        {
            Id = 11,
            Longitude = 4.7802783333,
            Latitude = 51.5888333333,
            Name = "Grote Kerk",
            Description = "De kleine kerk stond er al. De geschiedenis van deze kerk kan je vinden op google maps.",
            ImgFilePath = "grotekerk11.png"
        });
        sights.Add(new Sight
        {
            Id = 12,
            Longitude = 4.7751383333,
            Latitude = 51.588195,
            Name = "Het Poortje",
            Description = "Wie heeft dit verzonnen.",
            ImgFilePath = ""
        });
        sights.Add(new Sight
        {
            Id = 13,
            Longitude = 4.77575,
            Latitude = 51.5870833333,
            Name = "Ridderstraat",
            Description = "Deze straat is gemaakt in de tijd van de piraten, maar piratenstraat klinkt zo stom...",
            ImgFilePath = "ridderstraat13.png"
        });
        sights.Add(new Sight
        {
            Id = 14,
            Longitude = 4.776555,
            Latitude = 51.5874166667,
            Name = "Grote Markt",
            Description = "Kleine markt bestond al, maar deze markt is wel groot ja. Daarom heet het de 'grote markt'.  ",
            ImgFilePath = "grotemarkt12.png"
        });
        sights.Add(new Sight
        {
            Id = 15,
            Longitude = 4.7763333333,
            Latitude = 51.5880283333,
            Name = "Bevrijdingsmonument",
            Description = "Bij het betreden van het monument hoort een authentieke saluut.",
            Category = Categories.Monument,
            ImgFilePath = "bevrijdingsmonument15.png"
        });
        sights.Add(new Sight
        {
            Id = 16,
            Longitude = 4.7761116667,
            Latitude = 51.58875,
            Name = "Stadhuis",
            Description = "Het huis van de stad, level (7), word volgende week geupgrade. We komen nog 1400 goud te kort.",
            ImgFilePath = "stadhuis16.png"
        });

        sights.Add(new Sight
        {
            Id = 17,
            Longitude = 4.77725,
            Latitude = 51.5876383333,
            Name = "Antonius van Paduakerk",
            Description = "Wie is anton. Maar de kerk is wel mooi.",
            ImgFilePath = "antonius17.png"
        });

        sights.Add(new Sight
        {
            Id = 18,
            Longitude = 4.778945,
            Latitude = 51.588,
            Name = "Bibliotheek",
            Description = "Deze plek is afgeraden voor blinde mensen.",
            ImgFilePath = "bieb18.png"
        });
        sights.Add(new Sight
        {
            Id = 19,
            Longitude = 4.7810283333,
            Latitude = 51.5877216667,
            Name = "Kloosterkazerne",
            Description = "Hier wonen nonnen, die ook kunnen vechten.",
            ImgFilePath = "kloosterkazerne19.png"
        });
        sights.Add(new Sight
        {
            Id = 20,
            Longitude = 4.782,
            Latitude = 51.58775,
            Name = "Chasse theater",
            Description = "Voorstelling, filmpje, tentoonstelling, wacht die niet, ehh, maar wel entertainment enzo. Is wel leuk. ",
            ImgFilePath = "chasse1.png"
        });

        sights.Add(new Sight
        {
            Id = 21,
            Longitude = 4.7808883333,
            Latitude = 51.5886116667,
            Name = "Binding van Isaac",
            Description = "Wie is isaac?",
            ImgFilePath = "hasbulla.jpg"
        });
        sights.Add(new Sight
        {
            Id = 22,
            Longitude = 4.781,
            Latitude = 51.5896666667,
            Name = "Beyerd",
            Description = "Hier komen bussen langs.",
            ImgFilePath = "beyerd22.png"
        });
        sights.Add(new Sight
        {
            Id = 23,
            Longitude = 4.78,
            Latitude = 51.589555,
            Name = "Gasthuispoort",
            Description = "Gasten zijn welkom. Het is wel een poort, dus als je wilt schuilen voor de regen is er gelimiteerd plek.",
            ImgFilePath = "gasthuispoort23.png"
        });

        sights.Add(new Sight
        {
            Id = 24,
            Longitude = 4.777945,
            Latitude = 51.5891116667,
            Name = "Willem Merkxtuin",
            Description = "Een mooie tuin, waarom dit een monument is, geen idee. Dit is eigenlijk de achtertuin van gewoon een paar mensen.",
            ImgFilePath = "willem24.png"
        });

        sights.Add(new Sight
        {
            Id = 25,
            Longitude = 4.7783616667,
            Latitude = 51.589695,
            Name = "Begijnenhof",
            Description = "Dit is een hof. Lekker zelf weten.",
            ImgFilePath = "hof25.png"
        });
        sights.Add(new Sight
        {
            Id = 26,
            Longitude = 4.77625,
            Latitude = 51.5895,
            Name = "Einde stadswandeling",
            Description = "Yippie! Nu een voldoende eisen. Anders pak slaag.",
            ImgFilePath = "monke.png"
        });

        this.sqlite_conn.InsertAll(sights);

        // TO DO: Remove old records from DB.


        List<Route> routes = new List<Route>();
        routes.Add(new Route
        {
            Id = 1,
            Name = "Test route",
            Description = "De main route die voor het tentamen gelopen gaat worden. Hierin zitten vele culturele geweldige bezienswaardigheden.",
            Difficulty = Difficulties.Easy
        });

        this.sqlite_conn.InsertAll(routes);

        List<SightRouteLink> sightRouteLinks = new List<SightRouteLink>();

        sightRouteLinks.Add(new SightRouteLink { Id = 1, RouteId = 1, SightId = 1 });
        sightRouteLinks.Add(new SightRouteLink { Id = 2, RouteId = 1, SightId = 2 });
        sightRouteLinks.Add(new SightRouteLink { Id = 3, RouteId = 1, SightId = 3 });
        sightRouteLinks.Add(new SightRouteLink { Id = 4, RouteId = 1, SightId = 4 });
        sightRouteLinks.Add(new SightRouteLink { Id = 5, RouteId = 1, SightId = 5 });
        sightRouteLinks.Add(new SightRouteLink { Id = 6, RouteId = 1, SightId = 6 });
        sightRouteLinks.Add(new SightRouteLink { Id = 7, RouteId = 1, SightId = 7 });
        sightRouteLinks.Add(new SightRouteLink { Id = 8, RouteId = 1, SightId = 8 });
        sightRouteLinks.Add(new SightRouteLink { Id = 9, RouteId = 1, SightId = 9 });
        sightRouteLinks.Add(new SightRouteLink { Id = 10, RouteId = 1, SightId = 10 });
        sightRouteLinks.Add(new SightRouteLink { Id = 11, RouteId = 1, SightId = 11 });
        sightRouteLinks.Add(new SightRouteLink { Id = 12, RouteId = 1, SightId = 12 });
        sightRouteLinks.Add(new SightRouteLink { Id = 13, RouteId = 1, SightId = 13 });
        sightRouteLinks.Add(new SightRouteLink { Id = 14, RouteId = 1, SightId = 14 });
        sightRouteLinks.Add(new SightRouteLink { Id = 15, RouteId = 1, SightId = 15 });
        sightRouteLinks.Add(new SightRouteLink { Id = 16, RouteId = 1, SightId = 16 });
        sightRouteLinks.Add(new SightRouteLink { Id = 17, RouteId = 1, SightId = 17 });
        sightRouteLinks.Add(new SightRouteLink { Id = 18, RouteId = 1, SightId = 18 });
        sightRouteLinks.Add(new SightRouteLink { Id = 19, RouteId = 1, SightId = 19 });
        sightRouteLinks.Add(new SightRouteLink { Id = 20, RouteId = 1, SightId = 20 });
        sightRouteLinks.Add(new SightRouteLink { Id = 21, RouteId = 1, SightId = 21 });
        sightRouteLinks.Add(new SightRouteLink { Id = 22, RouteId = 1, SightId = 22 });
        sightRouteLinks.Add(new SightRouteLink { Id = 23, RouteId = 1, SightId = 23 });
        sightRouteLinks.Add(new SightRouteLink { Id = 24, RouteId = 1, SightId = 24 });
        sightRouteLinks.Add(new SightRouteLink { Id = 25, RouteId = 1, SightId = 25 });
        sightRouteLinks.Add(new SightRouteLink { Id = 26, RouteId = 1, SightId = 26 });


        this.sqlite_conn.InsertAll(sightRouteLinks);

    }

    // Other database operations can be added here

    public void CloseConnection()
    {
        // Close the connection when done
        this.sqlite_conn.Close();
    }
}