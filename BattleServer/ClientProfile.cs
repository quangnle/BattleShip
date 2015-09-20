using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BattleServer
{
    class ClientProfile
    {
        public string Username { get; set; }
        public Socket Handler { get; set; }
    }
}
