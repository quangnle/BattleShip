using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.AIInterface
{
    public abstract class BaseMapInfo
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract int NumberOfBattleCruisers { get; }
        public abstract int NumberOfCruisers { get; }
        public abstract int NumberOfFrigates { get; }
        public abstract int NumberOfDestroyers { get; }
        public List<ShipInfo> ShipInfos { get; set; }
    }
}
