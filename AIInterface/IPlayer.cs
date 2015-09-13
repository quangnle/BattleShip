using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.AIInterface
{
    public interface IPlayer
    {
        string Name { get; set; }
        BaseMapInfo LoadMap();
        BaseMapInfo GetMap();
        Position GetMove(Random rnd);
        void UpdateInfo(HitInfo info);
    }
}
