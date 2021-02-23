using System.Windows;

namespace Minesweeper.Models
{
    public class Cell
    {
        public Point Coordinates { get; }
        public int CountNeighboors { get; set; }
        public bool IsBomb => IsEnemy;

        private bool IsEnemy { get; }
        public bool IsRevealed { get; set; }

        public Cell(Point position, bool isEnemy)
        {
            IsEnemy = isEnemy;

            Coordinates = position;
        }

        public void Reveal() => IsRevealed = true;
    }
}
