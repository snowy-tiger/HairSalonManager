using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HairSalonManager.Model.Util
{
    class GenderRadioConverter : IValueConverter
    {
        //Data -> Converter -> View
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //0 = 남자, 1 = 여자
            int genderValue = (int)value;
            int radioGender = Int32.Parse(parameter as string);

            return genderValue == radioGender;
        }

        //View -> Converter -> Data
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //0 = 남자, 1 = 여자
            bool genderValue = (bool)value;
            int radioGender = Int32.Parse(parameter as string);

            if (!genderValue)
                return Binding.DoNothing;
            else
                return radioGender;

        }
    }
}
