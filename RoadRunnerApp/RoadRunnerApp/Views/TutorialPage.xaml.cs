using RoadRunnerApp.UIControllers;
using RoadRunnerApp.Views.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace RoadRunnerApp.Views;

public partial class TutorialPage : ContentPage
{
    User user;
    public string TutTitle { get; set; }
    public string Skip {  get; set; }
    public TutorialPage(User user)
	{
        this.user = user;
        InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        
        if (user.language != "Nederlands")
        {
            CultureInfo selectedCulture = new CultureInfo("nl-NL");
            Thread.CurrentThread.CurrentCulture = selectedCulture;
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
        } else
        {
            CultureInfo selectedCulture = new CultureInfo("");
            Thread.CurrentThread.CurrentCulture = selectedCulture;
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
        }
        ResourceManager resourceManager = new ResourceManager("RoadRunnerApp.Resources.Strings.AppResources", typeof(TutorialPage).Assembly);


        TutTitle = resourceManager.GetString("Tut-Title");
        Skip = resourceManager.GetString("Tut-Skip");
        BindingContext = this;

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
        Navigation.PushAsync(new MapPage(user));
    }

    public void onButtonClickSkip(object sender, EventArgs e)
    {
        //TODO goes to the Map Page
        Navigation.PushAsync(new MapPage(user));
    }
}