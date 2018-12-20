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
    class ServiceViewModel : ViewModelBase
    {
        #region field
        readonly ServiceRepository _serviceRepository;

        #endregion

        #region property
        
        private ObservableCollection<ServiceVo> _serviceList;

        public ObservableCollection<ServiceVo> ServiceList
        {
            get { return _serviceList; }
            set {
                _serviceList = value;
                OnPropertyChanged("ServiceList");
            }
        }

        private ServiceVo _selectedService;

        public ServiceVo SelectedService
        {
            get { return _selectedService; }
            set {
                _selectedService = value;
                IsSelected = false;
                OnPropertyChanged("SelectedService");
            }
        }

        //private uint _serviceId;
        public ushort ServiceId
        {
            get { return _selectedService.ServiceId; }
            set {
                _selectedService.ServiceId = value;
                OnPropertyChanged("ServiceId");
            }
        }

        //private string _serviceName;
        public string ServiceName
        {
            get { return _selectedService.ServiceName; }
            set {
                _selectedService.ServiceName = value;
                OnPropertyChanged("ServiceName");
            }
        }

        //private uint _servicePrice;
        public uint ServicePrice
        {
            get { return _selectedService.ServicePrice; }
            set {
                _selectedService.ServicePrice = value;
                OnPropertyChanged("ServicePrice");
            }
        }

        //private ushort _serviceTime;
        public ushort ServiceTime
        {
            get { return _selectedService.ServiceTime; }
            set {
                _selectedService.ServiceTime = value;
                OnPropertyChanged("ServiceTime");
            }
        }

        //private string _serviceDescription;
        public string ServiceDescription
        {
            get { return _selectedService.ServiceDescription; }
            set {
                _selectedService.ServiceDescription = value;
                OnPropertyChanged("ServiceDescription");
            }
        }
        
        private bool _isSelected;

        public bool IsSelected
        {
            get
            {
                if (SelectedService == null)
                {
                    _isSelected = true;
                }
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public Command InsertCommand { get; set; }
        public Command ModifyCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command InitalizeCommand { get; }

        #endregion

        #region ctor
        public ServiceViewModel()
        {
            _serviceRepository = ServiceRepository.SR;

            _selectedService = new ServiceVo();
            _selectedService.ServiceId = 0;
            ServiceList = new ObservableCollection<ServiceVo>(_serviceRepository.GetServicesFromLocal());

            InsertCommand = new Command(ExecuteInsertMethod, CanExecuteMethod);
            ModifyCommand = new Command(ExecuteModifyMethod, CanExecuteMethod);
            DeleteCommand = new Command(ExecuteDeleteMethod, CanExecuteMethod);
            InitalizeCommand = new Command(ExecuteInitalizeMethod, CanExecuteMethod);
        }


        #endregion

        #region method
        private void ExecuteInitalizeMethod(object obj)
        {
            SelectedService = new ServiceVo();
            IsSelected = true;
        }
        private void ExecuteDeleteMethod(object obj)
        {
            if (SelectedService.ServiceId == 0)
            {
                MessageBox.Show("서비스를 선택해주세요");
                return;
            }
            _serviceRepository.DeleteService(SelectedService.ServiceId);
            ServiceList.Remove(SelectedService);
            SelectedService = new ServiceVo();
        }

        private void ExecuteModifyMethod(object obj)
        {
            if (!Check(SelectedService))
            {
                return;
            }
            _serviceRepository.UpdateService(SelectedService);
        }

        private void ExecuteInsertMethod(object obj)
        {
            if (!Check(SelectedService))
            {
                return;
            }
            else
            {
                _serviceRepository.InsertService(SelectedService);
                ServiceList.Add(SelectedService);
                ServiceList = new ObservableCollection<ServiceVo>(_serviceRepository.GetServices());
                SelectedService = new ServiceVo();
            }
        }

        private bool CanExecuteMethod(object arg)
        {
            return true;
        }

        private bool Check(ServiceVo sv)
        {
            if (sv.ServiceName != null && sv.ServiceDescription != null)
            {
                return true;
            }
            MessageBox.Show("빈칸이 존재합니다.");
            return false;
        }
        
        #endregion
    }
}
