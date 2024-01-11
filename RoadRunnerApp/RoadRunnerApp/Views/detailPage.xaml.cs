using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

using RoadRunnerApp.AppRoutes;
using DBRoute = RoadRunnerApp.AppDatabase.Route;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace RoadRunnerApp.Views;

public partial class detailPage : ContentPage
{
    public Object Content { get; private set; }
    public Object ReferalPage { get; private set; }
    public detailPage(Object content, Object referalPage)
    {
        var viewModel = new DetailViewModel();
        Content = content;
        ReferalPage = referalPage;
        if (content is Landmark landmark)
        {
            viewModel = new DetailViewModel
            {
                Title = landmark.name,
                HeaderInfo = landmark.theme,
                Image = landmark.ImgFilePath,
                Description = landmark.description
            };
        }
        else if (content is DBRoute route)
        {
            viewModel = new DetailViewModel
            {
                Title = route.Name,
                HeaderInfo = "",
                Image = "",
                Description = route.Description
            };
        }

        BindingContext = viewModel;

        InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetBackButtonTitle(this, "");
    }

    public void goBackClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}

public class DetailViewModel
{
    public string? Title { get; set; }
    public string? HeaderInfo { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
}