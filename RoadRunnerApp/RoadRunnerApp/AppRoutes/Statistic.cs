using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.AppRoutes
{
    public class Statistic
    {
        private double distance { get; }
        private int travelTime { get; }
        private double averageSpeed { get; }
        private int timesVisited { get; set; }

        public Statistic(double distance, int travelTime, double averageSpeed)
        {
            this.distance = distance;
            this.travelTime = travelTime;
            this.averageSpeed = averageSpeed;
        }

    }
}
