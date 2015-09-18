using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Messages
{
    [Flags]
    public enum MessageCode
    {
        // client
        Login = 100,
        Attack = 101,
        Ready = 102,

        // server
        LoginResult = 200,
        StartGame = 201,
        GetAttack = 202,
        AttackResponse = 203,
    }
}
