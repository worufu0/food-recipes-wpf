using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class AddScreen : Window
    {
        List<bool> filled = new List<bool>();

        public AddScreen()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                filled.Add(false);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void timeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            openFileDialog.ShowDialog();
            imageBox.Text = openFileDialog.FileName;
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri(imageBox.Text)),
                Stretch = Stretch.UniformToFill,
            };
            imageBorder.Child = image;
        }

        private void imageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            openFileDialog.ShowDialog();
            StackPanel tempImageStackPanel = stepsStackPanel.Children[stepsStackPanel.Children.Count - 1] as StackPanel;
            TextBox tempImageBox = tempImageStackPanel.Children[1] as TextBox;
            tempImageBox.Text = openFileDialog.FileName;
        }

        private void addStepButton_Click(object sender, RoutedEventArgs e)
        {
            bool checkAddStep = true;
            for (int i = 0; i < stepsStackPanel.Children.Count; i += 3)
            {
                StackPanel tempContentStackPanel = stepsStackPanel.Children[i + 1] as StackPanel;
                TextBox tempContentBox = tempContentStackPanel.Children[1] as TextBox;
                StackPanel tempImageStackPanel = stepsStackPanel.Children[i + 2] as StackPanel;
                TextBox tempImageBox = tempImageStackPanel.Children[1] as TextBox;
                if (string.IsNullOrEmpty(tempContentBox.Text.Trim()))
                {
                    checkAddStep = false;
                }

                if (string.IsNullOrEmpty(tempImageBox.Text.Trim()))
                {
                    checkAddStep = false;
                    break;
                }
            }

            if (checkAddStep)
            {
                Style fillTextBoxStyle = this.FindResource("fillTextBoxStyle") as Style;
                Style browseButtonStyle = this.FindResource("browseButtonStyle") as Style;
                TextBlock stepNameLabel = new TextBlock()
                {
                    Text = $"Bước {stepsStackPanel.Children.Count / 3 + 1}",
                    FontStyle = FontStyles.Italic,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Foreground = (Brush)(new BrushConverter().ConvertFrom("#008000")),
                    FontSize = 18,
                    FontWeight = FontWeights.Heavy,
                    Margin = new Thickness(10, 10, 20, 10)
                };

                TextBlock contentLabel = new TextBlock()
                {
                    Text = "Nội dung:",
                    Foreground = (Brush)(new BrushConverter().ConvertFrom("#696969")),
                    Width = 110,
                    FontSize = 16,
                    Margin = new Thickness(10)
                };

                TextBox contentBox = new TextBox()
                {
                    Style = fillTextBoxStyle,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 16,
                    Width = 380,
                    Height = 120,
                    MaxLength = 820,
                    Padding = new Thickness(8)
                };

                TextBlock imageLabel = new TextBlock()
                {
                    Text = "Ảnh minh họa:",
                    Foreground = (Brush)(new BrushConverter().ConvertFrom("#696969")),
                    Width = 110,
                    FontSize = 16,
                    Margin = new Thickness(10)
                };

                TextBox imageBox = new TextBox()
                {
                    Style = fillTextBoxStyle,
                    VerticalAlignment = VerticalAlignment.Top,
                    FontSize = 16,
                    Width = 200,
                    Height = 40,
                    MaxLength = 128,
                    IsReadOnly = true,
                    Padding = new Thickness(8)
                };

                Button imageButton = new Button()
                {
                    Style = browseButtonStyle
                };

                TextBlock imagePreviewText = new TextBlock()
                {
                    Text = "Hình ảnh",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = (Brush)(new BrushConverter().ConvertFrom("#D3D3D3"))
                };

                Border imagePreviewBorder = new Border()
                {
                    BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#D3D3D3")),
                    Width = 120,
                    Height = 100,
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(0, 0, 10, 0)
                };
                imagePreviewBorder.Child = imagePreviewText;

                StackPanel contentStackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10),
                };
                contentStackPanel.Children.Add(contentLabel);
                contentStackPanel.Children.Add(contentBox);

                StackPanel imageStackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10),
                };
                imageStackPanel.Children.Add(imageLabel);
                imageStackPanel.Children.Add(imageBox);
                imageStackPanel.Children.Add(imageButton);
                imageStackPanel.Children.Add(imagePreviewBorder);

                stepsStackPanel.Children.Add(stepNameLabel);
                stepsStackPanel.Children.Add(contentStackPanel);
                stepsStackPanel.Children.Add(imageStackPanel);

                contentBox.TextChanged += fillBox_TextChanged;
                imageBox.TextChanged += fillBox_TextChanged;
                imageButton.Click += imageButton_Click;

                filled.Add(true);
                filled.Add(false);
                filled.Add(false);
                addButton.IsEnabled = false;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe newRecipe = new Recipe();
            newRecipe.steps = new List<Step>();

            newRecipe.foodID = Global.data.searchValidID();
            newRecipe.foodName = foodNameBox.Text;
            newRecipe.description = descriptionBox.Text;
            newRecipe.foodType = foodTypeBox.Text;
            newRecipe.time = $"{timeBox.Text} phút";
            newRecipe.video = videoBox.Text;
            newRecipe.favorite = false;
            newRecipe.beProtected = false;

            string fileExtension = imageBox.Text.Substring(imageBox.Text.Length - 3);
            string fileName = $"Data/Images/{newRecipe.foodID}.0.{fileExtension}";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                File.Copy(imageBox.Text, fileName);
            }
            else
            {
                File.Copy(imageBox.Text, fileName);
            }
            newRecipe.image = $"{newRecipe.foodID}.0.{fileExtension}";

            newRecipe.steps.Add(new Step { content = firstStepContentBox.Text });
            fileExtension = firstStepImageBox.Text.Substring(firstStepImageBox.Text.Length - 3);
            fileName = $"Data/Images/{newRecipe.foodID}.1.{fileExtension}";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                File.Copy(firstStepImageBox.Text, fileName);
            }

            else
            {
                File.Copy(firstStepImageBox.Text, fileName);
            }
            newRecipe.steps[0].guideImage = $"{newRecipe.foodID}.1.{fileExtension}";

            for (int i = 0; i < stepsStackPanel.Children.Count; i += 3)
            {
                StackPanel tempContentStackPanel = stepsStackPanel.Children[i + 1] as StackPanel;
                TextBox tempContentBox = tempContentStackPanel.Children[1] as TextBox;
                StackPanel tempImageStackPanel = stepsStackPanel.Children[i + 2] as StackPanel;
                TextBox tempImageBox = tempImageStackPanel.Children[1] as TextBox;
                newRecipe.steps.Add(new Step{content = tempContentBox.Text});
                fileExtension = tempImageBox.Text.Substring(imageBox.Text.Length - 3);
                fileName = $"Data/Images/{newRecipe.foodID}.{newRecipe.steps.Count - 1}.{fileExtension}";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    File.Copy(tempImageBox.Text, fileName);
                }
                else
                {
                    File.Copy(tempImageBox.Text, fileName);
                }
                newRecipe.steps[newRecipe.steps.Count - 1].guideImage = $"{newRecipe.foodID}.{newRecipe.steps.Count - 1}.{fileExtension}";
            }

            newRecipe.steps.Add(new Step { content = lastStepContentBox.Text });
            fileExtension = lastStepImageBox.Text.Substring(lastStepImageBox.Text.Length - 3);
            fileName = $"Data/Images/{newRecipe.foodID}.{newRecipe.steps.Count}.{fileExtension}";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                File.Copy(lastStepImageBox.Text, fileName);
            }
            else
            {
                File.Copy(lastStepImageBox.Text, fileName);
            }
            newRecipe.steps[newRecipe.steps.Count - 1].guideImage = $"{newRecipe.foodID}.{newRecipe.steps.Count}.{fileExtension}";

            Global.data.recipes.Add(newRecipe);
            DialogResult = true;
        }

        private void fillBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(foodNameBox.Text.Trim()))
            {
                filled[0] = false;
            }
            else
            {
                filled[0] = true;
            }

            if (string.IsNullOrEmpty(descriptionBox.Text.Trim()))
            {
                filled[1] = false;
            }
            else
            {
                filled[1] = true;
            }

            if (string.IsNullOrEmpty(foodTypeBox.Text.Trim()))
            {
                filled[2] = false;
            }
            else
            {
                filled[2] = true;
            }

            if (string.IsNullOrEmpty(timeBox.Text.Trim()))
            {
                filled[3] = false;
            }
            else
            {
                filled[3] = true;
            }

            if (string.IsNullOrEmpty(imageBox.Text.Trim()))
            {
                filled[4] = false;
            }
            else
            {
                filled[4] = true;
            }

            if (string.IsNullOrEmpty(videoBox.Text.Trim()))
            {
                filled[5] = false;
            }
            else
            {
                filled[5] = true;
            }

            if (string.IsNullOrEmpty(firstStepContentBox.Text.Trim()))
            {
                filled[6] = false;
            }
            else
            {
                filled[6] = true;
            }

            if (string.IsNullOrEmpty(firstStepImageBox.Text.Trim()))
            {
                filled[7] = false;
            }
            else
            {
                filled[7] = true;
            }

            if (string.IsNullOrEmpty(lastStepContentBox.Text.Trim()))
            {
                filled[8] = false;
            }
            else
            {
                filled[8] = true;
            }

            if (string.IsNullOrEmpty(lastStepImageBox.Text.Trim()))
            {
                filled[9] = false;
            }
            else
            {
                filled[9] = true;
            }

            {
                for (int i = 0; i < stepsStackPanel.Children.Count; i += 3)
                {
                    StackPanel tempContentStackPanel = stepsStackPanel.Children[i + 1] as StackPanel;
                    TextBox tempContentBox = tempContentStackPanel.Children[1] as TextBox;
                    StackPanel tempImageStackPanel = stepsStackPanel.Children[i + 2] as StackPanel;
                    TextBox tempImageBox = tempImageStackPanel.Children[1] as TextBox;
                    if (string.IsNullOrEmpty(tempContentBox.Text.Trim()))
                    {
                        filled[i + 11] = false;
                    }
                    else
                    {
                        filled[i + 11] = true;
                    }

                    if (string.IsNullOrEmpty(tempImageBox.Text.Trim()))
                    {
                        filled[i + 12] = false;
                    }
                    else
                    {
                        filled[i + 12] = true;
                    }
                }
            }

            if (filled.Contains(false))
            {
                addButton.IsEnabled = false;
            }
            else
            {
                addButton.IsEnabled = true;
            }
        }

        private void firstStepBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            openFileDialog.ShowDialog();
            firstStepImageBox.Text = openFileDialog.FileName;
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri(firstStepImageBox.Text)),
                Stretch = Stretch.UniformToFill,
            };
            firstStepImageBorder.Child = image;
        }

        private void lastStepBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            openFileDialog.ShowDialog();
            lastStepImageBox.Text = openFileDialog.FileName;
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri(lastStepImageBox.Text)),
                Stretch = Stretch.UniformToFill,
            };
            lastStepImageBorder.Child = image;
        }
    }
}
