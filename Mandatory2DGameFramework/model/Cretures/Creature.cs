using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;

using Mandatory2DGameFramework.worlds;
using Mandatory2DGameFramework.Logger; // *** TILFØJET: For at bruge MyLogger i Operator Overload ***
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mandatory2DGameFramework.Strategy;

namespace Mandatory2DGameFramework.model.Cretures
{
    // Creature er abstrakt for at understøtte Template Method
    public abstract class Creature : IAttackable
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


      

        public void Loot(WorldObject obj)
        {
            // --- Trin 1: Validering ---
            // Tjek om objektet overhovedet kan samles op
            if (!obj.Lootable)
            {
                MyLogger.Instance.LogWarning($"{Name} forsøgte at loote {obj.Name}, men det er ikke lootable.");
                return;
            }

            MyLogger.Instance.LogInfo($"{Name} looter {obj.Name}!");

            // --- Trin 2: Type-tjek og Strategy-tildeling ---

            // Er objektet et våben?
            if (obj is AttackItem lootedWeapon)
            {
                // Ja. Nu vælger vi en ANGRIBS-STRATEGI baseret på våbnet.
                // Vi bruger 'Range' til at beslutte, om det er Melee eller Ranged.
                if (lootedWeapon.Range > 1)
                {
                    this.AttackStrategy = new RangedAttackStrategy(lootedWeapon);
                    MyLogger.Instance.LogInfo($"{Name} har udstyret (Strategy): {lootedWeapon.Name} (Ranged).");
                }
                else
                {
                    this.AttackStrategy = new MeleeAttackStrategy(lootedWeapon);
                    MyLogger.Instance.LogInfo($"{Name} har udstyret (Strategy): {lootedWeapon.Name} (Melee).");
                }
                return; // Vi er færdige
            }

            // Er objektet en rustning?
            if (obj is DefenceItem lootedArmor)
            {
                // Ja. Nu håndterer vi FORSVARS-STRATEGIEN.
                // Dette er mere komplekst pga. Composite-mønsteret.

                // Scenarie 1: Vi har intet forsvar i forvejen.
                if (this.DefenceStrategy == null)
                {
                    // Nemt: Vores nye rustning ER vores strategi.
                    // (Dette virker, fordi DefenceItem implementerer IDefenceStrategy)
                    this.DefenceStrategy = lootedArmor;
                    MyLogger.Instance.LogInfo($"{Name} har udstyret sit første forsvar: {lootedArmor.Name}.");
                }
                // Scenarie 2: Vi har allerede en 'Composite' (en "samling" af rustning).
                else if (this.DefenceStrategy is DefenceComposite composite)
                {
                    // Nemt: Vi tilføjer bare den nye rustning til samlingen.
                    composite.AddDefence(lootedArmor);
                    MyLogger.Instance.LogInfo($"{lootedArmor.Name} blev tilføjet til {Name}'s rustnings-sæt.");
                }
                // Scenarie 3: Vi har ét enkelt item (f.eks. et skjold), men IKKE en composite.
                else
                {
                    // Vi skal nu opgradere til en composite!
                    MyLogger.Instance.LogInfo($"{Name} opretter et rustnings-sæt for at bære flere dele...");

                    // 1. Opret den nye "samling"
                    var newCompositeSet = new DefenceComposite();

                    // 2. Læg vores GAMLE item (skjoldet) i samlingen
                    newCompositeSet.AddDefence(this.DefenceStrategy);

                    // 3. Læg vores NYE item (støvlerne) i samlingen
                    newCompositeSet.AddDefence(lootedArmor);

                    // 4. Sæt "samlingen" som vores nye, aktive strategi
                    this.DefenceStrategy = newCompositeSet;
                }
                return; // Vi er færdige
            }

            // Hvis det er et andet lootable item (f.eks. en Potion),
            // kunne logikken tilføjes her.
            MyLogger.Instance.LogWarning($"Genstand {obj.Name} blev lootet, men er en ukendt type.");
        }

        public override string ToString()
        {
            // Oprettet for at vise de nye Strategy properties (selvom de er interfaces, vil ToString() fra deres konkrete klasse kaldes)
            return $"{{{nameof(Name)}={Name}, {nameof(HitPoint)}={HitPoint.ToString()}, {nameof(AttackStrategy)}={AttackStrategy}, {nameof(DefenceStrategy)}={DefenceStrategy}}}";
        }
    }
}