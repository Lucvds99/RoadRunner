using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.AppRoutes
{
    public class CustomLocation
    {
        public float latitude { get; }
        public float longitude { get; }

        public CustomLocation(float longitude, float latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }

    }
}
