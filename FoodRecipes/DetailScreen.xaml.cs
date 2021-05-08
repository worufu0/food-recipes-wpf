using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace FoodRecipes
{
    public partial class DetailScreen : Window
    {
        int id = Global.idTransfer;
        List<string> stepNames = new List<string>();

        public DetailScreen()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void detailScreen_Loaded(object sender, RoutedEventArgs e)
        {
            stepNames = Global.data.recipes[id].getStepNames();
            stepsListView.ItemsSource = stepNames;
            stepContentListView.ItemsSource = Global.data.recipeIdentification(id).steps.Skip(0).Take(1);
            foodName.Text = Global.data.recipeIdentification(id).foodName.ToUpper();
        }

        private void stepsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stepContentListView.ItemsSource = Global.data.recipeIdentification(id).steps.Skip(stepsListView.SelectedIndex).Take(1);
            thumbnailListView.ItemsSource = Global.data.recipeIdentification(id).getThumbnails(stepsListView.SelectedIndex);
        }

        private void nextStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (stepsListView.SelectedIndex == Global.data.recipeIdentification(id).getStepNames().Count - 1)
            {
                stepsListView.SelectedIndex = 0;
                stepContentListView.ItemsSource = Global.data.recipeIdentification(id).steps.Skip(stepsListView.SelectedIndex).Take(1);
            }
            else
            {
                stepContentListView.ItemsSource = Global.data.recipeIdentification(id).steps.Skip(stepsListView.SelectedIndex++).Take(1);
            }
        }

        private void previousStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (stepsListView.SelectedIndex == 0)
            {
                stepsListView.SelectedIndex = Global.data.recipeIdentification(id).getStepNames().Count - 1;
                stepContentListView.ItemsSource = Global.data.recipeIdentification(id).steps.Skip(stepsListView.SelectedIndex).Take(1);
            }
            else
            {
                stepContentListView.ItemsSource = Global.data.recipeIdentification(id).steps.Skip(stepsListView.SelectedIndex--).Take(1);
            }
        }

        private void playVideoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start($"{Global.data.recipeIdentification(id).video}");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
