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
        static int ConverterCount = 0;

        //DATA -> CONVERTER -> TimeTable
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ConverterCount == 49)
            {
                ConverterCount = 0;
            }
            if (parameter.Equals("1") && ConverterCount == 0)
            {
                ConverterCount++;
                return Binding.DoNothing;
            }
                
            DataGridCell cell = (DataGridCell)value;
            DataRowView row = (DataRowView)cell.DataContext;
            object[] data = row.Row.ItemArray;

            if (data[ConverterCount++] is string)
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
