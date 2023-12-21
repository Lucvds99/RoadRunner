using RoadRunnerApp.UIControllers;
using RoadRunnerApp.Views;

namespace RoadRunnerApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            User user = new User();
            NavigationPage navigationPage = new NavigationPage(new LanguageSelectionPage(user));
            NavigationPage.SetHasBackButton(navigationPage.CurrentPage, false);

            //I want to turn off the back button for navigation Page I dont know how to DO that 
            MainPage = navigationPage;
        }
    }
}
