using SQLite;

namespace AppDatabase;

public class Sight
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Categories Category { get; set; }
    public string ImgFilePath { get; set; }
}

public enum Categories
{
    Museum,
    Park,
    Monument,
    // Add more categories as needed
}

public class Route
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Difficulties Difficulty { get; set; }
}

public enum Difficulties
{
    Easy,
    Moderate,
    Hard,
}

public class SightRouteLink
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    // Foreign keys referencing Sight and Route tables
    public int SightId { get; set; }
    public int RouteId { get; set; }
}