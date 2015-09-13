using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.AIInterface
{
    public interface IPlayer
    {
        BaseMapInfo LoadMap();
        Position GetMove(Random rnd);
        void UpdateInfo(HitInfo info);
    }
}
