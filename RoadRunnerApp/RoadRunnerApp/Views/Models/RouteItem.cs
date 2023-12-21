namespace RoadRunnerApp.Views.Models
{
    public class RouteItem : BindableObject
    {
        private string _routeName;
        private string _imageSource;

        public string RouteName
        {
            get { return _routeName; }
            set
            {
                if (_routeName != value)
                {
                    _routeName = value;
                    OnPropertyChanged(nameof(RouteName));
                }
            }
        }

        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }
    }
}
