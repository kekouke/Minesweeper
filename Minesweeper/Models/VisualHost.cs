using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Minesweeper.Models
{
    public class VisualHost : FrameworkElement, IDrawable
    {
        private VisualCollection _children;
        protected override int VisualChildrenCount => _children.Count;

        public VisualHost()
        {
            _children = new VisualCollection(this);
        }

        public void Draw(GameCellViewModel[,] gameCellController)
        {
            _children.Clear();

            var drawingVisual = new DrawingVisual();
            using (DrawingContext context = drawingVisual.RenderOpen())
            {
                foreach (var cellController in gameCellController)
                {
                    cellController.Draw(context);
                }
            }

            _children.Add(drawingVisual);
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new IndexOutOfRangeException();
            }

            return _children[index];
        }
    }
}
