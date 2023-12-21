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
        public event EventHandler<List<Mlocation>> ReverseCoordinatesReceived;


        // For route determination from user to start location (backtracking)
        private Mlocation _initialStartLocation;
        private List<Landmark> _passedLandmarks;

        public List<Mlocation> decodedCoordinates { get; set; }

        public RouteManager()
        {
           
        }
        // To Do: Je moeder.

        private static string url = "https://routes.googleapis.com/directions/v2:computeRoutes";


        public async Task<string> GetHTTPRequest(string json)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage message = new HttpRequestMessage();

            message.Content = new StringContent(json);
            message.Headers.Add("X-Goog-Api-Key", "AIzaSyBXG_XrA3JRTL58osjxd0DbqH563e2t84o");
            message.Headers.Add("X-Goog-FieldMask", "routes.duration,routes.distanceMeters,routes.polyline.encodedPolyline");
            message.RequestUri = new Uri(url);
            message.Method = HttpMethod.Post;

            HttpResponseMessage response = client.SendAsync(message).Result;

            response.EnsureSuccessStatusCode();
            Trace.WriteLine("Bozo: " + response.Content.ReadAsStringAsync().Result);

            string requestResult = response.Content.ReadAsStringAsync().Result;
            return requestResult;
        }

        public async Task<List<Mlocation>> GetDecodedRoute(List<Landmark>landmarks, Mlocation userLocation)
        {
            List<Landmark> copiedLandmarks = new List<Landmark>(landmarks);


            Landmark original = landmarks[0];
            Landmark destination = landmarks.Last();

            // Remove first and last landmarks so we can have all the intermediate landmarks
            copiedLandmarks.Remove(copiedLandmarks.First<Landmark>());
            copiedLandmarks.Remove(copiedLandmarks.Last<Landmark>());

            Waypoint[] intermediatePoints = new Waypoint[copiedLandmarks.Count];

            for (int i = 0; i < copiedLandmarks.Count; i++)
            {         
                Landmark currentLandmark = copiedLandmarks[i];
                intermediatePoints[i] = new Waypoint(new Location(currentLandmark.location.latitude, currentLandmark.location.longitude), currentLandmark.name, "address");
            }

            Request routeRequest = new Request
            {
               
                origin = new Waypoint(new Location(userLocation.Latitude, userLocation.Longitude), "user", "banaan"),
                intermediates = intermediatePoints,
                destination = new Waypoint(new Location(destination.location.latitude, destination.location.longitude), "banaan", "banaan"),
                travelMode = "WALK"
                //intermediates = new Waypoint[] { new Waypoint(new Location(51.5941117, 4.7794167), "henk", "gayweg 7") }
            };

            string json = JsonConvert.SerializeObject(routeRequest);

            Trace.WriteLine("Bozo: " + json);

            string requestResult = GetHTTPRequest(json).Result;

            decodedCoordinates = GetDecodedLocations(GetEncodedPolylines(requestResult));
            return decodedCoordinates;
        }



        public async Task<List<Mlocation>> GetDecodedReverseRoute(List<Landmark> landmarks, Mlocation userLocation)
        {
            if (_initialStartLocation == null)
                return null;


                Mlocation originalStart = _initialStartLocation;
            


            Request routeRequest = new Request
            {

                origin = new Waypoint(new Location(userLocation.Latitude, userLocation.Longitude), "user", "banaan"),
                intermediates = [],
                destination = new Waypoint(new Location(originalStart.Latitude, originalStart.Longitude), "banaan", "banaan"),
                travelMode = "WALK"
                //intermediates = new Waypoint[] { new Waypoint(new Location(51.5941117, 4.7794167), "henk", "gayweg 7") }
            };

            string json = JsonConvert.SerializeObject(routeRequest);

            Trace.WriteLine("BozoJsonReverse: " + json);

            string requestResult = GetHTTPRequest(json).Result;

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

        public async Task GetRouteCoordinates(List<Landmark> landmarks, Mlocation userLocation)
        {
    
            List<Mlocation> decodedCoords = await GetDecodedRoute(landmarks, userLocation);

            // Coordinates are recieved, trigger event
            CoordinatesReceived?.Invoke(this, decodedCoords);

            // Set initial start location so route can be backtracked dynamically 
            if (_initialStartLocation == null) 
            {
                _initialStartLocation = userLocation;
            }

        }

        public async Task GetReverseRouteCoordinates(List<Landmark> landmarks, Mlocation userLocation)
        {
            List<Mlocation> decodedCoords = await GetDecodedReverseRoute(landmarks, userLocation);

            // Coordinates are recieved, trigger event
            ReverseCoordinatesReceived?.Invoke(this, decodedCoords);

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


