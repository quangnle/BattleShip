using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Messages
{
    [Serializable]
    class AttackResponseMessage: BaseMessage
    {
        public override int Code
        {
            get { return (int) MessageCode.AttackResponse; }
        }

        public bool IsDestroyed { get; set; }
        public bool IsHit { get; set; }
    }
}
