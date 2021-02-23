namespace Minesweeper.Models
{
    public class GameConfiguration
    {
        public int Height { get; }
        public int Weight { get; }
        public int MinesCount { get; }

        public GameConfiguration(int height, int weight, int minesCount)
        {
            Height = height;
            Weight = weight;
            MinesCount = minesCount;
        }
    }
}