using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public virtual string DisplayName { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
