using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectCars.RacingApp.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RaceDialog : ContentDialog
    {
        public string RacerName { get; private set; }
        public bool DialogCompleted { get; private set; }
        public bool UserAgrees { get; private set; }

        public RaceDialog()
        {
            this.InitializeComponent(); 
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DialogCompleted = true;
            RacerName = this.txtRacerName.Text;
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DialogCompleted = false;
        }
    }
}
