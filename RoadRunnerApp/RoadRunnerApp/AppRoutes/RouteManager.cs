using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Google.Api.Gax.Grpc;
using Google.Maps.Routing.V2;
using Google.Type;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using static Google.Rpc.Context.AttributeContext.Types;
using PolylineEncoder.Net.Utility;
using Mlocation = Microsoft.Maui.Devices.Sensors.Location;




namespace RoadRunnerApp.AppRoutes
{
    public class RouteManager : IRouteService
    {

        public event EventHandler<List<Mlocation>> CoordinatesReceived;
        public event EventHandler<List<Landmark>> LandmarksRecieved;

        public List<Mlocation> decodedCoordinates { get; set; }

        public RouteManager()
        {
            

        
        }
        // To Do: Je moeder.

        private static string url = "https://routes.googleapis.com/directions/v2:computeRoutes";

        public async Task<List<Mlocation>> HttpRequest(List<Landmark>landmarks)
        {
            Landmark original = landmarks[0];
            Landmark destination = landmarks.Last();
            //string jsonString = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            Request routeRequest = new Request
            {
               
                origin = new Waypoint(new Location(original.location.latitude, original.location.longitude), "banaan", "banaan"),
                intermediates = [new Waypoint(new Location(51.5906117, 4.7761667), "Kasteel nibba", "Katoenstraat 12")],
                destination = new Waypoint(new Location(destination.location.latitude, destination.location.longitude), "banaan", "banaan"),
                travelMode = "WALK"
                //intermediates = new Waypoint[] { new Waypoint(new Location(51.5941117, 4.7794167), "henk", "gayweg 7") }
            };

            string json = JsonConvert.SerializeObject(routeRequest);

            Trace.WriteLine("Bozo: " + json);


            HttpClient client = new HttpClient();

            HttpRequestMessage message = new HttpRequestMessage();

            message.Content = new StringContent(json);
            message.Headers.Add("X-Goog-Api-Key", "AIzaSyBXG_XrA3JRTL58osjxd0DbqH563e2t84o");
            message.Headers.Add("X-Goog-FieldMask", "routes.duration,routes.distanceMeters,routes.polyline.encodedPolyline");
            message.RequestUri = new Uri(url);
            message.Method = HttpMethod.Post;
       
            HttpResponseMessage response = await client.SendAsync(message);

            response.EnsureSuccessStatusCode();
            Trace.WriteLine("Bozo: " + response.Content.ReadAsStringAsync().Result);

            string requestResult = response.Content.ReadAsStringAsync().Result;

            decodedCoordinates = GetDecodedLocations(GetEncodedPolylines(requestResult));
            return decodedCoordinates;
        }


        public List<string> GetEncodedPolylines(string httpResult)
        {

            JObject jsonObject = new JObject(JObject.Parse(httpResult));
            JArray routes = (JArray)(jsonObject["routes"]);

            List<string> encodedPolylines = new();

            foreach (JObject route in routes)
            {
                JObject polyline = (JObject)route.GetValue("polyline");
                string encodedPolyline = polyline.GetValue("encodedPolyline").ToString();
                encodedPolylines.Add(encodedPolyline);
            }

            return encodedPolylines;
        }

        public List<Mlocation> GetDecodedLocations(List<string> encodedPolyLines)
        {
            List<Mlocation> routeLocations = new List<Mlocation>();
            PolylineUtility decoder = new PolylineUtility();

            var decodedpoints = decoder.Decode(encodedPolyLines[0]);

            foreach (var coordinate in decodedpoints)
            {
                routeLocations.Add(new Mlocation(coordinate.Latitude, coordinate.Longitude));
            }

            return routeLocations;
        }

        public async Task GetRouteCoordinates(List<Landmark> landmarks)
        {
    
            List<Mlocation> decodedCoords = await HttpRequest(landmarks);

            // Coordinates are recieved, trigger event
            CoordinatesReceived?.Invoke(this, decodedCoords);

        }

        // Retrieve landmarks from database
        public void GetLandmarks() //Change to Task later
        {
            // To do: Link this method with database so we get actual landmarks from DB
            
            
            // Test List
            List<Landmark> retrievedLandmarks = new List<Landmark>();
            retrievedLandmarks.Add(new Landmark(1, "Chasse theater", "oulleh", "eets", new AppRoutes.CustomLocation(51.58775, 4.782)));
            retrievedLandmarks.Add(new Landmark(1, "VVV-pand", "oulleh", "eets", new AppRoutes.CustomLocation(51.5941117, 4.7794167)));
            retrievedLandmarks.Add(new Landmark(1, "Nassau Baronie Monument", "oulleh", "eets", new AppRoutes.CustomLocation(51.5925, 4.779695)));
            retrievedLandmarks.Add(new Landmark(1, "Kasteel van Breda", "oulleh", "eets", new AppRoutes.CustomLocation(51.5906117, 4.7761667)));
            retrievedLandmarks.Add(new Landmark(1, "Grote Kerk", "oulleh", "eets", new AppRoutes.CustomLocation(51.5888333, 4.775278)));
            retrievedLandmarks.Add(new Landmark(1, "Bevrijdingsmonument", "oulleh", "eets", new AppRoutes.CustomLocation(51.5880283, 4.7763333)));
            retrievedLandmarks.Add(new Landmark(1, "Stadhuis", "oulleh", "eets", new AppRoutes.CustomLocation(51.58875, 4.7761111)));


            LandmarksRecieved?.Invoke(this, retrievedLandmarks);
        }
    }

}


