using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HairSalonManager.ViewModel
{
    class StylistViewModel : ViewModelBase
    {
        #region field
        readonly StylistRepository _stylistRepository;

        #endregion

        #region property
        private ObservableCollection<StylistVo> _stylistList;

        public ObservableCollection<StylistVo> StylistList
        {
            get { return _stylistList; }
            set {
                _stylistList = value;
                OnPropertyChanged("StylistList");
            }
        }

        private StylistVo _selectedStylist;

        public StylistVo SelectedStylist
        {
            get { return _selectedStylist; }
            set {
                _selectedStylist = value;
                OnPropertyChanged("SelectedStylist");
            }
        }

        //private uint _stylistId;
        public uint StylistId
        {
            get { return _selectedStylist.StylistId; }
            set {
                _selectedStylist.StylistId = value;
                OnPropertyChanged("StylistId");
            }
        }

        //private string _stylistName;
        public string StylistName
        {
            get { return _selectedStylist.StylistName; }
            set {
                _selectedStylist.StylistName = value;
                OnPropertyChanged("StylistName");
            }
        }

        //private uint _additionalPrice;
        public uint AdditionalPrice
        {
            get { return _selectedStylist.AdditionalPrice; }
            set { _selectedStylist.AdditionalPrice = value;
                OnPropertyChanged("AdditionalPrice");
            }
        }

        //private byte _personalDay;
        public byte PersonalDay
        {
            get { return _selectedStylist.PersonalDay; }
            set {
                _selectedStylist.PersonalDay = value;
                OnPropertyChanged("PersonalDay");
            }
        }

        public Command InsertCommand { get; set; }
        public Command ModifyCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command InitalizeCommand { get; }

        #endregion

        #region ctor
        public StylistViewModel()
        {
            _stylistRepository = StylistRepository.SR;

            _selectedStylist = new StylistVo();

            StylistList = new ObservableCollection<StylistVo>(_stylistRepository.GetStylistsFromLocal());

            InsertCommand = new Command(ExecuteInsertMethod, CanExecuteMethod);
            ModifyCommand = new Command(ExecuteModifyMethod, CanExecuteMethod);
            DeleteCommand = new Command(ExecuteDeleteMethod, CanExecuteMethod);
            InitalizeCommand = new Command(ExecuteInitalizeMethod, CanExecuteMethod);
        }
        #endregion

        #region method
        private void ExecuteInitalizeMethod(object obj)
        {
            SelectedStylist = new StylistVo();
        }
        private void ExecuteDeleteMethod(object obj)
        {
            _stylistRepository.RemoveStylist(SelectedStylist.StylistId);
            StylistList.Remove(SelectedStylist);
            SelectedStylist = new StylistVo();
        }

        private void ExecuteModifyMethod(object obj)
        {
            if (!Check(SelectedStylist))
            {
                return;
            }
            _stylistRepository.UpdateStylist(SelectedStylist);
        }

        private void ExecuteInsertMethod(object obj)
        {
            if (!Check(SelectedStylist))
            {
                return;
            }
            else
            {
                _stylistRepository.InsertStylist(SelectedStylist);
                StylistList.Add(SelectedStylist);
            }
        }

        private bool CanExecuteMethod(object arg)
        {
            return true;
        }

        private bool Check(StylistVo sv)
        {
            if(sv.StylistId!=null && sv.StylistName!=null && sv.AdditionalPrice!=null && sv.PersonalDay != null)
            {
                return true;
            }
            MessageBox.Show("빈칸이 존재합니다.");
            return false;
        }

        #endregion
    }
}
