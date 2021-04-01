using Minesweeper.Models;

namespace Minesweeper.Services
{
    public class GameConfigurationService : IGameConfigurationService
    {
        public GameConfiguration GetBeginnerConfig 
            => new GameConfiguration(8, 8, 10, "Beginner");

        public GameConfiguration GetAdvancedConfig 
            => new GameConfiguration(16, 16, 40, "Advanced");
        
        public GameConfiguration GetExpertConfig 
            => new GameConfiguration(24, 24, 99, "Expert");
    }
}