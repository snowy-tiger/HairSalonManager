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
        private DateTime _oldValue;
        private List<int> _oldTimes;
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0]==null)
                return Binding.DoNothing;
            if (values[1] == null)
                return Binding.DoNothing;
            string info = parameter as string;
            _oldValue = (DateTime)values[0];
            _oldTimes = values[1] as List<int>;
            int resultValue = info.Equals("Hour")?_oldValue.Hour: _oldValue.Minute;

            return _oldTimes.IndexOf(resultValue);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string info = parameter as string;
            int index = (int)value;
            

            switch (parameter)
            {
                case "Hour":
                    _oldValue = new DateTime(_oldValue.Year, _oldValue.Month, _oldValue.Day, _oldTimes[index], _oldValue.Minute, 0);
                    break;
                case "Minute":
                    _oldValue = new DateTime(_oldValue.Year, _oldValue.Month, _oldValue.Day, _oldValue.Hour, _oldTimes[index], 0);
                    break;
            }

            object[] obj = { _oldValue };
            return obj;
        }
    }
}
