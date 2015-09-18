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
using BotterNet.Encoders;

namespace DefaultBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _host = "192.168.112.103";
        private int _port = 4444;
        private int _bufferSize = 1024;
        private GameProcessor _processor = new GameProcessor();

        private IMessageEncoder _encoder = new GeneralMessageEncoder();

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
                client.Connect(_host, _port);

                while (!_processor.IsGameEnd)
                {
                    var stream = client.GetStream();

                    var bytes = _encoder.Encode(new LoginMessage() { UserName = "Legos" });
                    stream.Write(bytes, 0, bytes.Length);

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

            var t = new Task(() => SocketProcess());
            t.Start();
        }
    }
}
