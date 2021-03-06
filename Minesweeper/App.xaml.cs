using System.Windows;
using Minesweeper.Views;
using Minesweeper.ViewModels;
using Prism.Ioc;
using Prism.Unity;
using Minesweeper.Models;
using Minesweeper.Services;

namespace Minesweeper
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDrawable, VisualHost>();
            containerRegistry.Register<IGameConfigurationService, GameConfigurationService>();
        }

        protected override Window CreateShell()
        {
            var window = Container.Resolve<MainWindow>();

            window.DataContext = Container.Resolve<MainWindowViewModel>();

            return window;
        }
    }
}
