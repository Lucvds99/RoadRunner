using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors;


namespace RoadRunnerApp.AppRoutes
{
    public class Route
    {
        private List<Landmark> landmarks;
        private List<CustomLocation> path;
        private Statistic statistic;
       
        public Route(List<Landmark> landmarks, List<CustomLocation> path)
        {
            this.landmarks = landmarks;
            this.path = path;

           // Statistic stat = new Statistic();

            CalculateDistance();
        }

        private double CalculateDistance()
        {
            for (int i = 0; i <= path.Count; i++)
            {
                
            }
            return 0;
        }


    }
}
