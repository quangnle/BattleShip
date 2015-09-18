using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Message
{
    [Serializable]
    public class LoginMessage: BattleMessage
    {
        
        public override int Code { get { return (int)MessageCode.Login; } }
        public string UserName { get; set; }
    }
}
