using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Minesweeper.Models
{
    public class Field : List<List<Cell>>
    {
        public Field(int cols, int rows)
        {
            Cols = cols;
            Rows = rows;
        }

        public int Cols { get; set; }
        public int Rows { get; set; }

        public void CountNeighbors() => ForEach(x => CountNeighbors(x));

        public void ForEach(Action<Cell> action)
        {
            for (int i = 0; i < Cols; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    action.Invoke(this[i][j]);
                }
            }
        }

        // TODO
        public void Reveal(int x, int y)
        {
            Queue<Cell> q = new Queue<Cell>();
            q.Enqueue(this[x][y]);

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
                            int x_cord = cell.X + xoff;
                            int y_cord = cell.Y + yoff;

                            if (x_cord > -1 && x_cord < Cols && y_cord > -1 && y_cord < Rows)
                            {

                                if (!this[x_cord][y_cord].IsRevealed)
                                {
                                    q.Enqueue(this[x_cord][y_cord]);
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
                    int x = cell.X + xoff;
                    int y = cell.Y + yoff;

                    if (x > -1 && x < Cols && y > -1 && y < Rows)
                    {
                        if (this[x][y].IsBomb)
                        {
                            cell.CountNeighboors += 1;
                        }
                    }
                }
            }
        }
    }
}
