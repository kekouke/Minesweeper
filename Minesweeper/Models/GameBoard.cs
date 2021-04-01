using System;
using System.Collections.Generic;
using System.Windows;
using Minesweeper.Enums;

namespace Minesweeper.Models
{
    public class GameBoard
    {
        private readonly GameConfiguration _gameConfiguration;
        
        private Cell[,] _cells;
        
        public GameBoard(GameConfiguration gameConfiguration)
        {
            _gameConfiguration = gameConfiguration;
            Cols = gameConfiguration.Weight;
            Rows = gameConfiguration.Height;
            
            InitializeField();
            CountNeighbors();
        }

        public int Cols { get; }
        public int Rows { get; }
        
        private void InitializeField()
        {
            var rnd = new Random();
            
            _cells = new Cell[_gameConfiguration.Weight, _gameConfiguration.Height];

            for (int i = 0; i < _gameConfiguration.Weight; i++)
            {
                for (int j = 0; j < _gameConfiguration.Height; j++)
                {
                    _cells[i, j] = new Cell(new Point(i, j), CellType.Free);
                }
            }
            
            int minesCount = _gameConfiguration.MinesCount;

            while (minesCount > 0)
            {
                int x = rnd.Next() % Cols;
                int y = rnd.Next() % Rows;

                var cell = GetCellByCoord(x, y);
                
                if (cell.Type == CellType.Mine)
                    continue;

                cell.Type = CellType.Mine;
                minesCount--;

            }
        }

        private void CountNeighbors() => ForEach(x => CountNeighbors(x));

        public void ForEach(Action<Cell> action)
        {
            for (int i = 0; i < Cols; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    action.Invoke(_cells[i, j]);
                }
            }
        }

        // TODO
        public void Reveal(int x, int y)
        {
            var q = new Queue<Cell>();
            q.Enqueue(_cells[x, y]);

            while (q.Count != 0)
            {
                var cell = q.Dequeue();
                cell.Reveal();

                if (cell.Type == CellType.Free)
                {
                    for (int xoff = -1; xoff <= 1; xoff++)
                    {
                        for (int yoff = -1; yoff <= 1; yoff++)
                        {
                            var x_cord = (int) cell.Coordinates.X + xoff;
                            var y_cord = (int) cell.Coordinates.Y + yoff;

                            if (x_cord > -1 && x_cord < Cols && y_cord > -1 && y_cord < Rows)
                            {

                                if (!_cells[x_cord, y_cord].IsRevealed)
                                {
                                    q.Enqueue(_cells[x_cord, y_cord]);
                                }
                            }

                        }
                    }
                }

            }
        }

        private void CountNeighbors(Cell cell)
        {
            if (cell.Type == CellType.Mine)
                return;

            for (int xoff = -1; xoff <= 1; xoff++)
            {
                for (int yoff = -1; yoff <= 1; yoff++)
                {
                    var x = (int) cell.Coordinates.X + xoff;
                    var y = (int) cell.Coordinates.Y + yoff;

                    if (GetCellByCoord(x, y) != null)
                    {
                        if (_cells[x, y].Type == CellType.Mine)
                        {
                            cell.CountNeighboors += 1;
                        }
                    }
                }
            }

            if (cell.CountNeighboors > 0)
            {
                cell.Type = CellType.Neighboor;
            }
        }

        public Cell GetCellByCoord(int x_coord, int y_coord)
        {
            if (x_coord < 0 || x_coord >= Cols || y_coord < 0 || y_coord >= Rows)
                return null;
            
            return _cells[x_coord, y_coord];
        }
    }
}
