namespace Minesweeper.Models
{
    public class GameConfiguration
    {
        public int Height { get; }
        public int Weight { get; }
        public int MinesCount { get; }
        
        private readonly string _name;

        public GameConfiguration(int height, int weight, int minesCount, string name)
        {
            Height = height;
            Weight = weight;
            MinesCount = minesCount;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}