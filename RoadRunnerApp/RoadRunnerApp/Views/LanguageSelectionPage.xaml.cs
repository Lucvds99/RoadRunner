using Microsoft.Maui.Controls;
using RoadRunnerApp.UIControllers;

namespace RoadRunnerApp.Views
{
    public partial class LanguageSelectionPage : ContentPage
    {
        private bool isButtonSelectedNL = false;
        private bool isButtonSelectedENG = false;
        private User user;

        public LanguageSelectionPage(User user)
        {
            this.user = user;
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

            ClickableAreaENG.Background = Color.FromHex("B8B8F3");
            ClickableAreaNL.Background = Color.FromHex("B8B8F3");
        }

        private void OnClickableAreaNL(object sender, EventArgs e)
        {
            Frame clickedFrame = sender as Frame;
            var label = clickedFrame.FindByName<Label>("language");
            isButtonSelectedENG = false;
            isButtonSelectedNL = true;
            updateFrames();
        }

        private void OnClickableAreaENG(object sender, EventArgs e)
        {
            Frame clickedFrame = sender as Frame;
            var label = clickedFrame.FindByName<Label>("language");
            isButtonSelectedENG = true;
            isButtonSelectedNL = false;
            updateFrames();
        }

        private void OnClickedEnterButton(object sender, EventArgs e)
        {

            //TODO : if no selection has been made popup with please enter a language. 
            if (isButtonSelectedENG)
                user.setLanguage("English");

            if (isButtonSelectedNL)
                user.setLanguage("Nederlands");
            Navigation.PushAsync(new TutorialPage(user));
        }

        private void updateFrames()
        {
            if (isButtonSelectedENG)
            {
                ClickableAreaENG.Background = Color.FromHex("D7B8F3");//Color = Mauve
                ClickableAreaNL.Background = Color.FromHex("B8B8F3");//Color = Periwinkle
            }
            else 
            {
                ClickableAreaNL.Background = Color.FromHex("D7B8F3");//Color = Mauve
                ClickableAreaENG.Background = Color.FromHex("B8B8F3");//Color = Periwinkle
            }
        }
    }
}

