using System.Windows.Controls;
using System.Windows.Input;
using Minesweeper.ViewModels;

namespace Minesweeper.Views
{
    public partial class GameBoardView : UserControl
    {
        public GameBoardView()
        {
            InitializeComponent();
        }

        private void OnLeftMouseButtonClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender as ContentControl == null) return;

            var viewModel = DataContext as GameBoardViewModel;

            viewModel?.HandleCellClick(e.GetPosition(sender as ContentControl), 0);
            viewModel?.Invalidate();
        }

        private void OnRightMouseButtonClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender as ContentControl == null) return;

            var viewModel = DataContext as GameBoardViewModel;
            
            viewModel?.HandleCellClick(e.GetPosition(sender as ContentControl), 1);
            viewModel?.Invalidate();
        }
    }
}