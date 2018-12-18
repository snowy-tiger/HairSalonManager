using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HairSalonManager.Model.Util
{
    class TimeDivideConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0]==null)
                return Binding.DoNothing;
            if (values[1] == null)
                return Binding.DoNothing;
            string info = parameter as string;
            DateTime? timeValue = values[0] as DateTime?;
            List<int> times = values[1] as List<int>;
            int resultValue = info.Equals("Hour")?timeValue.Value.Hour: timeValue.Value.Minute;

            return times.IndexOf(resultValue);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            //if (values[0] == null)
            //    return Binding.DoNothing;
            //if (values[1] == null)
            //    return Binding.DoNothing;
            //string info = parameter as string;
            //DateTime? timeValue = values[0] as DateTime?;
            //List<int> times = values[1] as List<int>;
            //int resultValue = info.Equals("Hour") ? timeValue.Value.Hour : timeValue.Value.Minute;

            //return times.IndexOf(resultValue);

            throw new NotImplementedException();
        }
    }
}
