using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WJLThoughts.Common.WPF.IconFont
{

    public static class IconfontExtensions
    {
        public static object GetIconFont(this IconfontEnum iconType, Brush brush, double Fontsize = 16)
        {
            try
            {
                return new TextBlock { Style = System.Windows.Application.Current.FindResource("IconTbStyle") as System.Windows.Style, Text = iconType.GetIconFontKey(), Foreground = brush, FontSize = Fontsize };
            }
            catch
            {
                return null;
            }
        }
        public static string GetIconFontKey(this IconfontEnum iconType)
        {
            try
            {

                return System.Windows.Application.Current.FindResource(iconType.ToString()).ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
