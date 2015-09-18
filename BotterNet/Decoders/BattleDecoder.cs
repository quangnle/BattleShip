using BotterNet.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Decoder
{
    public interface BattleDecoder
    {
        BattleMessage Parse(byte[] content);
    }
}
