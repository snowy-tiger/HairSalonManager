//using HairSalonManager.Model.Vo;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HairSalonManager.ViewModel
//{
//    class SettingViewModel : ViewModelBase
//    {
//        #region field
//        readonly SettingRepository _settingRepository;
//        #endregion


//        #region property
//        private ObservableCollection<SettingVo> _settingList;

//        public ObservableCollection<SettingVo> SettingList
//        {
//            get { return _settingList; }
//            set {
//                _settingList = value;
//                OnPropertyChanged("SettingList");
//            }
//        }

//        private SettingVo _selectedSetting;

//        public SettingVo SelectedSetting
//        {
//            get { return _selectedSetting; }
//            set {
//                _selectedSetting = value;
//                OnPropertyChanged("SelectedSetting");
//            }
//        }

//        public Command ModifyCommand { get; set; }
//        public Command InitalizeCommand { get; }
//        #endregion


//        #region ctor
//        public SettingViewModel()
//        {
//            _settingRepository = _settingRepository.SR;

//            _selectedSetting = new SettingVo();

//            SettingList = new ObservableCollection<SettingVo>(_settingRepository.GetSetting());

//            ModifyCommand = new Command(ExecuteModifyMethod, CanExecuteMethod);
//            InitalizeCommand = new Command(ExecuteInitalizeMethod, CanExecuteMethod);
//        }

        
//        }
//        #endregion


//        #region method
//        private void ExecuteInitalizeMethod(object obj)
//        {
//            throw new NotImplementedException();
//        }

//        private bool CanExecuteMethod(object arg)
//        {
//            throw new NotImplementedException();
//        }

//        private void ExecuteModifyMethod(object obj)
//        {
//            throw new NotImplementedException();
//        #endregion

//    }
//}
