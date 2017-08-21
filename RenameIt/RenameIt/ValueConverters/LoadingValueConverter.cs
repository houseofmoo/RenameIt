using System;
using System.Diagnostics;
using System.Globalization;

namespace RenameIt.ValueConverters
{
    /// <summary>
    /// Converts the <see cref="Identifiers.Pages"/> to an actual page.
    /// </summary>
    class LoadingValueConverter : Common.ValueConverters.BaseValueConverter<LoadingValueConverter>
    {
        /// <summary>
        /// Converts from <see cref="bool"/> to Visibility value (<see cref="string"/>)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? "Visible" : "Hidden";
        }

        /// <summary>
        /// No implemented, not needed.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
