using BotterNet.Exceptions;
using BotterNet.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Decoder
{
    public class LoginDecoder: BattleDecoder
    {
        public BattleMessage Parse(byte[] content)
        {
            var binReader = new BinaryReader(new MemoryStream(content));

            return new LoginMessage() {
                UserName = Encoding.ASCII.GetString(content)
            };
        }
    }
}
