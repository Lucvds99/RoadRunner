using CommunityToolkit.Maui.Views;
using RoadRunnerApp.Views.Models;

namespace RoadRunnerApp.UIControllers
{
    public partial class SimplePopup : Popup
    {
        private PopupModel _PopupModel;
        public SimplePopup(NotificationVariant notificationVariant, string title, string content, string imgSource)
        {

            switch (notificationVariant)
            {
                case NotificationVariant.ERROR:
                        generateErrorPopup(title, content);
                    break;
                case NotificationVariant.STANDARD:
                        generateStandardPopup(title, content, imgSource);
                    break;
                case NotificationVariant.REACHED_LOCATION:
                    generateReachedLocationPopup(title, content, imgSource);
                    break;
            }

        }

        private void generateReachedLocationPopup(string title, string content, string imgSource)
        {
            _PopupModel = new PopupModel { PopupTitle = title, PopupContent = content , PopupImageSource = imgSource, HasButton1 = false, HasButton2 = false };
            BindingContext = _PopupModel;
            InitializeComponent();
        }

        public void generateErrorPopup(string title, string content)
        {
            _PopupModel = new PopupModel { PopupTitle = title, PopupContent = content , PopupImageSource = "", HasButton1 = false, HasButton2 = false };
            BindingContext = _PopupModel;
            InitializeComponent();
        }
        public void generateStandardPopup(string title, string content, string imgSource)
        {
            _PopupModel = new PopupModel {PopupTitle = title, PopupContent = content , PopupImageSource = imgSource, PopupButtonText1="", HasButton1=false, HasButton2=false};
            BindingContext = _PopupModel;
            InitializeComponent();
        }


    }
}
