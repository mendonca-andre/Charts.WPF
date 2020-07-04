namespace Charts.WPF.Converters
{
#if NETFX_CORE
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml;
#else
#endif
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class DoubleToGridLengthPercent : IValueConverter
    {
#if NETFX_CORE
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
#else
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

#endif

        private object InternalConvert(object value, Type targetType, object parameter)
        {
            var percentage = (double)value;
            if (parameter != null)
            {
                return percentage <= 1 ? new GridLength(1.0 - percentage, GridUnitType.Star) : new GridLength(100.0 - percentage, GridUnitType.Star);
            }

            return new GridLength(percentage, GridUnitType.Star);
        }
    }
}
