﻿using BotterNet;
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
        private IMessageDecoder _decoder;
        private IMessageEncoder _encoder;

        public delegate void LoginSuccessDelegate(string username, Socket socket);
        public LoginSuccessDelegate OnLoginSuccess;

        private bool _isAuthenticated = false;

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

            while (!(_socket.Available == 0 & _socket.Poll(1000, SelectMode.SelectRead)) && !_isAuthenticated)
            {
                var numBytes = _socket.Receive(buffer, 1024, SocketFlags.None);

                mr.OnReceivedBytes(buffer, numBytes);
            }

            Console.WriteLine("client disconnected");
        }

        private void ReceiveMessage(BaseMessage message)
        {
            if(message.GetType() == typeof(LoginMessage))
            {
                var loginMessage = message as LoginMessage;

                Console.WriteLine("Received LoginMessage(Username = {0})", loginMessage.UserName);

                if (OnLoginSuccess != null)
                    OnLoginSuccess(loginMessage.UserName, _socket);

                _isAuthenticated = true;
            }
        }
    }
}
