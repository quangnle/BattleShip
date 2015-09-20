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
            var host = "0.0.0.0";
            var port = 9999;

            if (args.Length > 1)
                host = args[0];

            if (args.Length > 2)
                port = Int32.Parse(args[1]);

            var server = new BattleServer(host, port);
            server.Start();
        }
    }
}
