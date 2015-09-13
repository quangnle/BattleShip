using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.BattleEventArguments
{
    class SwitchTurnArgs: EventArgs
    {
        public int PlayerId { get; set; }
    }
}
