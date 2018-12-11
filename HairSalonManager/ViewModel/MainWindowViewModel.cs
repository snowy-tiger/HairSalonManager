﻿using HairSalonManager.Model.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HairSalonManager.ViewModel
{
    class MainWindowViewModel:Notifier
    {
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
