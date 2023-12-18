using Metal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.AppRoutes
{
    internal class Landmark
    {
        public int id { get; }
        private string name { get; }
        private string description { get; }
        private string theme { get; }
        private Location location { get; }

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
