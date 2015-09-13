using BattleShip.AIInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Processor
{
    public class DllLoader
    {
        public static IPlayer LoadPlayer(string path)
        {
            Type objType = null;
            IPlayer player = null;
            try
            {
                Assembly assembly = null;
                assembly = Assembly.LoadFile(path);
                if (assembly != null)
                {
                    var tInfo = assembly.DefinedTypes.First();
                    objType = tInfo.AsType();
                }

                if (objType != null)
                    player = (IPlayer)Activator.CreateInstance(objType);
            }
            catch (Exception) { throw; }

            return player;
        }
    }
}
