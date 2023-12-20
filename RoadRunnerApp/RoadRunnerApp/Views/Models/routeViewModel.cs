using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoadRunnerApp.AppRoutes;
using System.Collections.ObjectModel;

namespace RoadRunnerApp.Views.Models;

public partial class routeViewModel : ObservableObject
{
    public routeViewModel()
    {
        Routes = new ObservableCollection<string>();
    }


    [ObservableProperty]
    ObservableCollection<string> routes;

    [ObservableProperty]
    string text;

    [RelayCommand]
    void Add()
    {
        Routes.Add(Text);
        Text = string.Empty;
    }
}
