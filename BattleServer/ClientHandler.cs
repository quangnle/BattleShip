using BotterNet;
using BotterNet.Decoder;
using BotterNet.Encoders;
using BotterNet.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BattleServer
{
    class ClientHandler
    {
        private Socket _socket;
        private BattleDecoder _decoder;
        private BattleEncoder _encoder;

        public ClientHandler(Socket clientSock)
        {
            _socket = clientSock;

            _encoder = new GeneralMessageEncoder();
            _decoder = new GeneralMessageDecoder();
        }

        public void Start()
        {
            var buffer = new byte[1024];

            var mr = new MessageReceiver();
            mr.OnMessageReceived += ReceiveMessage;

            while (!(_socket.Available == 0 & _socket.Poll(1000, SelectMode.SelectRead)))
            {
                var numBytes = _socket.Receive(buffer, 1024, SocketFlags.None);

                mr.OnReceivedBytes(buffer, numBytes);
            }

            Console.WriteLine("client disconnected");
        }

        private void ReceiveMessage(BattleMessage message)
        {
            Console.WriteLine("Received message {0}", message.GetType());

            var bytes = _encoder.Encode(message);
            _socket.Send(bytes);
        }
    }
}
