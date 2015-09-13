using BattleShip.AIInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Processor
{
    public class PlayerController
    {
        private BattleMap _map;
        private IPlayer _player;
        public string Name { get { return _player.Name; } }
        public PlayerController(IPlayer player, BaseMapInfo mapInfo)
        {
            _player = player;
            _map = new BattleMap(mapInfo);
            _map.InitShips(mapInfo);
        }

        public HitInfo GetShot(Position pos)
        {
            HitInfo result = null;
            if (pos.Row >= 0 && pos.Row < _map.Height && pos.Column >= 0 && pos.Column < _map.Width)
            {
                var val = _map[pos.Row, pos.Column];
                var p = new Position { Row = pos.Row, Column = pos.Column };
                if (val <= 0)
                {
                    result = new HitInfo { IsHit = false, Destroyed = false };
                }
                else
                {
                    var count = 0;
                    for (int row = 0; row < _map.Height; row++)
                    {
                        for (int col = 0; col < _map.Width; col++)
                        {
                            if (_map[row, col] == val) count++;
                        }
                    }

                    if (count == 1)
                        result = new HitInfo { IsHit = true, Destroyed = true };
                    else
                        result = new HitInfo { IsHit = true, Destroyed = false };
                }

                _map[pos.Row, pos.Column] = -1;
                result.Pos = p;
                return result;
            }
            else throw new Exception("Invalid shot");
        }

        /// <summary>
        /// should be async to avoid long time calculation
        /// </summary>
        /// <returns></returns>
        public Position GetMove(Random rnd)
        {
            return _player.GetMove(rnd);
        }

        public void Update(HitInfo hitInfo)
        {
            _player.UpdateInfo(hitInfo);
        }

        public bool IsDead()
        {
            for (int row = 0; row < _map.Height; row++)
            {
                for (int col = 0; col < _map.Width; col++)
                {
                    if (_map[row, col] > 0) return false;
                }
            }

            return true;
        }

    }
}
