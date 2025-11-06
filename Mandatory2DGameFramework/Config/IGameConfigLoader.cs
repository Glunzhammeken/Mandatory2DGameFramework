using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.Config
{
    public interface IGameConfigLoader
    {
        (int MaxX, int MaxY, string GameLevel) LoadConfig();
    }
}
