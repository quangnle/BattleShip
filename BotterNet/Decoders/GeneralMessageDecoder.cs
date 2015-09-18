using BotterNet.Exceptions;
using BotterNet.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Decoder
{
    public class GeneralMessageDecoder: IMessageDecoder
    {
        public BaseMessage Parse(byte[] content)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream(content))
            {
                return (bf.Deserialize(ms) as BaseMessage);
            }
        }
    }
}
