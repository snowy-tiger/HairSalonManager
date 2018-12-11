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
        private ReadOnlyCollection<CommandViewModel> _menuCommands;

        public IReadOnlyCollection<CommandViewModel> _MenuCommands
        {
            get
            {
                if (_menuCommands == null)
                {
                    List<CommandViewModel> list = this.CreateCommands();
                    _menuCommands = new ReadOnlyCollection<CommandViewModel>(list);
                }
                return _menuCommands;

            }
          
        }
       

        private List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>()
            {
                new CommandViewModel("메인",new RelayCommand(GoMainWindowPage)),
                new CommandViewModel("미용사",new RelayCommand(GoStylistPage)),
                new CommandViewModel("결제",new RelayCommand(GoPayPage))
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
