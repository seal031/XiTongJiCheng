using System;
using System.Windows;
using System.Windows.Data;

namespace IntegrationClient.HelperClasses
{
    public class BooleanHiddenVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            bool? isVisible = value as bool?;

            if (isVisible == null || isVisible == false)
                return Visibility.Hidden;
            else
                return Visibility.Visible;
        }

        public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            if (!( value is Visibility ))
                return DependencyProperty.UnsetValue;

            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return true;
            else
                return false;
        }
        #endregion
    }
}
