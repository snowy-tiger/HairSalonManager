using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.ViewModel
{
    class SettingViewModel : ViewModelBase
    {
        #region field
        readonly SettingRepository _settingRepository;
        #endregion

        #region property
        private ObservableCollection<SettingVo> _settingList;

        public ObservableCollection<SettingVo> SettingList
        {
            get { return _settingList; }
            set
            {
                _settingList = value;
                OnPropertyChanged("SettingList");
            }
        }

        private SettingVo _selectedSetting;

        public SettingVo SelectedSetting
        {
            get { return _selectedSetting; }
            set
            {
                _selectedSetting = value;
                OnPropertyChanged("SelectedSetting");
            }
        }
        private string _search;

        public string Search
        {
            get { return _search; }
            set { _search = value;
                OnPropertyChanged("Search");
                OnSearchChanged();
            }
        }
        

        public Command ModifyCommand { get; set; }
        public Command InitalizeCommand { get; }
        
        #endregion


        #region ctor
        public SettingViewModel()
        {
            _settingRepository = SettingRepository.SR;

            _selectedSetting = new SettingVo();

            SettingList = new ObservableCollection<SettingVo>(_settingRepository.GetSettings());

            ModifyCommand = new Command(ExecuteModifyMethod);
            InitalizeCommand = new Command(ExecuteInitalizeMethod);
        }



        #endregion


        #region method
        private void ExecuteInitalizeMethod(object obj)
        {
            throw new NotImplementedException();
        }     

        private void ExecuteModifyMethod(object obj)
        {
            _settingRepository.UpdateSetting(SelectedSetting);
        }

        private void OnSearchChanged()
        {
            SettingList = new ObservableCollection<SettingVo>(_settingRepository.GetSettings().Where(x => x.Property == Search));
        }
        #endregion
    }
}
