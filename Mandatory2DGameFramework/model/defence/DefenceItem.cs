using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.defence
{
    // *** Opdatering: Implementerer IDefenceStrategy ***
    public class DefenceItem : WorldObject, IDefenceStrategy
    {
        public int ReduceHitPoint { get; set; } // Hvor meget skade reduceres

        public DefenceItem()
        {
            Name = string.Empty;
            ReduceHitPoint = 0;
        }

        // --- Implementation af IDefenceStrategy ---

        /// <summary>
        /// Reducerer indkommende skade med DefenceItem'ets værdi.
        /// </summary>
        /// <param name="incomingDamage">Den rå mængde skade før reduktion.</param>
        /// <returns>Den resterende skade efter reduktion (kan ikke være negativ).</returns>
        public int ReduceDamage(int incomingDamage)
        {
            // Reducer skaden, men sørg for, at resultatet ikke bliver negativt
            int remainingDamage = incomingDamage - ReduceHitPoint;

            

            return Math.Max(0, remainingDamage);
        }

        /// <summary>
        /// Returnerer forsvarsværdien. Bruges af Decorator og Composite til totalberegning.
        /// </summary>
        /// <returns>DefenceItem'ets ReduceHitPoint-værdi.</returns>
        public int GetDefenceValue()
        {
            return ReduceHitPoint;
        }

        // --- Øvrige metoder ---

        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(ReduceHitPoint)}={ReduceHitPoint.ToString()}}}";
        }
    }
}