using BotterNet.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Encoders
{
    public interface IMessageEncoder
    {
        byte[] Encode(BaseMessage message);
    }
}
