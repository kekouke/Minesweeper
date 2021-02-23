using Minesweeper.Models;

namespace Minesweeper.Services
{
    public interface IGameConfigurationService
    {
        GameConfiguration GetBeginnerConfig { get; }
        
        GameConfiguration GetAdvancedConfig { get; }
        
        GameConfiguration GetExpertConfig { get; }
    }
}