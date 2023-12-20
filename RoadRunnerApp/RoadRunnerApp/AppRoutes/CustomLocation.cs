using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.AppRoutes
{
    public class CustomLocation
    {
        public double latitude { get; }
        public double longitude { get; }

        public CustomLocation(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

    }
}
