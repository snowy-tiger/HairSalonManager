using HairSalonManager.Model.Repository;
using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HairSalonManager.ViewModel
{
    class DataCommandViewModel<T> : CommandViewModel
    {
        private ReservedServiceRepository  _reservedServiceRepository;

        public ReservedServiceRepository ReservedServiceRepository
        {
            get
            {
                if (_reservedServiceRepository == null)
                    _reservedServiceRepository = ReservedServiceRepository.RSR;
                return _reservedServiceRepository;

            }            
        }


        private object data; 

        public object Data
        {
            get { return data; }
            set { data = value; }
        }


        public DataCommandViewModel(string displayName,T data) : base(displayName)
        {            
            Data = data;
        }

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new Command(param => this.OnRequestClose());

                return _closeCommand;
            }
        }

        public event EventHandler RequestClose;

        void OnRequestClose()
        {           
            if (Data as ReservedServiceVo != null)
            {
                ReservedServiceVo v = (ReservedServiceVo)Data;
                ReservedServiceRepository.RemoveReservedService(v.ResNum, v.SerId);
            }
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
