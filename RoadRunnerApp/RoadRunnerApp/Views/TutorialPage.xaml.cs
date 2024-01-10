using RoadRunnerApp.UIControllers;
using RoadRunnerApp.Views.Models;
using System.Collections.Generic;
using System.Resources;

namespace RoadRunnerApp.Views;

public partial class TutorialPage : ContentPage
{
    User user; 
	public TutorialPage(User user)
	{
        this.user = user;
        InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);

        ResourceManager resourceManager = new ResourceManager("RoadRunnerApp.AppResource", typeof(TutorialPage).Assembly);

        //Items for in the Carousel in Tutorial page
        var items = new List<CarouselItem>
        {
            new CarouselItem {Title = resourceManager.GetString("Tut-It-1-title"), Description = resourceManager.GetString("Tut-It-1-desc"), Image = "testpicture.png", hasButton=false},
            new CarouselItem {Title = resourceManager.GetString("Tut-It-2-title"), Description = resourceManager.GetString("Tut-It-2-desc"), Image = "testpicture.png", hasButton=false},
            new CarouselItem {Title = resourceManager.GetString("Tut-It-3-title"), Description = resourceManager.GetString("Tut-It-3-desc"), Image = "", Button = "continue", hasButton=true}
        };
        carouselView.ItemsSource = items;
    }
    public void onButtonClick(object sender, EventArgs e)
    {
        //TODO goes to the Map Page
        Navigation.PushAsync(new MapPage());
    }

    public void onButtonClickSkip(object sender, EventArgs e)
    {
        //TODO goes to the Map Page
        Navigation.PushAsync(new MapPage());
    }
}