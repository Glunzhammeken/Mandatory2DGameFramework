using System;
using System.IO;
using System.Xml.Linq;
using Mandatory2DGameFramework.Config;
using Mandatory2DGameFramework.Logger; 

namespace Mandatory2DGameFramework.Configuration
{
    public  class GameConfigLoader : IGameConfigLoader
    {
        private const string ConfigFileName = "GameConfig.xml";

        /// <summary>
        /// Loader game configuration from XML file in root directory.
        /// Returns defaults on error.
        /// </summary>
        public (int MaxX, int MaxY, string GameLevel) LoadConfig()
        {
            try
            {
                
                var baseDir = AppContext.BaseDirectory ?? Environment.CurrentDirectory;
                var path = Path.Combine(baseDir, ConfigFileName);

                if (!File.Exists(path))
                {
                    MyLogger.Instance.LogWarning($"Config file not found at '{path}'. Using defaults.");
                    return (0, 0, "Default");
                }

                var doc = XDocument.Load(path);
                var config = doc.Element("GameConfiguration");

                if (config == null)
                {
                    MyLogger.Instance.LogError($"XML root 'GameConfiguration' not found in {path}.");
                    return (0, 0, "Default");
                }

                var worldElement = config.Element("World");
                int maxX = (int?)worldElement?.Attribute("MaxX") ?? 0;
                int maxY = (int?)worldElement?.Attribute("MaxY") ?? 0;

                var levelElement = config.Element("Settings");
                string gameLevel = (string?)levelElement?.Attribute("Level") ?? "Default";

                MyLogger.Instance.LogInfo($"Configuration loaded from '{path}': World({maxX}, {maxY}), Level: {gameLevel}");
                return (maxX, maxY, gameLevel);
            }
            catch (Exception ex)
            {
                MyLogger.Instance.LogCritical($"Critical error loading configuration: {ex.Message}");
                return (0, 0, "Default");
            }
        }
    }
}