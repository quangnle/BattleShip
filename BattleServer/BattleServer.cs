using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BattleServer
{
    class BattleServer
    {
        private Socket _socket;

        public BattleServer(string host, int port)
        {
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(host), port));
        }

        public void Start()
        {
            _socket.Listen(1000);

            var clientHandler = _socket.Accept();

            var buffer = new byte[1024];

            //clientHandler.Receive(buffer)
        }
    }
}
