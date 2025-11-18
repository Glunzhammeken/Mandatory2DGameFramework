using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;
using System;

namespace Mandatory2DGameFramework.Factory
{
    public static class WorldObjectFactory
    {
        /// <summary>
        /// Genererer en WorldObject baseret på den ønskede type og konfigurerer den.
        /// </summary>
        /// <param name="objectType">Strengen, der repræsenterer den ønskede type (f.eks. "Sword", "Shield").</param>
        /// <param name="name">Navnet på objektet.</param>
        /// <param name="value">Den primære værdi (f.eks. Hit for AttackItem, ReduceHitPoint for DefenceItem).</param>
        /// <returns>En ny WorldObject instans, eller null hvis typen er ukendt.</returns>
        public static WorldObject CreateObject(string objectType, string name, int value)
        {
           

            switch (objectType.ToLower())
            {
                case "sword":
                case "axe":
                    
                    return new AttackItem
                    {
                        Name = name,
                        Hit = value,
                        Range = 1, 
                        Lootable = true
                    };

                case "shield":
                case "armor":
                    
                    return new DefenceItem
                    {
                        Name = name,
                        ReduceHitPoint = value,
                        Lootable = true
                    };

                default:
                    
                    return null;
            }
        }
    }
}