using System.Windows.Controls;
using System.Windows.Input;
using Minesweeper.ViewModels;

namespace Minesweeper.Views
{
    public partial class GameBoardView : UserControl
    {
        private readonly GameBoardViewModel _viewModel;
        public GameBoardView()
        {
            InitializeComponent();
            
            Canvas.Children.Add(new VisualHost());
            
            _viewModel = new GameBoardViewModel(Canvas.Children[0] as VisualHost);
            DataContext = _viewModel;
        }
        
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.HandleCellClick(e.GetPosition(sender as Canvas));
            _viewModel.Invalidate();
        }
    }
}