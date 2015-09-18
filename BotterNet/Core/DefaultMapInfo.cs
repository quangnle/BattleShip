using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Core
{
    public class DefaultMapInfo : BaseMapInfo
    {
        public override int Width { get { return 10; } }
        public override int Height { get { return 10; } }
        public override int NumberOfBattleCruisers { get { return 1; } }
        public override int NumberOfCruisers { get { return 2; } }
        public override int NumberOfFrigates { get { return 3; } }
        public override int NumberOfDestroyers { get { return 4; } }
    }
}
