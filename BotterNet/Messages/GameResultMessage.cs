using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Messages
{
    class GameResultMessage: BattleMessage
    {
        public override int Code
        {
            get { return (int) MessageCode.GameResult; }
        }
    }
}
