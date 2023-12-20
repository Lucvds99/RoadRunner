using RoadRunnerApp.UIControllers;
using RoadRunnerApp.Views.Models;

namespace RoadRunnerApp.Views;

public partial class TutorialPage : ContentPage
{
    User user; 
	public TutorialPage(User user)
	{
        this.user = user;
        string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in vol";

        InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);


        //Items for in the Carousel in Tutorial page
        var items = new List<CarouselItem>
        {
            new CarouselItem {Title = "page 1", Description = lorem, Image = "testpicture.png", hasButton=false},
            new CarouselItem {Title = "page 2", Description = lorem, Image = "testpicture.png", hasButton=false},
            new CarouselItem {Title = "page 3", Description = lorem, Image = "", hasButton=false},
            new CarouselItem {Title = "page 4", Description = lorem, Image = "", Button = "continue", hasButton=true}
        };
        carouselView.ItemsSource = items;
    }
    public void onButtonClick(object sender, EventArgs e)
    {
        //TODO goes to the Map Page
        //Navigation.PushAsync(Map page including User);
    }

    public void onButtonClickSkip(object sender, EventArgs e)
    {
        //TODO goes to the Map Page
        //Navigation.PushAsync(Map page including User);
    }
}