using BotterNet;
using BotterNet.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleServer
{
    class BattleServer
    {
        private Socket _socket;

        private List<ClientProfile> _clients = new List<ClientProfile>();

        public BattleServer(string host, int port)
        {
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(host), port));
        }

        public void Start()
        {
            _socket.Listen(1000);

            while (!(_socket.Available == 0 & _socket.Poll(1000, SelectMode.SelectRead)))
            {
                var clientSock = _socket.Accept();

                var remoteEndPoint = clientSock.RemoteEndPoint as IPEndPoint;
                Console.WriteLine("{0} connected from port {1}", remoteEndPoint.Address, remoteEndPoint.Port);

                var clientHandler = new ClientHandler(clientSock);
                clientHandler.OnLoginSuccess = OnClientAuthenticated;

                var thread = new Thread(() => clientHandler.Start());
                thread.Start();
            }
        }

        private void OnClientAuthenticated(string username, Socket socket)
        {
            _clients.Add(new ClientProfile() { Username = username, Handler = socket });

            if(_clients.Count >= 2)
            {
                StartGame(_clients[0], _clients[1]);
                _clients.RemoveRange(0, 2);
            }
        }

        private void StartGame(ClientProfile sideA, ClientProfile sideB)
        {
            Console.WriteLine("Paired {0} with {1}", sideA.Username, sideB.Username);

            var gameControl = new GameController(sideA, sideB);
            gameControl.Start();
        }
    }
}
