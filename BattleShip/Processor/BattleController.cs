using BattleShip.AIInterface;
using BattleShip.BattleEventArguments;
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
        private PlayerController[] _players = new PlayerController[2];
        public OnAttackHandler OnAttack;
        public OnBattleEndHandler OnBattleEnd;

        public BattleMap MapPlayerA { get { return _players[0].Map; } }
        public BattleMap MapPlayerB { get { return _players[1].Map; } }

        //public delegate void UpdateMapDelegate();
        //public delegate void SwitchTurnDelegate(int playerId);
        //public delegate void ShootDelegate(int row, int column);

        public event EventHandler OnMapUpdated;
        public event EventHandler OnTurnSwitched;
        public event EventHandler OnShot;

        public void Initialize(IPlayer player1, IPlayer player2)
        {
            try
            {
                _players[0] = new PlayerController(player1, player1.LoadMap());
                _players[1] = new PlayerController(player2, player2.LoadMap());
            }
            catch (Exception) { throw; }
            
        }
        public void Battle()
        {
            Random rnd = new Random();
            try
            {
                var currentIdx = 0;
                if (OnTurnSwitched != null)
                    OnTurnSwitched(this, new SwitchTurnArgs() { PlayerId = currentIdx });

                while (!_players[0].IsDead() && !_players[1].IsDead())
                {
                    var currentPlayer = _players[currentIdx];
                    var opponent = _players[(currentIdx + 1) % 2];

                    var pos = currentPlayer.GetMove(rnd);

                    if (OnShot != null)
                    {
                        OnShot(this, new ShotEventArgs() { Row = pos.Row, Column = pos.Column });
                        Thread.Sleep(400);
                    }

                    var hitInfo = opponent.GetShot(pos);
                    currentPlayer.Update(hitInfo);

                    if (OnAttack != null)
                        OnAttack(currentPlayer.Name, pos, hitInfo.IsHit);

                    if (!hitInfo.IsHit)
                    {
                        currentIdx = (currentIdx + 1) % 2;
                        if (OnTurnSwitched != null)
                            OnTurnSwitched(this, new SwitchTurnArgs() { PlayerId = currentIdx });
                    }

                    if (OnMapUpdated != null)
                        OnMapUpdated(this, null);

                    Thread.Sleep(400);
                }

                var winner = _players[0].Name;
                if (_players[0].IsDead())
                    winner = _players[1].Name;

                if (OnBattleEnd != null)
                    OnBattleEnd(winner);
            }
            catch (Exception)
            { 
                throw;
            }
            
        }
    }
}
