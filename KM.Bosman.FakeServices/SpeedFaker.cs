using Bogus;
using KM.Bosman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM.Bosman.FakeServices
{
    public class SpeedFaker : Faker<Speed>
    {
        public SpeedFaker()
        {
            StrictMode(true);
            RuleFor(p => p.Value, f => f.Random.Float(0, 20));
            RuleFor(p => p.Unit, f => SpeedUnit.Knots);
        }
    }
}
