using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FoodRecipes
{
    public partial class SplashScreen : Window
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public SplashScreen()
        {
            InitializeComponent();
        }

        private void splashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            HomeScreen homeScreen = this.Owner as HomeScreen;
            tip.Text += homeScreen.data.tipList.getTip();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(11);
            dispatcherTimer.Start();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            HomeScreen homeScreen = this.Owner as HomeScreen;
            homeScreen.Opacity = 1;
            homeScreen.ShowInTaskbar = true;
            this.Close();
        }

        private void skipButton_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen homeScreen = this.Owner as HomeScreen;
            this.Close();
            homeScreen.Opacity = 1;
            homeScreen.ShowInTaskbar = true;
        }

        private void splashScreenDisabledCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HomeScreen homeScreen = this.Owner as HomeScreen;
            homeScreen.config.splashScreenDisabled = true;
        }

        private void splashScreenDisabledCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HomeScreen homeScreen = this.Owner as HomeScreen;
            homeScreen.config.splashScreenDisabled = false;
        }
    }
}
