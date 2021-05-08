using System;
using System.Windows;
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
            try
            {
                tip.Text = Global.data.getTip();
                dispatcherTimer.Interval = TimeSpan.FromSeconds(10);
                dispatcherTimer.Start();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            this.Hide();
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
            this.Close();
        }

        private void skipButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            this.Hide();
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
            this.Close();
        }

        private void splashScreenDisabledCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Global.data.splashScreenDisabled = true;
        }

        private void splashScreenDisabledCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Global.data.splashScreenDisabled = false;
        }
    }
}
