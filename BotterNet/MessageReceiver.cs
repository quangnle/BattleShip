using BotterNet.Decoder;
using BotterNet.Message;
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
        private Dictionary<MessageCode, BattleDecoder> _decoders = new Dictionary<MessageCode, BattleDecoder>();
        
        public delegate void MessageReceivedDelegate(BattleMessage message);
        public MessageReceivedDelegate OnMessageReceived;

        public void OnReceivedBytes(byte[] buffer, int numBytes)
        {
            _buffer.AddRange(buffer.Take(numBytes).ToArray());

            if (_buffer.Count < sizeof(Int32))
                return;

             var binReader = new BinaryReader(new MemoryStream(_buffer.ToArray()));
            
            // header
            var length = binReader.ReadInt32();
            var headerLength = sizeof(Int32) + sizeof(Int16);

            if (_buffer.Count >= headerLength + length)
            {
                var messageCode = (MessageCode) binReader.ReadInt16();

                // message body
                var buf = binReader.ReadBytes(length);

                var message = _decoders[messageCode].Parse(buf);

                if (OnMessageReceived != null)
                    OnMessageReceived(message);

                _buffer = _buffer.GetRange(headerLength, _buffer.Count - headerLength - length);
            }
        }

        public void AttachDecoder(MessageCode messageCode, BattleDecoder decoder)
        {
            _decoders[messageCode] = decoder;
        }
    }
}
