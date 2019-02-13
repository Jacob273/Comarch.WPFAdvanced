using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using KM.Bosman.FakeServices;
using KM.Bosman.IServices;
using KM.Bosman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.ServiceLocation;

namespace KM.Bosman.WPFClient
{
    public class ViewModelLocator
    {
        // public SpeedsViewModel SpeedsViewModel => new SpeedsViewModel(new FakeSpeedService());

        // private IUnityContainer container;
        // private IContainer container;

        public ViewModelLocator()
        {
             UseUnity();

           // UseAutoFac();
        }


        // Install-Package Unity
        private void UseUnity()
        {
            IUnityContainer container = new UnityContainer();
            //          container.RegisterType<ISpeedService, FakeSpeedService>();
            container.RegisterSingleton<ISpeedService, FakeSpeedService>();
            container.RegisterType<SpeedsViewModel>();

            // Install-Package Unity.ServiceLocation
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
        }

        //  public SpeedsViewModel SpeedsViewModel => container.Resolve<SpeedsViewModel>();


        public SpeedsViewModel SpeedsViewModel => ServiceLocator.Current.GetInstance<SpeedsViewModel>();

        // Install-Package AutoFac
        private void UseAutoFac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FakeSpeedService>().As<ISpeedService>().SingleInstance();
            builder.RegisterType<SpeedsViewModel>();
            builder.RegisterType<SpeedFaker>();

            IContainer container = builder.Build();

            // Install-Package Autofac.Extras.CommonServiceLocator
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

        }

        // Install-Package CommonServiceLocator



    }
}
