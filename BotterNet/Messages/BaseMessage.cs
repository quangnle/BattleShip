using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Messages
{
    [Serializable]
    public abstract class BaseMessage
    {
        public static int HeaderLength = sizeof(Int32);

        public int IdMessage { get; set; }

        public abstract int Code { get; }

        public virtual byte[] GetBytes()
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                return ms.ToArray();
            }
        }
    }
}
