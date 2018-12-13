using HairSalonManager.Model.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HairSalonManager.ViewModel
{
    class MenuViewModel
    {        
        private ReadOnlyCollection<ButtonCommandViewModel> _menuCommands;

        public ReadOnlyCollection<ButtonCommandViewModel> MenuCommands
        {
            get
            {
                if (_menuCommands == null)
                {
                    List<ButtonCommandViewModel> list = this.CreateCommands();
                    _menuCommands = new ReadOnlyCollection<ButtonCommandViewModel>(list);
                }
                return _menuCommands;

            }
          
        }
       

        private List<ButtonCommandViewModel> CreateCommands()
        {
            return new List<ButtonCommandViewModel>()
            {
                new ButtonCommandViewModel("메인",new RelayCommand(GoMainWindowPage),"red"),
                new ButtonCommandViewModel("미용사",new RelayCommand(GoStylistPage),"yellow"),
                new ButtonCommandViewModel("결제",new RelayCommand(GoPayPage),"blue")
            };

        }

        private void GoPayPage(object obj)
        {
            NavigationServiceProvider.Navigate("/PayPage.xaml");
        }

        private void GoStylistPage(object obj)
        {
            NavigationServiceProvider.Navigate("/StylistPage.xaml");
        }

        private void GoMainWindowPage(object o)
        {
            NavigationServiceProvider.Navigate("/MainPage.xaml");
        }
    }
}
