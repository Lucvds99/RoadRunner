using Microsoft.Maui.Controls;

namespace RoadRunnerApp.Views
{
    public partial class LanguageSelectionPage : ContentPage
    {
        private bool isButtonSelectedNL = false;
        private bool isButtonSelectedENG = false;

        public LanguageSelectionPage()
        {
            InitializeComponent();
        }

        private void OnClickableAreaNL(object sender, EventArgs e)
        {
            Frame clickedFrame = sender as Frame;
            var label = clickedFrame.FindByName<Label>("language");
            isButtonSelectedENG = false;
            isButtonSelectedNL = true;
            updateFrames();
        }


        private void OnClickableArea(object sender, EventArgs e)
        {
            Frame clickedFrame = sender as Frame;
            var label = clickedFrame.FindByName<Label>("language");
            isButtonSelectedENG = true;
            isButtonSelectedNL = false;
            updateFrames();
        }
        private void updateFrames()
        {
            if (isButtonSelectedENG)
            {
                ClickableAreaENG.Background = (Color)App.Current.Resources["Periwinkle"];
                ClickableAreaNL.Background = (Color)App.Current.Resources["Mauve"];
            }
            else 
            {
                ClickableAreaNL.Background = (Color)App.Current.Resources["Periwinkle"];
                ClickableAreaENG.Background = (Color)App.Current.Resources["Mauve"];
            }
        }
    }
}

