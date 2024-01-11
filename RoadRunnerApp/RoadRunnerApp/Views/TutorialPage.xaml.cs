using RoadRunnerApp.UIControllers;
using RoadRunnerApp.Views.Models;
using RoadRunnerApp.AppRoutes;

namespace RoadRunnerApp.Views;

public partial class TutorialPage : ContentPage
{
    User user;
    List<Landmark> landmarksVisited;
    List<Landmark> landmarksToVisit;
	public TutorialPage(User user)
	{
        this.landmarksVisited = new List<Landmark>();
        this.landmarksToVisit = new List<Landmark>();
        this.user = user;
        string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in vol";

        InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);

        //Items for in the Carousel in Tutorial page
        var items = new List<CarouselItem>
        {
            new CarouselItem {Title = "Navigatie", Description = "de buttons onder op de map Page worden gebruikt om te navigeren naar de andere pagina's. de linker pagina wordt gebruikt voor de individuele monumenten. De rechter pagina wordt gebruikt voor de Routes. En de middelste pagina is de map.", Image = "testpicture.png", hasButton=false},
            new CarouselItem {Title = "Route", Description = "De bezienswaardig heden in een route worden weergegeven met een rode marker. als je naar de marker loopt krijg je binnen een afstand van 15 meter een popup met informatie over de Locatie", Image = "testpicture.png", hasButton=false},
            new CarouselItem {Title = "ben jij er klaar voor ? ", Description = "ben jij klaar voor de route die jij gaat lopen in de mooie stad Breda!", Image = "", Button = "continue", hasButton=true}
        };
        carouselView.ItemsSource = items;
    }
    public void onButtonClick(object sender, EventArgs e)
    {
        //TODO goes to the Map Page
        Navigation.PushAsync(new MapPage(landmarksVisited, landmarksToVisit, null));
    }

    public void onButtonClickSkip(object sender, EventArgs e)
    {
        //TODO goes to the Map Page
        Navigation.PushAsync(new MapPage(landmarksToVisit, landmarksVisited, null));
    }
}