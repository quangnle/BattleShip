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
    public delegate void MessageReceivedDelegate(BaseMessage message);

    public class MessageReceiver
    {
        private List<byte> _buffer = new List<byte>();
        
        private IMessageDecoder _decoder = new GeneralMessageDecoder();

        public IMessageDecoder Decoder
        {
            get
            {
                return _decoder;
            }
            set
            {
                _decoder = value;
            }
        }

        public MessageReceivedDelegate OnMessageReceived;

        public void OnReceivedBytes(byte[] buffer, int numBytes)
        {
            _buffer.AddRange(buffer.Take(numBytes).ToArray());

            if (_buffer.Count < BaseMessage.HeaderLength)
                return;

             var binReader = new BinaryReader(new MemoryStream(_buffer.ToArray()));
            
            // header
            var length = binReader.ReadInt32();

            if (_buffer.Count >= BaseMessage.HeaderLength + length)
            {
                // message body
                var buf = binReader.ReadBytes(length);

                var message = _decoder.Parse(buf);

                if (OnMessageReceived != null)
                    OnMessageReceived(message);

                _buffer = _buffer.GetRange(BaseMessage.HeaderLength, _buffer.Count - BaseMessage.HeaderLength - length);
            }
        }

    }
}
