using KM.Bosman.FakeServices;
using KM.Bosman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM.Bosman.WPFClient
{
    public class ViewModelLocator
    {
        public SpeedsViewModel SpeedsViewModel => new SpeedsViewModel(new FakeSpeedService());
    }
}
