using RoadRunnerApp.AppRoutes;
using RoadRunnerApp.Views.Models;
using System.Collections.ObjectModel;

namespace RoadRunnerApp.Views;

public partial class LocationPage : ContentPage
{
    private ObservableCollection<locationItem> _locationCollection;
    private RouteManager _routeManager;
    private List<Landmark> _landmarks;
	public LocationPage(RouteManager routeManager)
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");


        _routeManager = routeManager;
        _locationCollection = new ObservableCollection<locationItem>();

        _landmarks = _routeManager.GetLandmarks();

        foreach (var landmark in _landmarks)
        {
            _locationCollection.Add(new locationItem {Id = landmark.id.ToString(), LocationName = landmark.name, ImageSource = landmark.ImgFilePath });
        }

        collectionView.ItemsSource = _locationCollection;
    }
    private void OnFrameTapped(object sender, EventArgs e)
    {
        if (sender is Frame tappedFrame && tappedFrame.BindingContext is locationItem tappedItem)
        {
            string frameId = tappedItem.Id;

            // Now you can use frameId as needed, for example, printing to the console
            Console.WriteLine($"Frame with Id {frameId} tapped!");

            // Retrieve the corresponding Landmark from the list based on the Id
            Landmark correspondingLandmark = _landmarks.FirstOrDefault(landmark => landmark.id.ToString() == frameId);

            if (correspondingLandmark != null)
            {
                // Now you have the Landmark, and you can use it as needed
                Console.WriteLine($"Corresponding Landmark: {correspondingLandmark.name}");

                // Or use the Landmark for further actions, e.g., navigating to a detail page with the Landmark as a parameter
                Navigation.PushAsync(new detailPage(correspondingLandmark, this));
            }
        }
    }





    private void LocationsButton(object sender, EventArgs e)
    {

    }

    private void MapButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MapPage(_routeManager));
    }

    private void RoutesButton(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RoutesPage(_routeManager));
    }
}