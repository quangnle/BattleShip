using BattleShip.AIInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Processor
{
    public class BattleMap
    {
        private int[] _dx = new int[] { 0, 1, 0, 1 };
        private int[] _dy = new int[] { -1, 0, 1, 0 };

        private int _width, _height;
        private int[,] _map;
        private int _currentId;
        public BattleMap(BaseMapInfo mapInfo)
        {
            _currentId = 1;
            _width = mapInfo.Width;
            _height = mapInfo.Height;
            _map = new int[_height, _width];
        }

        public int this[int row, int col] 
        { 
            get { return _map[row, col]; }
            set { _map[row, col] = value; }
        }

        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public void InitShips(BaseMapInfo mapInfo)
        {
            var shipInfos = mapInfo.ShipInfos.Where(si => si.ShipType == ShipType.BattleCruiser);
            if (shipInfos != null && shipInfos.Count() == mapInfo.NumberOfBattleCruisers)
            {
                foreach (var shipInfo in shipInfos)
                {
                    shipInfo.Id = _currentId;
                    PlaceShip(shipInfo);
                    _currentId++;
                }
            }
            else throw new Exception("Invalid map.");

            shipInfos = mapInfo.ShipInfos.Where(si => si.ShipType == ShipType.Cruiser);
            if (shipInfos != null && shipInfos.Count() == mapInfo.NumberOfCruisers)
            {
                foreach (var shipInfo in shipInfos)
                {
                    shipInfo.Id = _currentId;
                    PlaceShip(shipInfo);
                    _currentId++;
                }
            }
            else throw new Exception("Invalid map.");

            shipInfos = mapInfo.ShipInfos.Where(si => si.ShipType == ShipType.Frigate);
            if (shipInfos != null && shipInfos.Count() == mapInfo.NumberOfFrigates)
            {
                foreach (var shipInfo in shipInfos)
                {
                    shipInfo.Id = _currentId;
                    PlaceShip(shipInfo);
                    _currentId++;
                }
            }
            else throw new Exception("Invalid map.");

            shipInfos = mapInfo.ShipInfos.Where(si => si.ShipType == ShipType.Destroyer);
            if (shipInfos != null && shipInfos.Count() == mapInfo.NumberOfDestroyers)
            {
                foreach (var shipInfo in shipInfos)
                {
                    shipInfo.Id = _currentId;
                    PlaceShip(shipInfo);
                    _currentId++;
                }
            }
            else throw new Exception("Invalid map.");
        }
        private void PlaceShip(ShipInfo shipInfo)
        {
            var length = (int)shipInfo.ShipType;
            var dir = (int)shipInfo.Direction;

            for (int i = 0; i < length; i++)
            {
                var col = shipInfo.Position.Column + _dx[dir] * i;
                var row = shipInfo.Position.Row + _dy[dir] * i;
                if (col >= 0 && col < _width && row >= 0 && row < _height)
                    _map[row, col] = shipInfo.Id;
                else
                    throw new Exception("Invalid map.");
            }
        }

    }
}
