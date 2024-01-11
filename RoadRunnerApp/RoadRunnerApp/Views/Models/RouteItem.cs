namespace RoadRunnerApp.Views.Models
{
    public class RouteItem : BindableObject
    {
        private string _id;
        private string _routeName;
        private string _imageSource;
        private string _description;
        private string _difficulty;

        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
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
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        public string Difficulty
        {
            get { return _difficulty; }
            set
            {
                if (_difficulty != value)
                {
                    _difficulty = value;
                    OnPropertyChanged(nameof(Difficulty));
                }
            }
        }
    }
}
