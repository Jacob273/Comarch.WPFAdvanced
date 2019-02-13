using KM.Bosman.FakeServices;
using KM.Bosman.IServices;
using KM.Bosman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace KM.Bosman.WPFClient
{
    public class ViewModelLocator
    {
        // public SpeedsViewModel SpeedsViewModel => new SpeedsViewModel(new FakeSpeedService());

        private IUnityContainer container;

        public ViewModelLocator()
        {
            UseUnity();
        }


        // Install-Package Unity
        private void UseUnity()
        {
            container = new UnityContainer();
            container.RegisterType<ISpeedService, FakeSpeedService>();
            container.RegisterType<SpeedsViewModel>();
        }

        public SpeedsViewModel SpeedsViewModel => container.Resolve<SpeedsViewModel>();



    }
}
