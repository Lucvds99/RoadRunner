using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mlocation = Microsoft.Maui.Devices.Sensors.Location;


namespace RoadRunnerApp.AppRoutes
{
    public interface IRouteService
    {

        event EventHandler<List<Mlocation>> CoordinatesReceived;
        Task GetRouteCoordinates();

    }
}
