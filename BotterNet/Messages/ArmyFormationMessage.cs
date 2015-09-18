using BotterNet.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Messages
{
    class ArmyFormationMessage: BaseMessage
    {
        public override int Code
        {
            get { return (int) MessageCode.ArmyFormation; }
        }

        public BaseMapInfo Map { get; set; }
    }
}
