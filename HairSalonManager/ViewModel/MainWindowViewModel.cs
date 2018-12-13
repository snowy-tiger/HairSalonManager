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
                new ButtonCommandViewModel("메인",new Command(GoMainPage),"#2D2F31"),
                new ButtonCommandViewModel("결제",new Command(GoPayPage),"#2D2F31"),
                new ButtonCommandViewModel("통계",new Command(GoStatisticsPage),"#2D2F31"),
                new ButtonCommandViewModel("관리",new Command(GoManagePage),"#2D2F31"),
                new ButtonCommandViewModel("설정",new Command(GoSettingPage),"#2D2F31")
            };
        }
        #endregion

        #region Navigator xaml
        private void GoSettingPage(object obj)
        {
            //NavigationUri = "/View/SettingPage.xaml";
        }

        private void GoStatisticsPage(object obj)
        {
            Navigate("/View/StatisticsPage.xaml", "통계");
        }

        private void GoManagePage(object parameter)
        {
            //NavigationUri = "/View/ManagePage.xaml";
        }

        private void GoPayPage(object parameter)
        {
            //NavigationServiceProvider.Navigate("/PayPage.xaml");
        }

        private void GoMainPage(object parameter)
        {
            
            Navigate("/View/MainPage.xaml", "메인");
        }
        #endregion

        private WindowState _windowState;

        private string _navigationUri;
        public string NavigationUri
        {
            get { return _navigationUri; }
            set { _navigationUri = value;
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

            _navigationUri = "/View/MainPage.xaml";
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

        public void Navigate(string uri, string title)
        {
            NavigationUri = uri;
            Title = title;
        }
    }
}
