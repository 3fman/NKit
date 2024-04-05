using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace NKit.Xaml
{
    public class MainViewModel
    {
        public void RecordMousePoint(Point point)
        {
            Console.WriteLine(point.X + "," + point.Y);
        }

        public void PrintSum(int a, int b)
        {
            Console.WriteLine(a + b);
        }
    }

    public class MouseEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MouseEventArgs args = (MouseEventArgs)value;
            return args.GetPosition(parameter as UIElement);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
