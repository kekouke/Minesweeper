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
        public bool IsFlagged { get; set; }

        public Cell(Point position, CellType cellType)
        {
            Type = cellType;
            Coordinates = position;
        }

        public void Reveal()
        {
            if (!IsFlagged)
                IsRevealed = true;
        }

        public void SetFlag()
        {
            IsFlagged = !IsFlagged;
        }
    }
}
