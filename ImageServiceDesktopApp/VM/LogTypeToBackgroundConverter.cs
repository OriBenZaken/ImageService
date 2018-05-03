using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImageServiceDesktopApp.VM
{
    class LogTypeToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.Name != "Brush")
            {
                throw new InvalidOperationException("Must convert to a brush!");
            }

            string type = (string)value;
            switch(type)
            {
                case "INFO":
                    return System.Windows.Media.Brushes.LightGreen;
                case "WARNING":
                    return System.Windows.Media.Brushes.Yellow;
                case "FAIL":
                    return System.Windows.Media.Brushes.Coral;
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
