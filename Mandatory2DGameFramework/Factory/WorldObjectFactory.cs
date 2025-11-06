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
            // Normalt ville logikken for at vælge typen være mere avanceret,
            // f.eks. baseret på XML-konfigurationer eller Enum.

            switch (objectType.ToLower())
            {
                case "sword":
                case "axe":
                    // Factory skaber AttackItem
                    return new AttackItem
                    {
                        Name = name,
                        Hit = value,
                        Range = 1, // Standardværdi for tæt kamp
                        Lootable = true
                    };

                case "shield":
                case "armor":
                    // Factory skaber DefenceItem
                    return new DefenceItem
                    {
                        Name = name,
                        ReduceHitPoint = value,
                        Lootable = true
                    };

                default:
                    // Du kan eventuelt logge en fejl her ved hjælp af din MyLogger Singleton
                    // MyLogger.Instance.LogError($"Ukendt WorldObject type forsøgt oprettet: {objectType}");
                    return null;
            }
        }
    }
}