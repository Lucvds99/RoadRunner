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
        event EventHandler<List<Landmark>> LandmarksRecieved;
        event EventHandler<List<Mlocation>> CoordinatesReceived;

        void GetLandmarks();
        Task GetRouteCoordinates(List<Landmark> landmarks, Mlocation userLocation);


    }
}
