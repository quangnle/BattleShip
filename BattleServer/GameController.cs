using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleServer
{
    class GameController
    {
        private ClientProfile _profileA;
        private ClientProfile _profileB;

        delegate void UpdateFunc();

        GameState _state;
        Dictionary<GameState, UpdateFunc> _updateFunctions = new Dictionary<GameState, UpdateFunc>();
        bool _isRunning;

        public GameController(ClientProfile profileA, ClientProfile profileB)
        {
            _profileA = profileA;
            _profileB = profileB;

            _state = GameState.Unready;
            _isRunning = true;

            _updateFunctions[GameState.Unready] = UpdateUnready;
            _updateFunctions[GameState.WaitForArmyFormations] = UpdateWaitForArmyFormations;
            _updateFunctions[GameState.KickoffMatch] = UpdateKickoffMatch;
            _updateFunctions[GameState.WaitForConfirmations] = UpdateWaitForConfirmations;
            _updateFunctions[GameState.GetMoveFromA] = UpdateGetMoveFromA;
            _updateFunctions[GameState.GetMoveFromB] = UpdateGetMoveFromB;
            _updateFunctions[GameState.ReportGameResult] = UpdateReportGameResult;
        }

        public void Start()
        {
            while(_isRunning)
            {
                _updateFunctions[_state]();

                Thread.Sleep(67);
            }
        }

        private void UpdateUnready()
        {
            _state = GameState.WaitForArmyFormations;
        }

        private void UpdateWaitForArmyFormations()
        {

        }

        private void UpdateKickoffMatch()
        {

        }

        private void UpdateWaitForConfirmations()
        {

        }

        private void UpdateGetMoveFromA()
        {

        }

        private void UpdateGetMoveFromB()
        {

        }

        private void UpdateReportGameResult()
        {

        }
    }
}
