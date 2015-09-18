using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Messages
{
    class AttackMessage: BaseMessage
    {
        public override int Code
        {
            get { return (int) MessageCode.Attack; }
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}
