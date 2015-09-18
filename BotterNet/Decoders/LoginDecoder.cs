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
            if (LoginMessage.Length != content.Length)
                throw new InvalidMessage();

            var binReader = new BinaryReader(new MemoryStream(content));

            return new LoginMessage() {
                Username = Encoding.ASCII.GetString(binReader.ReadBytes(LoginMessage.USERNAME_LENGTH)),
                Password = Encoding.ASCII.GetString(binReader.ReadBytes(LoginMessage.PASSWORD_LENGTH))
            };
        }
    }
}
