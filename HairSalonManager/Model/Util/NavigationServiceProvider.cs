using HairSalonManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Util
{
    class NavigationServiceProvider
    {
        static public MainWindowViewModel _mainWindowInstance;

        //메인 윈도우의 화면을 다른 xaml로 전환한다.
        static public void Navigate(string path)
        {
            _mainWindowInstance.NavigationUri = path;
        }
    }
}
