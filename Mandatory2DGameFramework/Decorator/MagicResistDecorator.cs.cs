
using Mandatory2DGameFramework.model.defence;

namespace Mandatory2DGameFramework.Decorator
{
    /// <summary>
    /// En KONKRET decorator, der tilføjer magisk resistens oven på et andet forsvar.
    /// </summary>
    public class MagicResistDecorator : DefenceDecorator
    {
        private int _magicResistAmount;

        public MagicResistDecorator(IDefenceStrategy defence, int magicResistAmount) : base(defence)
        {
            _magicResistAmount = magicResistAmount;
        }

        /// <summary>
        /// Overskriver basismetoden. Først kalder den _wrappee's metode,
        /// derefter tilføjer den sin EGEN reduktion.
        /// </summary>
        public override int ReduceDamage(int incomingDamage)
        {
            // 1. Få skaden reduceret af det, vi "wrapper" (f.eks. et skjold)
            int damageAfterBaseDefence = _wrappee.ReduceDamage(incomingDamage);

            // 2. Anvend VORES EGEN logik (yderligere magisk reduktion)
            int finalDamage = damageAfterBaseDefence - _magicResistAmount;

            // Log det evt.
            // MyLogger.Instance.LogInfo($"Magic Resist blokerede yderligere {_magicResistAmount} skade.");

            return Math.Max(0, finalDamage); // Sørg for ikke at returnere negativ skade
        }

        /// <summary>
        /// Vi kan også forstærke GetDefenceValue
        /// </summary>
        public override int GetDefenceValue()
        {
            // Returnerer basis-forsvaret PLUS vores egen bonus
            return _wrappee.GetDefenceValue() + _magicResistAmount;
        }
    }
}