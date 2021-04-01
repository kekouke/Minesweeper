using System.Windows;
using Minesweeper.Models;
using Minesweeper.Services;

namespace Minesweeper
{
    public static class Settings
    {
        public static readonly Size CellSize = new Size(30, 30);
        
        public static readonly GameConfiguration DefaultGameConfig 
            = new GameConfigurationService().GetBeginnerConfig; 
    }
}