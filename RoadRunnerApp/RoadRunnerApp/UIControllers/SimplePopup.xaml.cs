using CommunityToolkit.Maui.Views;

namespace RoadRunnerApp.UIControllers
{
    public partial class SimplePopup : Popup
    {
        public SimplePopup()
        {
            InitializeComponent();
            BindingContext = this; // Zorg ervoor dat de BindingContext is ingesteld op de huidige instantie van SimplePopup

        }

        public string PopupTitle { get; set; } = "Bamibal";
        public string PopupContent { get; set; } = "We hebben een soort popup I guess?";
        public string PopupImageSource { get; set; } = "arnokin.png"; // Vervang dit met het juiste pad naar je afbeelding
    }
}
