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

        private void OnSelectButtonClicked(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            // Set button states based on the clicked button
            switch (clickedButton.Text)
            {
                case "NL":
                    isButtonSelectedNL = true;
                    isButtonSelectedENG = false;
                    break;

                case "ENG":
                    isButtonSelectedNL = false;
                    isButtonSelectedENG = true;
                    break;
            }

            // Update button states
            UpdateButtonState(SelectButtonNL, isButtonSelectedNL);
            UpdateButtonState(SelectButtonENG, isButtonSelectedENG);
        }

        private void UpdateButtonState(Button button, bool isButtonSelected)
        {
            // Set button background and text color based on the state
            if (isButtonSelected)
            {
                button.BackgroundColor = Colors.Green; // Set the highlighted color
                button.TextColor = Colors.White; // Set the text color when selected
            }
            else
            {
                button.BackgroundColor = Colors.Gray; // Set the default color
                button.TextColor = Colors.Black; // Set the text color when not selected
            }
        }
    }
}

