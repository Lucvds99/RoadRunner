using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.AppRoutes
{
    public class Location
    {
        public double latitude { get; }
        public double longitude { get; }

        public Location(double longitude, double latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }

    }
}
