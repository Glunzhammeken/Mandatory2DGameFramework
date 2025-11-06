using Mandatory2DGameFramework.model.attack;
using System;

namespace Mandatory2DGameFramework.Strategy
{
    /// <summary>
    /// Konkret strategi for fjernangreb.
    /// Beregner skadebonus, evt. med en straf for lang rækkevidde.
    /// </summary>
    public class RangedAttackStrategy : IAttackStrategy
    {
        private AttackItem _weapon;

        public RangedAttackStrategy(AttackItem weapon)
        {
            _weapon = weapon;
        }

        public int CalculateDamageBonus()
        {
            // Simpel bonus: Giver kun 80% af Hit-værdien som rå skade for at simulere 
            // at rækkevidde/præcision er sværere.
            int baseDamage = _weapon.Hit;
            int finalDamage = (int)Math.Floor(baseDamage * 0.8);

            
            return finalDamage;
        }
    }
}