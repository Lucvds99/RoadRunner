using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.AppRoutes
{
    public class Landmark
    {
        public int id { get; }
        public string name { get; }
        public string description { get; }
        public string theme { get; }
        public Location location { get; }

        // Constructor Ouleh
        public Landmark(int id, string name, string description, string theme, Location location)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.theme = theme;
            this.location = location;
        }



    }
}
