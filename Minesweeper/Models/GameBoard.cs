using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

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
            
            GenerateField();
            CountNeighbors();
        }

        public int Cols { get; }
        public int Rows { get; }

        /*
         * Изменить метод генерации поля, так как текущий метод не учитывает сколько
         * бомб необходимо установить с текущей конфигурацией игры
         */
        private void GenerateField()
        {
            var rnd = new Random();
            _cells = new Cell[_gameConfiguration.Weight, _gameConfiguration.Height];

            for (int i = 0; i < _gameConfiguration.Weight; i++)
            {
                for (int j = 0; j < _gameConfiguration.Height; j++)
                {
                    var isEnemy = rnd.NextDouble() <= 0.2;
                    _cells[i, j] = new Cell(new Point(i, j), isEnemy);
                }
            }
        }

        public void CountNeighbors() => ForEach(x => CountNeighbors(x));

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
            Queue<Cell> q = new Queue<Cell>();
            q.Enqueue(_cells[x, y]);

            while (q.Count != 0)
            {
                var cell = q.Dequeue();
                cell.Reveal();

                if (cell.CountNeighboors == 0)
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
            if (cell.IsBomb)
            {
                return;
            }

            for (int xoff = -1; xoff <= 1; xoff++)
            {
                for (int yoff = -1; yoff <= 1; yoff++)
                {
                    var x = (int) cell.Coordinates.X + xoff;
                    var y = (int) cell.Coordinates.Y + yoff;

                    if (x > -1 && x < Cols && y > -1 && y < Rows)
                    {
                        if (_cells[x, y].IsBomb)
                        {
                            cell.CountNeighboors += 1;
                        }
                    }
                }
            }
        }

        public Cell GetCellByCoord(int x_coord, int y_coord)
        {
            return _cells[x_coord, y_coord];
        }
    }
}
