using BotterNet.Decoder;
using BotterNet.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet
{
    public class MessageReceiver
    {
        private List<byte> _buffer = new List<byte>();
        //private Dictionary<MessageCode, BattleDecoder> _decoders = new Dictionary<MessageCode, BattleDecoder>();
        BattleDecoder _decoder = new GeneralMessageDecoder();
        
        public delegate void MessageReceivedDelegate(BattleMessage message);
        public MessageReceivedDelegate OnMessageReceived;

        public void OnReceivedBytes(byte[] buffer, int numBytes)
        {
            var receivedBuffer = buffer.Take(numBytes).ToArray();

            Console.WriteLine("Received {0} bytes: {1}", numBytes, String.Join(" ", receivedBuffer.Select(b => b.ToString("X2"))));

            _buffer.AddRange(receivedBuffer);

            if (_buffer.Count < BattleMessage.HeaderLength)
                return;

             var binReader = new BinaryReader(new MemoryStream(_buffer.ToArray()));
            
            // header
            var length = binReader.ReadInt32();

            if (_buffer.Count >= BattleMessage.HeaderLength + length)
            {
                // message body
                var buf = binReader.ReadBytes(length);

                var message = _decoder.Parse(buf);

                if (OnMessageReceived != null)
                    OnMessageReceived(message);

                _buffer = _buffer.GetRange(BattleMessage.HeaderLength, _buffer.Count - BattleMessage.HeaderLength - length);
            }
        }

        //public void AttachDecoder(MessageCode messageCode, BattleDecoder decoder)
        //{
        //    _decoders[messageCode] = decoder;
        //}
    }
}
