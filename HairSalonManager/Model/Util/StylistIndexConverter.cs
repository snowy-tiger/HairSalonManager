using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HairSalonManager.Model.Util
{
    class StylistIndexConverter : IValueConverter
    {
        private ObservableCollection<StylistVo> _stylistList = new ObservableCollection<StylistVo>(StylistRepository.SR.GetStylistsFromLocal());

        //Data -> Converter -> View
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return -1;
            uint stylistId = (uint)value;
            return _stylistList.IndexOf(_stylistList.Single(x => x.StylistId == stylistId));

        }
        //View -> Converter -> Data
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Binding.DoNothing;
            int selectedIndex = (int)value;
            return _stylistList[selectedIndex].StylistId;
        }
    }
}
