using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotterNet.Core
{
    public class HitInfo
    {
        public Position Pos { get; set; }
        public bool IsHit { get; set; }
        public bool Destroyed { get; set; }
    }
}
