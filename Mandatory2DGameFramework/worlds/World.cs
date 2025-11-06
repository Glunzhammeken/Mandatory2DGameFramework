using Mandatory2DGameFramework.Configuration;
using Mandatory2DGameFramework.model.Cretures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.worlds
{
    /// <summary>
    /// Repræsenterer spillets 2D-verden og holder styr på alle objekter og væsener.
    /// Konfigureres via XML.
    /// </summary>
    public class World
    {
        // MaxX og MaxY indlæses fra XML. Private settere sikrer, at de kun kan ændres 
        // internt (typisk i konstruktøren).
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }
        public string CurrentGameLevel { get; private set; } // Henter GameLevel fra XML

        // world objects
        private List<WorldObject> _worldObjects;
        // world creatures
        private List<Creature> _creatures;

        /// <summary>
        /// Opretter en ny verden ved at indlæse dimensioner og spilniveau fra GameConfig.xml.
        /// Hvis indlæsningen fejler, bruges standardværdier.
        /// </summary>
        public World()
        {
            // Indlæs konfiguration ved oprettelse (bruger den statiske loader)
            var config = GameConfigLoader.LoadConfig();

            // Tildel værdier fra konfigurationen. Bruger fallback (80x40) hvis XML fejler.
            MaxX = config.MaxX > 0 ? config.MaxX : 80;
            MaxY = config.MaxY > 0 ? config.MaxY : 40;
            CurrentGameLevel = config.GameLevel ?? "Normal";

            _worldObjects = new List<WorldObject>();
            _creatures = new List<Creature>();
        }

        // Du kan også inkludere metoder til at tilføje/fjerne objekter her:

        public void AddWorldObject(WorldObject obj)
        {
            _worldObjects.Add(obj);
        }

        public void AddCreature(Creature creature)
        {
            _creatures.Add(creature);
        }


        public override string ToString()
        {
            // Tilføj CurrentGameLevel for fuldstændighed
            return $"{{{nameof(MaxX)}={MaxX.ToString()}, {nameof(MaxY)}={MaxY.ToString()}, {nameof(CurrentGameLevel)}={CurrentGameLevel}}}";
        }
    }
}