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
    class StylistViewModel : ViewModelBase
    {
        #region field
        //readonly StylistRepository _stylistRepository;

        #endregion

        #region property
        private ObservableCollection<StylistVo> _stylistList;

        public ObservableCollection<StylistVo> StylistList
        {
            get { return _stylistList; }
            set { _stylistList = value; }
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

        //private int _personalDay;
        public int PersonalDay
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
            //_stylistRepository = StylistRepository.SR;

            _selectedStylist = new StylistVo();
            //StylistList = new ObservableCollection<StylistVo>(_stylistRepository.StylistList);

            InsertCommand = new Command(ExecuteInsertMethod, CanExecuteMethod);
            ModifyCommand = new Command(ExecuteModifyMethod, CanExecuteMethod);
            DeleteCommand = new Command(ExecuteDeleteMethod, CanExecuteMethod);
            InitalizeCommand = new Command(ExecuteInitalizeMethod, CanExecuteMethod);
        }
        #endregion

        #region method
        private void ExecuteInitalizeMethod(object obj)
        {
            
        }
        private void ExecuteDeleteMethod(object obj)
        {
            StylistList.Remove(SelectedStylist);
        }

        private void ExecuteModifyMethod(object obj)
        {

        }

        private void ExecuteInsertMethod(object obj)
        {
            StylistList.Add(SelectedStylist);
        }

        private bool CanExecuteMethod(object arg)
        {
            return true;
        }

        #endregion
    }
}
