using RoadRunnerApp.Views.Models;
using System.Collections.ObjectModel;
using RoadRunnerApp.UIControllers;
using System.Globalization;
using System.Resources;

namespace RoadRunnerApp.Views;

public partial class LocationPage : ContentPage
{
    public string Title { get; set; }
    User user;
    private ObservableCollection<locationItem> _locationCollection;
	public LocationPage(User user)
	{
        this.user = user;
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");

        if (user.language != "Nederlands")
        {
            CultureInfo selectedCulture = new CultureInfo("nl-NL");
            Thread.CurrentThread.CurrentCulture = selectedCulture;
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
        }
        else
        {
            CultureInfo selectedCulture = new CultureInfo("");
            Thread.CurrentThread.CurrentCulture = selectedCulture;
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
        }
        ResourceManager resourceManager = new ResourceManager("RoadRunnerApp.Resources.Strings.AppResources", typeof(TutorialPage).Assembly);

        Title = resourceManager.GetString("Location-Title");
        BindingContext = this;

        _locationCollection = new ObservableCollection<locationItem>
            {
                new locationItem { LocationName = "location 1", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 2", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 3", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 4", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 5", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 6", ImageSource = "museum.png"},
                new locationItem { LocationName = "location 7", ImageSource = "museum.png"}

            };

        collectionView.ItemsSource = _locationCollection;
    }

    private void LocationsButton(object sender, EventArgs e)
    {

    }

    private void MapButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MapPage(user));
    }

    private void RoutesButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RoutesPage(user));
    }
}