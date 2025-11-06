using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;
using Mandatory2DGameFramework.Logger; // *** TILFØJET: For at bruge MyLogger i Operator Overload ***
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.Cretures
{
    // Creature er abstrakt for at understøtte Template Method
    public abstract class Creature
    {
        // OBSERVER: Eventen som Observers abonnerer på
        public event EventHandler<CreatureHitEventArgs>? CreatureWasHit;

        public string Name { get; set; }
        public int HitPoint { get; set; }
        // Her skal du eventuelt have en MaxHitPoint, hvis du vil begrænse heling.
        // public int MaxHitPoint { get; set; }

        // STRATEGY: Properties til angrebs- og forsvarsstrategier (bruger interfaces)
        public IAttackStrategy? AttackStrategy { get; set; }
        public IDefenceStrategy? DefenceStrategy { get; set; }

        public Creature()
        {
            Name = string.Empty;
            HitPoint = 100;
            // MaxHitPoint = 100; // Hvis du har den
            AttackStrategy = null;
            DefenceStrategy = null;
        }

        // --- TEMPLATE METHOD (Skelettet for at UDDELE skade) ---

        /// <summary>
        /// Template Method: Definerer skelettet for at udføre et angreb.
        /// </summary>
        /// <returns>Den totale skade væsenet uddeler.</returns>
        public int Hit()
        {
            // Trin 1: Beregn grundskaden (Abstrakt trin)
            int baseDamage = CalculateBaseDamage();

            // Trin 2: Justér skaden med udstyr/strategi (Abstrakt trin, bruger Strategy)
            int finalDamage = ApplyAttackBonus(baseDamage);

            // Trin 3: Udfør en specialeffekt efter angrebet (Virtuelt 'Hook')
            PostAttackEffect();

            return finalDamage;
        }

        // --- TEMPLATE: Primitive Operations (Skal implementeres af subklasser) ---

        /// <summary>Definerer væsenets grundlæggende skade/styrke.</summary>
        protected abstract int CalculateBaseDamage();

        /// <summary>Justerer skaden baseret på våben/strategi (bruger Strategy-objektet).</summary>
        protected abstract int ApplyAttackBonus(int baseDamage);

        // --- TEMPLATE: Hook (Kan overrides) ---

        /// <summary>Valgfri handling efter angreb. (F.eks. heal, flygte).</summary>
        protected virtual void PostAttackEffect()
        {
            // Standard: Gør ingenting.
        }


        // --- OBSERVER & SKADESTAGNING ---

        /// <summary>
        /// Modtager skade, opdaterer HitPoint og notificerer Observers.
        /// </summary>
        /// <param name="incomingHit">Den rå mængde skade</param>
        public void ReceiveHit(int incomingHit)
        {
            int damageToTake = incomingHit;

            // Anvend Defence Strategy til at reducere skaden
            if (DefenceStrategy != null)
            {
                damageToTake = DefenceStrategy.ReduceDamage(incomingHit);
                // Logning af forsvaret:
                MyLogger.Instance.LogInfo($"{Name} brugte forsvar. Rå skade: {incomingHit}, Skade efter reduktion: {damageToTake}.");
            }

            if (damageToTake > 0)
            {
                HitPoint -= damageToTake;
                if (HitPoint < 0) HitPoint = 0;

                NotifyHit(damageToTake);
            }
        }

        // OBSERVER: Trigger metoden
        protected virtual void NotifyHit(int damageTaken)
        {
            // '?' sikrer, at der er abonnenter, før Invoke kaldes
            CreatureWasHit?.Invoke(this, new CreatureHitEventArgs(damageTaken, Name, HitPoint));
        }

        // --- OPERATOR OVERLOAD (Obligatorisk krav) ---

        /// <summary>
        /// Operator Overload: Overbelaster '+' operatoren for at tilføje HitPoints (heale).
        /// </summary>
        /// <param name="creature">Væsenet der skal heales.</param>
        /// <param name="healthBoost">Mængden af HitPoints, der skal tilføjes.</param>
        /// <returns>Det opdaterede Creature-objekt.</returns>
        public static Creature operator +(Creature creature, int healthBoost)
        {
            if (creature == null || healthBoost <= 0) return creature;

            // Tilføj HitPoint (du kan tilføje MaxHP begrænsning her)
            creature.HitPoint += healthBoost;

            MyLogger.Instance.LogInfo($"Operator Overload: {creature.Name} blev helet med {healthBoost} HP. Nyt HP: {creature.HitPoint}");

            return creature;
        }


        // --- ØVRIGE METODER ---

        public void Loot(WorldObject obj)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            // Oprettet for at vise de nye Strategy properties (selvom de er interfaces, vil ToString() fra deres konkrete klasse kaldes)
            return $"{{{nameof(Name)}={Name}, {nameof(HitPoint)}={HitPoint.ToString()}, {nameof(AttackStrategy)}={AttackStrategy}, {nameof(DefenceStrategy)}={DefenceStrategy}}}";
        }
    }
}