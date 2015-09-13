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
    public delegate void OnAttackHandler(string playerName, Position pos, bool isHit);
    public delegate void OnBattleEndHandler(string winner);
    public class BattleController
    {
        private PlayerController _player1, _player2;
        public OnAttackHandler OnAttack;
        public OnBattleEndHandler OnBattleEnd;

        public void Initialize(IPlayer player1, IPlayer player2)
        {
            try
            {
                _player1 = new PlayerController(player1, player1.LoadMap());
                _player2 = new PlayerController(player2, player2.LoadMap());
            }
            catch (Exception) { throw; }
            
        }
        public string Battle()
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

                var winner = _player1.Name;
                if (_player1.IsDead()) 
                    winner = _player2.Name;

                if (OnBattleEnd != null)
                    OnBattleEnd(winner);

                return winner;
            }
            catch (Exception)
            { 
                throw;
            }
            
        }
    }
}
