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
        public static readonly int USERNAME_LENGTH = 32;
        public static readonly int PASSWORD_LENGTH = 32;

        public static Int32 Length
        {
            get { return (USERNAME_LENGTH + PASSWORD_LENGTH); }
        }

        public static Int16 Code
        {
            get { return (Int16) MessageCode.Login; }
        }

        public string Username { get; set; }
        public string Password { get; set; }

        
        public byte[] GetBytes()
        {
            var usernameBytes = Encoding.ASCII.GetBytes(Username.ToArray());
            Array.Resize<byte>(ref usernameBytes, USERNAME_LENGTH);

            var passwordBytes = Encoding.ASCII.GetBytes(Password.ToArray());
            Array.Resize<byte>(ref passwordBytes, PASSWORD_LENGTH);

            var headerLength = sizeof(Int32) + sizeof(Int16);

            var finalBuffer = new byte[headerLength + Length];
            Array.Copy(BitConverter.GetBytes(Length), 0, finalBuffer, 0, sizeof(Int32));
            Array.Copy(BitConverter.GetBytes(Code), 0, finalBuffer, sizeof(Int32), sizeof(Int16));
            Array.Copy(usernameBytes, 0, finalBuffer, headerLength, USERNAME_LENGTH);
            Array.Copy(passwordBytes, 0, finalBuffer, headerLength + USERNAME_LENGTH, PASSWORD_LENGTH);

            return finalBuffer;
        }
    }
}
