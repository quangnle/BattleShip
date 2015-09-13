using BattleShip.AIInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip.Processor
{
    public delegate void OnAttackHandler(string playName, Position pos, bool isHit);
    public class BattleController
    {
        private PlayerController _player1, _player2;
        public OnAttackHandler OnAttack;

        public void Initialize(string player1Name, IPlayer player1, string player2Name, IPlayer player2)
        {
            try
            {
                _player1 = new PlayerController(player1Name, player1, player1.LoadMap());
                _player2 = new PlayerController(player2Name, player2, player2.LoadMap());
            }
            catch (Exception) { throw; }
            
        }
        public int Battle()
        {
            Random rnd = new Random();
            try
            {
                var currentPlayer = _player1;
                var opponent = _player2;

                while (!_player1.IsDead() && !_player2.IsDead())
                {
                    var pos = currentPlayer.GetMove(rnd);

                    var isHit = opponent.GetShot(pos);
                    if (OnAttack != null)
                        OnAttack(currentPlayer.Name, pos, isHit);

                    if (!isHit)
                    {
                        var tmp = currentPlayer;
                        currentPlayer = opponent;
                        opponent = tmp;
                    }
                }

                if (_player1.IsDead()) return 1;
                return 2;
            }
            catch (Exception)
            { 
                throw;
            }
            
        }
    }
}
