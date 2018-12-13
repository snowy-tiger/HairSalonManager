using HairSalonManager.Model.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HairSalonManager.ViewModel
{
    class MainWindowViewModel:Notifier
    {
        #region Menu
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
                new ButtonCommandViewModel("메인",new Command(GoMainWindowPage),"red"),
                new ButtonCommandViewModel("미용사",new Command(GoStylistPage),"yellow"),
                new ButtonCommandViewModel("결제",new Command(GoPayPage),"blue"),
                new ButtonCommandViewModel("서비스",new Command(GoServicePage),"blue"),
                new ButtonCommandViewModel("전체 일정",new Command(GoSchedulePage),"blue"),
                new ButtonCommandViewModel("설정",new Command(GoSettingPage),"blue")
            };
        }
        #endregion

        #region Navigator xaml
        private void GoSettingPage(object obj)
        {

        }

        private void GoSchedulePage(object obj)
        {

        }

        private void GoServicePage(object obj)
        {

        }

        private void GoPayPage(object obj)
        {
            //NavigationServiceProvider.Navigate("/PayPage.xaml");
        }

        private void GoStylistPage(object obj)
        {
            NavigationServiceProvider.Navigate("/StatisticsPage.xaml");
        }

        private void GoMainWindowPage(object o)
        {
            NavigationServiceProvider.Navigate("/MainPage.xaml");
        }
        #endregion

        private WindowState _windowState;

        private string navigationUri;
        public string NavigationUri
        {
            get { return navigationUri; }
            set { navigationUri = value;
                OnPropertyChanged("NavigationUri");
            }
        }

        public WindowState WindowState
        {
            get { return _windowState; }
            set
            {
                _windowState = value;
                OnPropertyChanged("WindowState");
            }
        }

        public Command MinimizeCommand { get; set; }
        public Command CloseCommand { get; set; }

        public string StaticTitle { get; set; }

        private string _windowTitle;

        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
                WindowTitle = _title + "::" + StaticTitle;
            }
        }


        public MainWindowViewModel()
        {
            //Command 객체 생성
            MinimizeCommand = new Command(MinimizeMethod, CanExecuteMethod);
            CloseCommand = new Command(CloseMethod, CanExecuteMethod);

            //NavigationServiceProvider 등록
            NavigationServiceProvider._mainWindowInstance = this;

            StaticTitle = "미용실 관리 프로그램";
            _title = "메인";
            _windowTitle = _title + "::" + StaticTitle;
        }

        private void MinimizeMethod(object parameter)
        {
            WindowState = WindowState.Minimized;
        }
        private void CloseMethod(object parameter)
        {
            Environment.Exit(0);
        }
        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }
    }
}
