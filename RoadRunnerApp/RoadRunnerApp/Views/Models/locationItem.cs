using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.Views.Models
{
    class locationItem : BindableObject
    {
        private string _locationName;
        private string _imageSource;

        public string LocationName
        {
            get { return _locationName; }
            set
            {
                if (_locationName != value)
                {
                    _locationName = value;
                    OnPropertyChanged(nameof(LocationName));
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
