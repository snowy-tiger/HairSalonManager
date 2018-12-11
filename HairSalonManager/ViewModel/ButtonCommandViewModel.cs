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
        public string Color { get; set; }
        public ButtonCommandViewModel(string displayName, ICommand command, string color) : base(displayName, command)
        {
            Color = color;
        }
    }
}
