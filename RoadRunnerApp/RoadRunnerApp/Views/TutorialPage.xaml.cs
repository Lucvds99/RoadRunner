using RoadRunnerApp.Views.Models;

namespace RoadRunnerApp.Views;

public partial class TutorialPage : ContentPage
{
	public TutorialPage()
	{
        string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in vol";

        InitializeComponent();
        var items = new List<TutorialCarouselItem>
        {
            new TutorialCarouselItem {Title = "page 1", Description = lorem, Image = "testpicture.png", hasButton=false},
            new TutorialCarouselItem {Title = "page 2", Description = lorem, Image = "testpicture.png", hasButton=false},
            new TutorialCarouselItem {Title = "page 3", Description = lorem, Image = "testpicture.png", hasButton=false},
            new TutorialCarouselItem {Title = "page 4", Description = lorem, Image = "testpicture.png", Button = "continue", hasButton=true}
        };
        carouselView.ItemsSource = items;
    }
    public void onButtonClick(object sender, EventArgs e)
    {
        //methode for next page. 
    }

    public void onButtonClickSkip(object sender, EventArgs e)
    {
        //methode for next page.
    }
}