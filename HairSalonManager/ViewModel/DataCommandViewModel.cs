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


        public DataCommandViewModel(string displayName,ICommand command,T data) : base(displayName,command)
        {            
            Data = data;
        }
       

        
    }
}
