using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.AppRoutes
{
    public class Location
    {
        public float latitude { get; }
        public float longitude { get; }

        public Location(float longitude, float latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }

    }
}
