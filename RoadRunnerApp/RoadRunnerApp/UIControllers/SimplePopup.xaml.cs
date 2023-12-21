using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RoadRunnerApp.UIControllers
{
    public partial class SimplePopup : Popup
    {
        private PopupModel _PopupModel;
        public SimplePopup(NotificationVariant notificationVariant, string title, string content )
        {

            switch (notificationVariant)
            {
                case NotificationVariant.ERROR:
                        generateErrorPopup(title, content);
                    break;
                case NotificationVariant.STANDARD:
                        generateStandardPopup(title, content);
                    break;
                case NotificationVariant.REACHED_LOCATION:
                    generateReachedLocationPopup(title, content);
                    break;
            }

        }

        private void generateReachedLocationPopup(string title, string content)
        {
            _PopupModel = new PopupModel { PopupTitle = title, PopupContent = content , PopupImageSource = "", HasButton1 = false, HasButton2 = false };
            BindingContext = _PopupModel;
            InitializeComponent();
        }

        public void generateErrorPopup(string title, string content)
        {
            _PopupModel = new PopupModel { PopupTitle = title, PopupContent = content , PopupImageSource = "", HasButton1 = false, HasButton2 = false };
            BindingContext = _PopupModel;
            InitializeComponent();
        }
        public void generateStandardPopup(string title, string content)
        {
            _PopupModel = new PopupModel {PopupTitle = title, PopupContent = content , PopupImageSource = "", PopupButtonText1="continue", HasButton1=true, HasButton2=false};
            BindingContext = _PopupModel;
            InitializeComponent();
        }


    }
}
