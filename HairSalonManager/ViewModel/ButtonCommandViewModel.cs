using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HairSalonManager.ViewModel
{
    class ButtonCommandViewModel : CommandViewModel
    {
        private static int _count = 0;
        
        public int Index { get; set; }

        private string _color;
        public string Color {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }
        public ButtonCommandViewModel(string displayName, ICommand command, string color) : base(displayName, command)
        {
            Index = _count++;
            Color = color;
        }
    }
}
