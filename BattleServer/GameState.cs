using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleServer
{
    enum GameState
    {
        Unready,
        WaitForArmyFormations,
        KickoffMatch,
        WaitForConfirmations,
        GetMoveFromA,
        GetMoveFromB,
        ReportGameResult
    }
}
