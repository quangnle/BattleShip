using BatterNet.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Messages
{
    class ArmyFormationMessage: BattleMessage
    {
        public override int Code
        {
            get { return (int) MessageCode.ArmyFormation; }
        }

        public BaseMapInfo Map { get; set; }
    }
}
