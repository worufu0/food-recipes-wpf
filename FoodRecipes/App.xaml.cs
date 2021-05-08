using System;
using System.Linq.Expressions;
using System.Windows;

namespace FoodRecipes
{
    public partial class App : Application
    {
        private void application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                Global.data = Global.data.load(Global.data, "Data/Data.ntp");
                if (Global.data.splashScreenDisabled == false)
                {
                    SplashScreen splashScreen = new SplashScreen();
                    splashScreen.Show();
                }
                else
                {
                    HomeScreen homeScreen = new HomeScreen();
                    homeScreen.Show();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                this.Shutdown();
            }
        }
    }
}
