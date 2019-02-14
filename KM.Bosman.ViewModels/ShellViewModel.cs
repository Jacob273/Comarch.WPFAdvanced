using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KM.Bosman.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {

        public string Message { get; set; }

        public ShellViewModel()
        {
            ShowSpeedsCommand = new RelayCommand(ShowSpeeds, ()=>CanShowSpeeds);
            Message = "My message";
        }

        public ICommand ShowSpeedsCommand { get; private set; }

        private void ShowSpeeds()
        {
         
        }

        private bool CanShowSpeeds => true;
        
        
    }
}
