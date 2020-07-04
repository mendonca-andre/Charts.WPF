namespace Charts.WPF.Converters
{
#if NETFX_CORE
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml;
#else
#endif
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// The boolean to visibility converter.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {

#if NETFX_CORE

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return InternalConvertBack(value, targetType, parameter);
        }

#else
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.InternalConvertBack(value, targetType, parameter);
        }

#endif

        /// <summary>
        /// The internal convert back.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object InternalConvertBack(object value, Type targetType, object parameter)
        {
            var back = value is Visibility visibility && visibility == Visibility.Visible;
            if (parameter == null)
            {
                return back;
            }

            if ((bool)parameter)
            {
                back = !back;
            }

            return back;
        }


        /// <summary>
        /// The internal convert.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private object InternalConvert(object value, Type targetType, object parameter)
        {
            try
            {
                var flag = false;
                if (value is bool b)
                {
                    flag = b;
                }

                if (parameter == null)
                {
                    return flag ? Visibility.Visible : Visibility.Collapsed;
                }

                if (bool.Parse((string)parameter))
                {
                    flag = !flag;
                }

                return flag ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            return Visibility.Collapsed;
        }
    }
}
