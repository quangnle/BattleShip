using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.BattleEventArguments
{
    class ShotEventArgs: EventArgs
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
