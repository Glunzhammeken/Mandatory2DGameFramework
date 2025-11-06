using Mandatory2DGameFramework.model.attack;

namespace Mandatory2DGameFramework.Strategy
{
    /// <summary>
    /// Konkret strategi for nærkampsangreb.
    /// Beregner skadebonus baseret på våbnets Hit-værdi.
    /// </summary>
    public class MeleeAttackStrategy : IAttackStrategy
    {
        private AttackItem _weapon;

        public MeleeAttackStrategy(AttackItem weapon)
        {
            _weapon = weapon;
        }

        public int CalculateDamageBonus()
        {
            
            return _weapon.Hit;
        }
    }
}