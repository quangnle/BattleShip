using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Message
{
    [Flags]
    public enum MessageCode
    {
        Login = 1,
        Attack = 2
    }
}
