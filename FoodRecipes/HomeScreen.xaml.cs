using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace FoodRecipes
{
    public partial class HomeScreen : Window
    {
        List<Recipe> filterResultRecipes = new List<Recipe>();
        List<string> foodTypes = new List<string>();

        public HomeScreen()
        {
            InitializeComponent();
        }

        private void homeScreen_Loaded(object sender, RoutedEventArgs e)
        {
            PageInfomation.currentPage = 1;
            PageInfomation.pageSize = 10;
            PageInfomation.itemCount = Global.data.recipes.Count;
            updatePageInfomation();
            dataListView.ItemsSource = Global.data.recipes.Skip((PageInfomation.currentPage - 1) * PageInfomation.pageSize).Take(PageInfomation.pageSize);

            foodTypes = Global.data.recipesClassify();
            classificationListView.ItemsSource = foodTypes;
        }

        private void homeScreen_Closed(object sender, EventArgs e)
        {
            Global.data.save(Global.data, "Data/Data.ntp");
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            homeScreen.WindowState = WindowState.Minimized;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(searchBox.Text) &&
                classificationListView.SelectedIndex == -1 &&
                showFavoriteCheckBox.IsChecked == false)
            {
                if (PageInfomation.currentPage != PageInfomation.total())
                {
                    PageInfomation.currentPage += 1;
                    updatePageInfomation();
                    dataListView.ItemsSource = Global.data.recipes.Skip((PageInfomation.currentPage - 1) * PageInfomation.pageSize).Take(PageInfomation.pageSize);
                }
            }
            else
            {
                if (filterResultRecipes.Count() != 0)
                {
                    if (PageInfomation.currentPage != PageInfomation.total())
                    {
                        PageInfomation.currentPage += 1;
                        updatePageInfomation();
                        dataListView.ItemsSource = filterResultRecipes.Skip((PageInfomation.currentPage - 1) * PageInfomation.pageSize).Take(PageInfomation.pageSize);
                    }
                }
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(searchBox.Text) &&
                classificationListView.SelectedIndex == -1 &&
                showFavoriteCheckBox.IsChecked == false)
            {
                if (PageInfomation.currentPage > 1)
                {
                    PageInfomation.currentPage -= 1;
                    updatePageInfomation();
                    dataListView.ItemsSource = Global.data.recipes.Skip((PageInfomation.currentPage - 1) * PageInfomation.pageSize).Take(PageInfomation.pageSize);
                }
            }
            else
            {
                if (filterResultRecipes.Count() != 0)
                {
                    if (PageInfomation.currentPage > 1)
                    {
                        PageInfomation.currentPage -= 1;
                        updatePageInfomation();
                        dataListView.ItemsSource = filterResultRecipes.Skip((PageInfomation.currentPage - 1) * PageInfomation.pageSize).Take(PageInfomation.pageSize);
                    }
                }
            }
        }

        private void searchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            recipeFilterAndSearch();
        }

        private void classificationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            recipeFilterAndSearch();
        }

        private void filterButton_Unchecked(object sender, RoutedEventArgs e)
        {
            classificationListView.SelectedItems.Clear();
        }

        private void showFavoriteCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            recipeFilterAndSearch();
        }

        private void showFavoriteCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            recipeFilterAndSearch();
        }

        private void likeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (showFavoriteCheckBox.IsChecked == true)
            {
                recipeFilterAndSearch();
            }
        }

        private void detailButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int id = (item as Recipe).foodID;
            Global.idTransfer = id;
            DetailScreen detailScreen = new DetailScreen();
            detailScreen.Owner = this;
            coverGrid.Visibility = Visibility.Visible;
            mainGrid.IsEnabled = false;
            detailScreen.ShowDialog();

            if (detailScreen.DialogResult == true)
            {
                mainGrid.IsEnabled = true;
                coverGrid.Visibility = Visibility.Collapsed;
            }

            else
            {
                mainGrid.IsEnabled = true;
                coverGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddScreen addScreen = new AddScreen();
            addScreen.Owner = this;
            coverGrid.Visibility = Visibility.Visible;
            mainGrid.IsEnabled = false;
            addScreen.ShowDialog();

            if (addScreen.DialogResult == true)
            {
                mainGrid.IsEnabled = true;
                coverGrid.Visibility = Visibility.Collapsed;
                searchBox.Clear();
                showFavoriteCheckBox.IsChecked = false;
                filterButton.IsChecked = false;

                PageInfomation.currentPage = 1;
                PageInfomation.pageSize = 10;
                PageInfomation.itemCount = Global.data.recipes.Count;
                updatePageInfomation();
                dataListView.ItemsSource = Global.data.recipes.Skip((PageInfomation.currentPage - 1) * PageInfomation.pageSize).Take(PageInfomation.pageSize);

                foodTypes = Global.data.recipesClassify();
                classificationListView.ItemsSource = foodTypes;
            }

            else
            {
                mainGrid.IsEnabled = true;
                coverGrid.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Cập nhật thông tin phân trang
        /// </summary>
        private void updatePageInfomation()
        {
            pageInfomationText.Text = $"{PageInfomation.currentPage} / {PageInfomation.total()}";

            if (PageInfomation.currentPage == PageInfomation.total())
            {
                nextButton.IsEnabled = false;
            }
            else
            {
                nextButton.IsEnabled = true;
            }

            if (PageInfomation.currentPage == 1 ||
                PageInfomation.currentPage == 0)
            {
                previousButton.IsEnabled = false;
            }
            else
            {
                previousButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// Cập nhật lại nguồn
        /// </summary>
        /// <param name="source">Nguồn</param>
        private void updateItemSource(List<Recipe> source)
        {
            PageInfomation.currentPage = 1;
            PageInfomation.itemCount = source.Count;
            if (PageInfomation.itemCount == 0)
            {
                PageInfomation.currentPage = 0;
                notificationText.Visibility = Visibility.Visible;
            }
            else
            {
                notificationText.Visibility = Visibility.Hidden;
            }
            updatePageInfomation();
            dataListView.ItemsSource = source.Skip((PageInfomation.currentPage - 1) * PageInfomation.pageSize).Take(PageInfomation.pageSize);
        }

        /// <summary>
        /// Tìm kiếm và lọc công thức nấu ăn theo loại, ưa thích
        /// </summary>
        private void recipeFilterAndSearch()
        {
            filterResultRecipes.Clear();

            if (string.IsNullOrEmpty(searchBox.Text) &&
                classificationListView.SelectedIndex == -1 &&
                showFavoriteCheckBox.IsChecked == false)
            {
                updateItemSource(Global.data.recipes);
            }
            else if (string.IsNullOrEmpty(searchBox.Text) &&
                     classificationListView.SelectedIndex == -1 &&
                     showFavoriteCheckBox.IsChecked == true)
            {
                foreach (Recipe recipe in Global.data.recipes)
                {
                    if (recipe.favorite == showFavoriteCheckBox.IsChecked)
                    {
                        filterResultRecipes.Add(recipe);
                    }
                }
                updateItemSource(filterResultRecipes);
            }
            else if (string.IsNullOrEmpty(searchBox.Text) &&
                     classificationListView.SelectedIndex != -1 &&
                     showFavoriteCheckBox.IsChecked == false)
            {
                foreach (Recipe recipe in Global.data.recipes)
                {
                    if (recipe.foodType == foodTypes[classificationListView.SelectedIndex])
                    {
                        filterResultRecipes.Add(recipe);
                    }
                }
                updateItemSource(filterResultRecipes);
            }
            else if (string.IsNullOrEmpty(searchBox.Text) &&
                     classificationListView.SelectedIndex != -1 &&
                     showFavoriteCheckBox.IsChecked == true)
            {
                foreach (Recipe recipe in Global.data.recipes)
                {
                    if (recipe.foodType == foodTypes[classificationListView.SelectedIndex] &&
                        recipe.favorite == showFavoriteCheckBox.IsChecked)
                    {
                        filterResultRecipes.Add(recipe);
                    }
                }
                updateItemSource(filterResultRecipes);
            }
            else if (!string.IsNullOrEmpty(searchBox.Text) &&
                     classificationListView.SelectedIndex == -1 &&
                     showFavoriteCheckBox.IsChecked == false)
            {
                foreach (Recipe recipe in Global.data.recipes)
                {
                    if (removeUnicode(recipe.foodName.ToLower()).Contains(removeUnicode(searchBox.Text.ToLower())))
                    {
                        filterResultRecipes.Add(recipe);
                    }
                }
                updateItemSource(filterResultRecipes);
            }
            else if (!string.IsNullOrEmpty(searchBox.Text) &&
                     classificationListView.SelectedIndex == -1 &&
                     showFavoriteCheckBox.IsChecked == true)
            {
                foreach (Recipe recipe in Global.data.recipes)
                {
                    if (removeUnicode(recipe.foodName.ToLower()).Contains(removeUnicode(searchBox.Text.ToLower())) &&
                        recipe.favorite == showFavoriteCheckBox.IsChecked)
                    {
                        filterResultRecipes.Add(recipe);
                    }
                }
                updateItemSource(filterResultRecipes);
            }
            else if (!string.IsNullOrEmpty(searchBox.Text) &&
                     classificationListView.SelectedIndex != -1 &&
                     showFavoriteCheckBox.IsChecked == false)
            {
                foreach (Recipe recipe in Global.data.recipes)
                {
                    if (removeUnicode(recipe.foodName.ToLower()).Contains(removeUnicode(searchBox.Text.ToLower())) &&
                        recipe.foodType == foodTypes[classificationListView.SelectedIndex])
                    {
                        filterResultRecipes.Add(recipe);
                    }
                }
                updateItemSource(filterResultRecipes);
            }
            else
            {
                foreach (Recipe recipe in Global.data.recipes)
                {
                    if (removeUnicode(recipe.foodName.ToLower()).Contains(removeUnicode(searchBox.Text.ToLower())) &&
                        recipe.foodType == foodTypes[classificationListView.SelectedIndex] &&
                        recipe.favorite == showFavoriteCheckBox.IsChecked)
                    {
                        filterResultRecipes.Add(recipe);
                    }
                }
                updateItemSource(filterResultRecipes);
            }
        }

        /// <summary>
        /// Bỏ tất cả dấu tiếng Việt trong chuỗi
        /// </summary>
        /// <param name="text">Chuỗi</param>
        /// <returns>Chuỗi đã khử dấu</returns>
        public static string removeUnicode(string text)
        {
            string result = text;
            string[] signedChars = new string[] {"á","à","ả","ã","ạ","â","ấ","ầ","ẩ","ẫ","ậ","ă","ắ","ằ","ẳ","ẵ","ặ",
                                                 "đ",
                                                 "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                                                 "í","ì","ỉ","ĩ","ị",
                                                 "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                                                 "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                                                 "ý","ỳ","ỷ","ỹ","ỵ"};
            string[] unsignedChars = new string[] {"a","a","a","a","a","a","a","a","a","a","a","a","a","a","a","a","a",
                                                   "d",
                                                   "e","e","e","e","e","e","e","e","e","e","e",
                                                   "i","i","i","i","i",
                                                   "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                                                   "u","u","u","u","u","u","u","u","u","u","u",
                                                   "y","y","y","y","y",};
            for (int i = 0; i < signedChars.Length; i++)
            {
                result = result.Replace(signedChars[i], unsignedChars[i]);
                result = result.Replace(signedChars[i].ToUpper(), unsignedChars[i].ToUpper());
            }
            return result;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
