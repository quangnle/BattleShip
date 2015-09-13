using BattleShip.AIInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot
{
    public class MyRobot : IPlayer
    {
        private Dictionary<Position, HitInfo> _dic;
        private Position _recent = null;

        public string Name { get; set; }
        public MyRobot()
        {
            Name = "Dumb Robot";
            _dic = new Dictionary<Position, HitInfo>();
        }

        public BaseMapInfo LoadMap()
        {
            DefaultMapInfo map = new DefaultMapInfo();

            map.ShipInfos = new List<ShipInfo>();
            map.ShipInfos.Add(new ShipInfo { Direction = Direction.North, Position = new Position { Row = 9, Column = 0 }, ShipType = ShipType.BattleCruiser });
            map.ShipInfos.Add(new ShipInfo { Direction = Direction.North, Position = new Position { Row = 9, Column = 1 }, ShipType = ShipType.Cruiser });
            map.ShipInfos.Add(new ShipInfo { Direction = Direction.North, Position = new Position { Row = 9, Column = 2 }, ShipType = ShipType.Cruiser });
            map.ShipInfos.Add(new ShipInfo { Direction = Direction.North, Position = new Position { Row = 9, Column = 3 }, ShipType = ShipType.Frigate });
            map.ShipInfos.Add(new ShipInfo { Direction = Direction.North, Position = new Position { Row = 9, Column = 4 }, ShipType = ShipType.Frigate });
            map.ShipInfos.Add(new ShipInfo { Direction = Direction.North, Position = new Position { Row = 9, Column = 5 }, ShipType = ShipType.Frigate });
            map.ShipInfos.Add(new ShipInfo { Direction = Direction.North, Position = new Position { Row = 9, Column = 6 }, ShipType = ShipType.Destroyer });
            map.ShipInfos.Add(new ShipInfo { Direction = Direction.North, Position = new Position { Row = 9, Column = 7 }, ShipType = ShipType.Destroyer });
            map.ShipInfos.Add(new ShipInfo { Direction = Direction.North, Position = new Position { Row = 9, Column = 8 }, ShipType = ShipType.Destroyer });
            map.ShipInfos.Add(new ShipInfo { Direction = Direction.North, Position = new Position { Row = 9, Column = 9 }, ShipType = ShipType.Destroyer });
            return map;
        }

        public Position GetMove(Random rnd)
        {
            Position pos = null;
            do 
            {
                var r = rnd.Next(0, 10) % 10;
                var c = rnd.Next(0, 10) % 10;
                pos = new Position { Row = r, Column = c };
            } while (_dic.ContainsKey(pos));

            _dic.Add(pos, null);
            _recent = pos;

            return pos;
        }

        public void UpdateInfo(HitInfo info)
        {
            //_dic[_recent] = info;
        }
    }
}
