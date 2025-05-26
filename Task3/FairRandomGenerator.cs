using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class FairRandomGenerator
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();
    }
}
