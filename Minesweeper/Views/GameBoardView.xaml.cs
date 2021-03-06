using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Minesweeper.ViewModels;
using Prism.Events;

namespace Minesweeper.Views
{
    public partial class GameBoardView : UserControl
    {
        public GameBoardView()
        {
            InitializeComponent();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender as ContentControl == null) return;

            var viewModel = DataContext as GameBoardViewModel;

            viewModel?.HandleCellClick(e.GetPosition(sender as ContentControl));
            viewModel?.Invalidate();
        }
    }
}