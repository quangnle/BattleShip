using BotterNet.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Encoders
{
    public class GeneralMessageEncoder: BattleEncoder
    {
        public byte[] Encode(BaseMessage message)
        {
            var bytes = message.GetBytes();
            
            var sendBytes = new byte[BaseMessage.HeaderLength + bytes.Length];
            
            Array.Copy(BitConverter.GetBytes(bytes.Length), sendBytes, sizeof(int));
            Array.Copy(bytes, 0, sendBytes, BaseMessage.HeaderLength, bytes.Length);

            return sendBytes;
        }
    }
}
