using RoadRunnerApp.AppDatabase;
using System.Reflection;

namespace RoadRunnerApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            //var database = new DatabaseManager();

            //Console.WriteLine(database.GetAllSights().Count);

            //Console.WriteLine("All Sights");
            //foreach (var sight in database.GetAllSights())
            //{
            //    Console.WriteLine(sight.Name);
            //}
            //Console.WriteLine("All Routes");
            //foreach (var route in database.GetAllRoutes())
            //{
            //    Console.WriteLine(route.Name);
            //}
            //Console.WriteLine("All Sights By Route 2");
            //foreach (var sight in database.GetSightsInRoute(2))
            //{
            //    Console.WriteLine(sight.Name);
            //}
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
