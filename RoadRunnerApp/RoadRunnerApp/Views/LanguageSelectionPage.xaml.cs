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
            //Go to the next page. give along wich button selected eng.  
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

