namespace AppDatabase;

public class QueryBuilder
{
    public string GetAllSights()
    {
        return "SELECT * FROM Sight";
    }

    public string GetAllRoutes()
    {
        return "SELECT * FROM Route";
    }

    public string GetSightsInRoute(int routeId)
    {
        return "SELECT Sight.* " +
                       "FROM Sight " +
                       "JOIN SightRouteLink ON Sight.Id = SightRouteLink.SightId " +
                       "WHERE SightRouteLink.RouteId = " + routeId + " ORDER BY SightRouteLink.Id";
    }
}