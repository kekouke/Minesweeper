using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameController _controller;
        public MainWindow()
        {
            InitializeComponent();
            Canvas.Children.Add(new VisualHost());
            _controller = new GameController(Canvas.Children[0] as VisualHost);
            DataContext = _controller;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _controller.HandleCellClick(e.GetPosition(sender as Canvas));
        }
    }
}
