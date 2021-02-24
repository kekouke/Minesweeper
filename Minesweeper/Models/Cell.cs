using Minesweeper.Enums;
using System.Windows;

namespace Minesweeper.Models
{
    public class Cell
    {
        public Point Coordinates { get; }

        public CellType Type { get; set; }

        public int CountNeighboors { get; set; }

        public bool IsRevealed { get; set; }

        public Cell(Point position, CellType cellType)
        {
            Type = cellType;
            Coordinates = position;
        }

        public void Reveal() => IsRevealed = true;
    }
}
