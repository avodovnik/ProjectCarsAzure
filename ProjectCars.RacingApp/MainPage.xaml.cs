using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProjectCars.RacingApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(StartRaceFrame));
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (CanTransitionModel())
            {
                if (args.IsSettingsInvoked)
                {
                    ContentFrame.Navigate(typeof(SettingsFrame));
                }
                else
                {
                    ContentFrame.Navigate(typeof(StartRaceFrame));
                }
            }
        }

        private bool CanTransitionModel()
        {
            if (RaceModel.Current == null) return true;
            if (RaceModel.Current.RaceState == RaceState.Finished) return true;
            return false;
        }
    }
}
