using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WJLThoughts.Common.WPF.ValueConverters
{
    public class StringToColorConverter : BaseValueConverter<StringToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return (System.Drawing.Color)new System.Drawing.ColorConverter().ConvertFromString(value.ToString());
            }
            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
