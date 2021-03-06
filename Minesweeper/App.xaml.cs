using System.Windows;
using Minesweeper.Views;
using Minesweeper.ViewModels;
using Prism.Ioc;
using Prism.Unity;
using Minesweeper.Models;

namespace Minesweeper
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDrawable, VisualHost>();
        }

        protected override Window CreateShell()
        {
            var window = Container.Resolve<MainWindow>();

            window.DataContext = Container.Resolve<MainWindowViewModel>();

            return window;
        }
    }
}
