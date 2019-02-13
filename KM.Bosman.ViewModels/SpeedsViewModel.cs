using KM.Bosman.IServices;
using KM.Bosman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM.Bosman.ViewModels
{
    public class SpeedsViewModel : ViewModelBase
    {
        public IEnumerable<Speed> Speeds { get; set; }

        public Speed SelectedSpeed { get; set; }

        private readonly ISpeedService speedService;

        public SpeedsViewModel(ISpeedService speedService)
        {
            this.speedService = speedService;

            Load();
        }

        private void Load()
        {
            Speeds = speedService.Get();
        }

        
    }
}
