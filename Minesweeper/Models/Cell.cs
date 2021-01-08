using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Minesweeper.Models
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public int CountNeighboors { get; set; }
        public bool IsBomb => IsEnemy;

        private readonly Pen _pen;
        private Point Position { get; }
        private Size Size { get; }
        private bool IsEnemy { get; }
        public bool IsRevealed { get; set; }

        public Cell(Point pos, Size size, bool isEnemy)
        {
            Position = new Point(pos.X * size.Width, pos.Y * size.Height);
            Size = size;
            IsEnemy = isEnemy;
            X = (int)pos.X;
            Y = (int)pos.Y;

            _pen = new Pen(new SolidColorBrush(Colors.Black), 2);
        }

        // TODO: Refactor this method
        public void Draw(DrawingContext context)
        {
            Brush brush = GetCellColor();
            context.DrawRectangle(brush, _pen, new Rect(Position, Size));
            
            if (IsRevealed)
            {
                if (IsBomb)
                {
                    context.DrawEllipse(new SolidColorBrush(Colors.White), _pen, new Point(Position.X + Size.Width / 2, Position.Y + Size.Height / 2), Size.Width / 3, Size.Height / 3);
                }
                else if (CountNeighboors != 0)
                {
                    var typeface = new Typeface(new FontFamily("Times New Roman"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
                    var formattedText = new FormattedText($"{CountNeighboors}", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 20, new SolidColorBrush(Colors.Black), 1);
                    context.DrawText(formattedText, new Point(Position.X + Size.Width / 3, Position.Y));
                }
            }
        }

        public void Reveal() => IsRevealed = true;

        private Brush GetCellColor()
        {
            return IsRevealed ? new SolidColorBrush(Colors.Gray) : new SolidColorBrush(Colors.White);
        }
    }
}
