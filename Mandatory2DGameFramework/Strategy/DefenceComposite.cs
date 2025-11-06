using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.defence
{
    // Composite: Til at håndtere flere forsvarsgenstande (f.eks. Brystplade + Hjelm)
    public class DefenceComposite : IDefenceStrategy
    {
        private List<IDefenceStrategy> _defences = new List<IDefenceStrategy>();

        public void AddDefence(IDefenceStrategy defence)
        {
            _defences.Add(defence);
        }

        public void RemoveDefence(IDefenceStrategy defence)
        {
            _defences.Remove(defence);
        }

        public int ReduceDamage(int incomingDamage)
        {
            // Reducer skaden ved at køre den gennem alle forsvarsgenstande i rækkefølge
            int currentDamage = incomingDamage;

            foreach (var defence in _defences)
            {
                currentDamage = defence.ReduceDamage(currentDamage);
            }

            return currentDamage; // Den skade, der er tilbage at tage
        }

        public int GetDefenceValue()
        {
            // Returnerer summen af alle forsvarsværdier i samlingen
            return _defences.Sum(d => d.GetDefenceValue());
        }
    }
}