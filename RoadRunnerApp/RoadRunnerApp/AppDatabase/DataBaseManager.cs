using SQLite;

namespace AppDatabase;

class DatabaseManager
{
    private QueryBuilder _queryBuilder;
    private SQLiteConnection sqlite_conn;

    public DatabaseManager()
    {
        _queryBuilder = new QueryBuilder();
        try
        {
            this.sqlite_conn = new SQLiteConnection("database.db");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during database initialization: {ex.Message}");
            throw; // Rethrow the exception to ensure it's not silently ignored
        }

        //CreateTables();
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

    private void CreateTables()
    {
        // Create tables for your data models
        this.sqlite_conn.CreateTable<Sight>();
        this.sqlite_conn.CreateTable<Route>();
        this.sqlite_conn.CreateTable<SightRouteLink>();
    }

    // Other database operations can be added here

    public void CloseConnection()
    {
        // Close the connection when done
        this.sqlite_conn.Close();
    }
}