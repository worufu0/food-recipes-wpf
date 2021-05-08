using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FoodRecipes
{
    public class RelativeToAbsolutePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string relative = (string)value;
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            string absolute = $"{folder}Data/Images/{relative}";
            return absolute;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolean = (bool)value;
            Visibility visibility;
            if (boolean)
            {
                visibility = Visibility.Visible;
            }
            else
            {
                visibility = Visibility.Hidden;
            }
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
