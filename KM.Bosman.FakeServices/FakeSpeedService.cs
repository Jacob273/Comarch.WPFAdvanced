using KM.Bosman.IServices;
using KM.Bosman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM.Bosman.FakeServices
{
    public class FakeSpeedService : ISpeedService
    {
        public IEnumerable<Speed> Get()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Speed>> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
