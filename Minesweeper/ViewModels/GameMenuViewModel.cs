using System.Windows;
using Minesweeper.Events;
using Minesweeper.Models;
using Minesweeper.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.ViewModels
{
    public class GameMenuViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IGameConfigurationService _configService;
        
        public DelegateCommand RestartGameCommand { get; }
        
        public DelegateCommand BeginnerNewGameCommand { get; }
        public DelegateCommand AdvancedNewGameCommand { get; }
        public DelegateCommand ExpertNewGameCommand { get; }


        public GameMenuViewModel(IEventAggregator eventAggregator, IGameConfigurationService configSrvice)
        {
            _eventAggregator = eventAggregator;
            _configService = configSrvice;
            
            RestartGameCommand = new DelegateCommand(RestartGame);
            
            BeginnerNewGameCommand = new DelegateCommand(() 
                => StartNewGame(_configService.GetBeginnerConfig));
            
            AdvancedNewGameCommand = new DelegateCommand(() 
                => StartNewGame(_configService.GetAdvancedConfig));
            
            ExpertNewGameCommand = new DelegateCommand(() 
                => StartNewGame(_configService.GetExpertConfig));
        }

        private void StartNewGame(GameConfiguration configuration) =>
            _eventAggregator.GetEvent<ChangeGameConfigEvent>().Publish(configuration);


        private void RestartGame() =>
            _eventAggregator.GetEvent<RestartGameEvent>().Publish();

    }
}