using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Commands;
using System.Windows;
using System.Windows.Input;
using Minesweeper.Models;

namespace Minesweeper
{
    public class GameController
    {
        public ICommand RestartGameCommand { get; set; }
        private readonly VisualHost Canvas;
        private Field Field { get; set; }

        public int Cols { get; set; } = 10;
        public int Rows { get; set; } = 10;

        // Width for Cell
        public double Width { get; set; } = 30;

        // Height for Cell
        public double Height { get; set; } = 30;


        public GameController(VisualHost canvas)
        {
            RestartGameCommand = new ClickCommand(Restart);
            Canvas = canvas;
            Field = GenerateField();
            Field.CountNeighbors();
            Invalidate();
        }

        public void Invalidate() => Canvas.Invalidate(Field);
        
        private void Restart()
        {
            Field = GenerateField();
            Field.CountNeighbors();
            Invalidate();
        }

        private Field GenerateField()
        {
            var rnd = new Random();
            var result = new Field(Cols, Rows);

            for (int i = 0; i < Cols; i++)
            {
                result.Add(new List<Cell>());
                for (int j = 0; j < Rows; j++)
                {
                    var isEnemy = rnd.NextDouble() <= 0.2;
                    var size = new Size(Width, Height);
                    result[i].Add(new Cell(new Point(i, j), size, isEnemy));
                }
            }

            return result;
        }

        public void HandleCellClick(Point clickPoint) //TODO (Index out of rabge)
        {
            int x = (int)Math.Floor(clickPoint.X / Width);
            int y = (int)Math.Floor(clickPoint.Y / Height);

            if (Field[x][y].IsBomb)
            {
                Field.ForEach(c => c.Reveal());
            }
            else
            {
                //Field[x][y].Reveal(); // Field.BFS(x, y);
                Field.Reveal(x, y);
            }

            Invalidate();
        }
    }
}
