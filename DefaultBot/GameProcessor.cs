using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultBot
{
    public delegate void LoginResponseHandler(int msgId, bool success);
    //protected delegate void AttackHandler(int msgId, int row, int col);
    //protected delegate void AttackResponseHandler(int msgId, HitInfo hitInfo);

    public class GameProcessor
    {
        public string Name { get; set; }
        public bool IsGameEnd { get; set; }

        public LoginResponseHandler OnLoginResponse;
    }
}
