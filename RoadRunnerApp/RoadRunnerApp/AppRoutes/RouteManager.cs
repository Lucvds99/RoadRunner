using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Google.Api.Gax.Grpc;
using Google.Maps.Routing.V2;
using Google.Type;
using Glocation = Google.Maps.Routing.V2.Location;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;



namespace RoadRunnerApp.AppRoutes
{
    public class RouteManager
    {
        public RouteManager()
        {
            SendHttpRequest();

        }
        // To Do: Je moeder.

        private string url = "https://routes.googleapis.com/directions/v2:computeRoutes";


        //public async void SendTest()
        //{
        //    //new Mlocation(51.58775f, 4.782f),
        //    //new Mlocation(51.5941117f, 4.7794167f),
        //    //new Mlocation(51.5925f, 4.7794167f),


        //    Request routeRequest = new Request
        //    {
        //        origin = new Waypoint(new Location(51.58775, 4.782), "Chassé Theater", "penisstraat 7"),
        //        destination = new Waypoint(new Location(51.5925, 4.7794167), "Purple rain", "piemelstraat 9"),
        //        travelMode = RouteTravelMode.WALK
        //    };

        //}

        public async void SendHttpRequest()
        {

     
            //string jsonString = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            Request routeRequest = new Request
            {
                origin = new Waypoint(new Location(51.58775, 4.782), "Chassé Theater", "penisstraat 7"),
                destination = new Waypoint(new Location(51.5925, 4.7794167), "Purple rain", "piemelstraat 9"),
                //travelMode = RouteTravelMode.WALK.ToString(),
                //intermediates = new Waypoint[] { new Waypoint(new Location(51.5941117, 4.7794167), "henk", "gayweg 7") }
            };

            string json = JsonConvert.SerializeObject(routeRequest);

            Trace.WriteLine("Bozo: " + json);


            HttpClient client = new HttpClient();

            HttpRequestMessage message = new HttpRequestMessage();
            //message.Headers.Add("Content-Type", "application/json");
            message.Content = new StringContent(json);
            message.Headers.Add("X-Goog-Api-Key", "AIzaSyBXG_XrA3JRTL58osjxd0DbqH563e2t84o");
            message.Headers.Add("X-Goog-FieldMask", "routes.duration,routes.distanceMeters,routes.polyline.encodedPolyline");
            message.RequestUri = new Uri(url);
            message.Method = HttpMethod.Post;

            HttpResponseMessage response = await client.SendAsync(message);

            response.EnsureSuccessStatusCode();
            Trace.WriteLine("Bozo: " + response.Content.ReadAsStringAsync().Result);

        }

    }

}


