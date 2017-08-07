using System;
using System.Diagnostics;
using System.Globalization;

namespace RenameIt.ValueConverters
{
    /// <summary>
    /// Converts the <see cref="Identifiers.Pages"/> to an actual page.
    /// </summary>
    class PageValueConverter : Base.ValueConverter<PageValueConverter>
    {
        /// <summary>
        /// Converts our <see cref="Identifiers.Pages"/> to the associated page
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Identifiers.Pages)value)
            {
                case Identifiers.Pages.RenameIt:
                    return new Views.Pages.RenameItPage();

                case Identifiers.Pages.Settings:
                    return new Views.Pages.SettingsPage();

                default:
                    Debugger.Break();
                    return null;
            }
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
