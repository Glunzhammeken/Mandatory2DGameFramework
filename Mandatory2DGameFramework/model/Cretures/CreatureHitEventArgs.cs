using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.Cretures
{
    /// <summary>
    /// Bruges til at overføre information om en skadesbegivenhed til Observers.
    /// Krævet af Observer-mønsteret.
    /// </summary>
    public class CreatureHitEventArgs : EventArgs
    {
        public int DamageTaken { get; }
        public string CreatureName { get; }
        public int CurrentHitPoints { get; }

        public CreatureHitEventArgs(int damage, string name, int currentHp)
        {
            DamageTaken = damage;
            CreatureName = name;
            CurrentHitPoints = currentHp;
        }
    }
}