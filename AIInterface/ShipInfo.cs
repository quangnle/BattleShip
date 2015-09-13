using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.AIInterface
{
    public class ShipInfo
    {
        public int Id { get; set; }
        public ShipType ShipType { get; set; }
        public Position Position { get; set; }
        public Direction Direction { get; set; }
    }
}
