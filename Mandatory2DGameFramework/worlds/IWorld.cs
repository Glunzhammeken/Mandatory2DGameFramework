using Mandatory2DGameFramework.model.Cretures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.worlds
{
    public interface IWorld
    {
        int MaxX { get; }
        int MaxY { get; }
        string CurrentGameLevel { get; }
        void AddCreature(Creature creature);
        void AddWorldObject(WorldObject obj);
       
    }
}
