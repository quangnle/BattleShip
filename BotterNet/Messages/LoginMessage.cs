using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Message
{
    public class LoginMessage: BattleMessage
    {
        public static Int16 Code
        {
            get { return (Int16) MessageCode.Login; }
        }

        public string Username { get; set; }

        public byte[] GetBytes()
        {
            var headerLength = sizeof(Int32) + sizeof(Int16);
            var usernameBytes = Encoding.ASCII.GetBytes(Username.ToArray());

            var packetLength = Convert.ToInt32(headerLength + usernameBytes.Length);

            var finalBuffer = new byte[packetLength];

            Array.Copy(BitConverter.GetBytes(packetLength), 0, finalBuffer, 0, sizeof(Int32));
            Array.Copy(BitConverter.GetBytes(Code), 0, finalBuffer, sizeof(Int32), sizeof(Int16));

            Array.Copy(usernameBytes, 0, finalBuffer, headerLength, usernameBytes.Length);

            return finalBuffer;
        }
    }
}
