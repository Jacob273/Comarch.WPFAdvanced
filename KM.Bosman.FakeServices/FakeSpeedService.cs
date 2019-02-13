using KM.Bosman.IServices;
using KM.Bosman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KM.Bosman.FakeServices
{
    public class FakeSpeedService : ISpeedService
    {
        private IEnumerable<Speed> speeds;

        private readonly SpeedFaker speedFaker;

        public FakeSpeedService(SpeedFaker speedFaker)
        {
            this.speedFaker = speedFaker;

            speeds = speedFaker.Generate(100);
        }

        public IEnumerable<Speed> Get()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            return speeds;
        }

        public Task<IEnumerable<Speed>> GetAsync()
        {
            return Task.Run(()=>Get());
        }
    }
}
