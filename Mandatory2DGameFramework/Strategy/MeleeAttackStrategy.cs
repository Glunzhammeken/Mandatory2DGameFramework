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
            // Simpel bonus: Våbnets Hit-værdi direkte.
            // Kunne også inkludere Creature's styrke-statistik.
            return _weapon.Hit;
        }
    }
}