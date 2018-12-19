using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HairSalonManager.Model.Util
{
    class FirstColumnConverter : IValueConverter
    {
        private int _count = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_count > 48)
                _count = 0;
            if (_count++ == 0)
                return 15;
            else
                return Binding.DoNothing;
        }

        //NOT IMPLEMENT
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
