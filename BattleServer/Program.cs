using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new BattleServer("0.0.0.0", 4444);
            server.Start();
        }
    }
}
