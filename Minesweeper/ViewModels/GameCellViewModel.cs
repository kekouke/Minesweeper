using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Minesweeper.Enums;
using Minesweeper.Models;

namespace Minesweeper
{
    public class GameCellViewModel
    {
        private Cell _cell;
        
        private readonly Pen _pen;
        
        private Point Position { get; }
        
        private Size Size { get; }
        
        public GameCellViewModel(Cell cell, Size size)
        {
            _cell = cell;
            /////////////
            _pen = new Pen(new SolidColorBrush(Colors.Black), 2);
            
            Size = size;
            Position = new Point(cell.Coordinates.X * size.Width, cell.Coordinates.Y * size.Height);
        }
        
        // TODO: Refactor this method
        public void Draw(DrawingContext context)
        {
            Brush brush = GetCellColor();
            context.DrawRectangle(brush, _pen, new Rect(Position, Size));
            
            if (_cell.IsRevealed)
            {
                if (_cell.Type == CellType.Mine)
                {
                    context.DrawEllipse(new SolidColorBrush(Colors.White), _pen, new Point(Position.X + Size.Width / 2, Position.Y + Size.Height / 2), Size.Width / 3, Size.Height / 3);
                }
                else if (_cell.Type == CellType.Neighboor)
                {
                    var typeface = new Typeface(new FontFamily("Times New Roman"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
                    var formattedText = new FormattedText($"{_cell.CountNeighboors}", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 20, new SolidColorBrush(Colors.Black), 1);
                    context.DrawText(formattedText, new Point(Position.X + Size.Width / 3, Position.Y));
                }
            }
        }
        
        private Brush GetCellColor()
        {
            return _cell.IsRevealed ? new SolidColorBrush(Colors.Gray) : new SolidColorBrush(Colors.White);
        }
    }
}