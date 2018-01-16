using ProjectCars.RacingApp.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectCars.RacingApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartRaceFrame : Page
    {
        public StartRaceFrame()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            RaceDialog dialog = new RaceDialog();
            await dialog.ShowAsync();
            
            if(dialog.DialogCompleted)
            {
                if(string.IsNullOrEmpty(dialog.RacerName))
                {
                    Button_Click(sender, e);
                    return;
                }

                (App.Current as App).RaceModel = new RaceModel(dialog.RacerName);
                Frame.Navigate(typeof(RaceFrame), dialog.RacerName);
            }
        }
    }
}
