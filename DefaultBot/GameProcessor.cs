using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotterNet.Core;

namespace DefaultBot
{
    public delegate void LoginResponseHandler(int msgId, bool success);
    public delegate void AttackHandler(int msgId, Position pos);
    public delegate void AttackResponseHandler(int msgId, HitInfo hitInfo);

    public class GameProcessor
    {
        public string Name { get; set; }
        public string ServerAddress { get; set; }
        public bool IsGameEnd { get; set; }

        public LoginResponseHandler OnLoginResponse;
    }
}
