using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BotterNet;
using BotterNet.Messages;

namespace DefaultBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _port = 4444;
        private int _bufferSize = 1024;
        private GameProcessor _processor = new GameProcessor();
        public MainWindow()
        {
            InitializeComponent();
        }

        protected void SocketProcess()
        {
            try
            {
                var receiver = new MessageReceiver();
                receiver.OnMessageReceived += OnDataReceived;

                var client = new TcpClient();
                client.Connect(_processor.ServerAddress, _port);

                var stream = client.GetStream();
                var loginMsg = new LoginMessage { IdMessage = 1, UserName = _processor.Name };
                var bytes = loginMsg.GetBytes();
                stream.Write(bytes, 0, bytes.Length);

                while (!_processor.IsGameEnd)
                {
                    
                    var buffer = new byte[_bufferSize];
                    int bytesCount = stream.Read(buffer, 0, _bufferSize);
                    receiver.OnReceivedBytes(buffer, bytesCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnDataReceived(BaseMessage message)
        {
            if (message is LoginMessage)
            {
                if (_processor.OnLoginResponse != null)
                    _processor.OnLoginResponse(message.Code, true);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _processor.Name = TxtName.Text;
            _processor.ServerAddress = TxtServerIp.Text;

            var t = new Task(() => SocketProcess());
            t.Start();
        }
    }
}
