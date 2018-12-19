using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HairSalonManager.Model.Util
{
    
    class TimeTableColorConverter : IValueConverter
    {
        int _converterCount = 0;
        string _param = "-1";
        //DATA -> CONVERTER -> TimeTable
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!_param.Equals(parameter as string))
                _converterCount = 0;
            _param = parameter as string;
            if (_param.Equals("1") && (_converterCount > 48 || _converterCount == 0))
            {
                _converterCount = 1;
                return Binding.DoNothing;
            }
            else if (_param.Equals("0") && _converterCount > 47)
            {
                _converterCount = 0;
            }
                
            DataGridCell cell = (DataGridCell)value;
            DataRowView row = (DataRowView)cell.DataContext;
            object[] data = row.Row.ItemArray;

            if (data[_converterCount++] is string)
                return "#2196F3";
            else return Binding.DoNothing;
        }

        //ONE WAY BINDING (NOT IMPLEMENTED)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
