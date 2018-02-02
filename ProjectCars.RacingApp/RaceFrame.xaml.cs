using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectCars.RacingApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RaceFrame : Page
    {
        public RaceFrame()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.textRacerName.Text = e.Parameter.ToString();
            base.OnNavigatedTo(e);

            RaceModel.Current.RaceStarted();
        }


        private async void btnStopRace_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Are you sure?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Keep racing"));
            messageDialog.Commands.Add(new UICommand(
                "Stop racing",
                new UICommandInvokedHandler(this.AbortRaceCommand)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private void AbortRaceCommand(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            Frame.Navigate(typeof(StartRaceFrame));

            RaceModel.Current.RaceAborted();
        }

        private void btnFinishRace_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SummaryFrame));
        }

        private void RPMSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            SetRPMGauge(RPMSlider.Value);
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            SetSpeedGauge(SpeedSlider.Value);
        }

        private void GearSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            SetGearGauge(GearSlider.Value);
        }

        public void SetRPMGauge(double RPM)
        {
            RPMNeedleTransform.Rotation = (RPM * 0.0225) - 45;
        }
        public void SetSpeedGauge(double speedMPH)
        {
            SpeedNeedleTransform.Rotation = (speedMPH * 1.125) - 45;
            SpeedTxt.Text = speedMPH.ToString();
        }
        public void SetGearGauge(double Gear)
        {
            GearTxt.Text = Gear.ToString();
        }


    }
}
