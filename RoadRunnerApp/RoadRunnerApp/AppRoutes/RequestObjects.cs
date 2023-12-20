using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace RoadRunnerApp.AppRoutes
{

    
    public class Request
    {
        public Waypoint origin { get; set; }
        public Waypoint destination { get; set; }
        public Waypoint[] intermediates { get; set; } // Waypoints along the route, for either stopping/passing by. Up to 25 intermediate waypoints are supported.
        public string travelMode { get; set; } // Current application only uses WALK as travelmode
    }

    public class Waypoint
    {
        public bool via { get; set; }
        public bool vehicleStopover {  get; set; }
        public bool sideOfRoad { get; set; }
        public Location location {  get; set; }
        public string placeId {  get; set; }
        public string address { get; set; }

        public Waypoint(Location location, string placeId, string adress)
        {
            //this.via = false;
            //this.vehicleStopover = false;
            //this.sideOfRoad = false;
            this.location = location;
            //this.placeId = placeId;
            //this.address = adress;
        }

    }

    public enum RouteTravelMode
    {
        TRAVEL_MODE_UNSPECIFIED,
        DRIVE,
        BICYCLE,
        WALK,
        TWO_WHEELER,
        TRANSIT
    }

    public class Location
    {
        public object latLng { get; set; }

        public Location(double latitude, double longitude)
        {
            this.latLng = new
            {
                latitude = latitude,
                longitude = longitude
            };
            
        }
    }
    
}
