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
                new ButtonCommandViewModel("메인",new Command(GoMainPage),"#1e1e1e"),
                new ButtonCommandViewModel("결제",new Command(GoPayPage),"#2D2F31"),
                new ButtonCommandViewModel("통계",new Command(GoStatisticsPage),"#2D2F31"),
                new ButtonCommandViewModel("관리",new Command(GoManagePage),"#2D2F31"),
                new ButtonCommandViewModel("설정",new Command(GoSettingPage),"#2D2F31")
            };
        }
        #endregion

        #region Navigator xaml
        private void GoSettingPage(object parameter)
        {
            Navigate((int)parameter, "/View/SettingPage.xaml", "설정");
        }

        private void GoManagePage(object parameter)
        {
            Navigate((int)parameter, "/View/ManagePage.xaml", "관리");
        }

        private void GoStatisticsPage(object parameter)
        {
            Navigate((int)parameter, "/View/StatisticsPage.xaml", "통계");
        }

        private void GoPayPage(object parameter)
        {
            Navigate((int)parameter, "/View/PayPage.xaml", "결제");
        }

        private void GoMainPage(object parameter)
        {
            
            Navigate((int)parameter, "/View/MainPage.xaml", "메인");
        }
        #endregion

        private WindowState _windowState;

        private int _previousMenuIndex;

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

            _previousMenuIndex = 0;
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

        public void Navigate(int index, string uri, string title)
        {
            MenuCommands[index].Color = "#1e1e1e";
            MenuCommands[_previousMenuIndex].Color = "#2D2F31";
            NavigationUri = uri;
            Title = title;
            _previousMenuIndex = index;
        }
    }
}
